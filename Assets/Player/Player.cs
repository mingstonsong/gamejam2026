using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerPhysicsMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 7.0f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Rigidbody2D rb;
    private float moveX;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundMask);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        Vector2 velocity = rb.velocity; // Unity 2022: use .velocity, not .linearVelocity
        velocity.x = moveX * moveSpeed;
        rb.velocity = velocity;
    }
}