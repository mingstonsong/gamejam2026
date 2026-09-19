using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveForce;
    public float maxHorizontalSpeed;
    public int numFlaps = 3;
    public float flapForce = 8f;
    public float maxUpSpeed = 6f;     // caps how fast you can rise
    public float maxFallSpeed = 12f;  // caps how fast you can fall (separate from rise cap)

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && numFlaps > 0)
        {
            rb.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse);
            numFlaps--;

            // Immediately clamp so a rapid second flap can't stack past the cap
            if (rb.velocity.y > maxUpSpeed)
                rb.velocity = new Vector2(rb.velocity.x, maxUpSpeed);
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
    }

}