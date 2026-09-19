using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimPlayer : MonoBehaviour
{
    #region Movement Variables

    [SerializeField] private float currentAcceleration;
    [SerializeField] private float defaultAccelerationAmount = 15f;

    [SerializeField] float rotationSpeed;
    [SerializeField] private float minimumVelocityThreshold;

    [SerializeField] private float exitWaterBoostMultiplier = 1.5f;

    private Vector2 inputVector = Vector2.zero;

    private SpriteRenderer sr;

    #endregion

    #region Movement Keys

    private KeyCode upMoveKey1 = KeyCode.W;
    private KeyCode upMoveKey2 = KeyCode.UpArrow;

    private KeyCode downMoveKey1 = KeyCode.S;
    private KeyCode downMoveKey2 = KeyCode.DownArrow;

    private KeyCode leftMoveKey1 = KeyCode.A;
    private KeyCode leftMoveKey2 = KeyCode.LeftArrow;

    private KeyCode rightMoveKey1 = KeyCode.D;
    private KeyCode rightMoveKey2 = KeyCode.RightArrow;

    #endregion

    #region References

    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private SpriteRenderer sprite;
    private CapsuleCollider2D capsuleCollider2D;

    #endregion

    #region Resetting

    [SerializeField] private Vector2 startingPosition;
    [SerializeField] private Vector3 startingRotation;

    #endregion

    #region States

    private enum State
    {
        TouchingWater,
        NotTouchingWater,
    }

    [SerializeField] private State state;

    #endregion

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        state = State.TouchingWater;
        currentAcceleration = defaultAccelerationAmount;
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        if (state == State.TouchingWater)
        {
            HandleMovement();
        }
    }

    public void FishOneGameStartVsMode()
    {
        ResetPlayerOne();
    }

    #region Movement Handling

    private void HandleInput()
    {
        float x = 0f;
        float y = 0f;

        if (Input.GetKey(leftMoveKey1) || Input.GetKey(leftMoveKey2)) x -= 1f;
        if (Input.GetKey(rightMoveKey1) || Input.GetKey(rightMoveKey2)) x += 1f;
        if (Input.GetKey(upMoveKey1) || Input.GetKey(upMoveKey2)) y += 1f;
        if (Input.GetKey(downMoveKey1) || Input.GetKey(downMoveKey2)) y -= 1f;

        inputVector = new Vector2(x, y);
    }

private void HandleMovement()
{
    rigidBody2D.velocity += inputVector.normalized * currentAcceleration * Time.deltaTime;

    if (rigidBody2D.velocity != Vector2.zero && rigidBody2D.velocity.magnitude < minimumVelocityThreshold)
    {
        rigidBody2D.velocity = Vector2.zero;
    }
    else if (rigidBody2D.velocity != Vector2.zero)
    {
        float velocityAngle = Mathf.Atan2(rigidBody2D.velocity.y, rigidBody2D.velocity.x) * Mathf.Rad2Deg;

        // Sprite's default pose faces right, with its "top" pointing local up —
        // so its top is 90° counter-clockwise from its facing direction.
        // To make the top point along velocity, offset by -90.
        float targetAngle = velocityAngle - 90f;

        Quaternion toRotation = Quaternion.Euler(0f, 0f, targetAngle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }
}

    #endregion

    #region Collisions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out WaterColliderScript waterColliderScript) && rigidBody2D.gravityScale != 0)
        {
            rigidBody2D.gravityScale = 0;
            rigidBody2D.drag = 1;
            state = State.TouchingWater;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out WaterColliderScript waterColliderScript) && rigidBody2D.gravityScale != 1)
        {
            rigidBody2D.gravityScale = 1;
            rigidBody2D.drag = 0;
            state = State.NotTouchingWater;

            if (rigidBody2D.velocity.y > 0)
            {
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, rigidBody2D.velocity.y * exitWaterBoostMultiplier);
            }
        }
    }

    #endregion

    #region Resetting

    private void ResetPlayerOne()
    {
        transform.position = startingPosition;
        transform.rotation = Quaternion.Euler(startingRotation);
        rigidBody2D.velocity = Vector2.zero;
        rigidBody2D.angularVelocity = 0f;
        sr.flipY = false;
    }

    #endregion
}