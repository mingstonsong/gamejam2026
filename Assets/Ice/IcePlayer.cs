using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class IcePlayer : MonoBehaviour
{
    public float moveForce;
    public float maxHorizontalSpeed;
    public int numFlaps = 3;
    public float flapSpeed = 8f;      // renamed: this is now a target velocity, not a force
    public float maxUpSpeed = 6f;     // caps how fast you can rise
    public float maxFallSpeed = 12f;  // caps how fast you can fall (separate from rise cap)

    public TMP_Text flapText;


    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public Animator anim;

    void Start()
    {
        
        // Guaranteed to find the player instance
        flapText = GameObject.FindGameObjectWithTag("FlapsUI").GetComponent<TMP_Text>();


        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        flapText.text = "Flaps: " + numFlaps;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && numFlaps > 0)
        {
            // Set velocity directly instead of adding to it — always the same flap,
            // whether you were falling, stationary, or already rising
            rb.velocity = new Vector2(rb.velocity.x, flapSpeed);
            numFlaps--;
            flapText.text = "Flaps: " + numFlaps;
        }
    }   

    void FixedUpdate()
    {
        float input = Input.GetAxisRaw("Horizontal");

        rb.AddForce(Vector2.right * input * moveForce, ForceMode2D.Force);

        // Clamp horizontal velocity
        float clampedX = Mathf.Clamp(rb.velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed);
        // Clamp vertical velocity: rise capped by maxUpSpeed, fall capped by maxFallSpeed
        float clampedY = Mathf.Clamp(rb.velocity.y, -maxFallSpeed, maxUpSpeed);

        rb.velocity = new Vector2(clampedX, clampedY);

        // Flip sprite without changing size
        if (input > 0)
            sr.flipX = false;
        else if (input < 0)
            sr.flipX = true;
        
        bool verticalVelocityIsZero = Mathf.Abs(rb.velocity.y) < 0.05f;

        // Running and jumping animation
        anim.SetBool("isRunning",verticalVelocityIsZero && input != 0);
        anim.SetBool("isFalling",rb.velocity.y < -0.05f);

    }

}