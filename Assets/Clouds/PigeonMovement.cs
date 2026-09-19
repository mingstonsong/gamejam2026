using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonMovement : MonoBehaviour
{
    public float speed = 3f;
    
    // How far left and right the object should go from its starting spot
    public float moveDistance = 50f; 

    private Vector3 leftTarget;
    private Vector3 rightTarget;
    private Vector3 currentTarget;
    private bool movingRight = false; 


    void Start()
    {
        // Calculate the boundaries based on the starting position
        leftTarget = transform.position + Vector3.left * moveDistance;
        rightTarget = transform.position + Vector3.right * moveDistance;
        
        // Start by moving toward the right
        currentTarget = leftTarget;
    }

    void Update()
{
    transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

    if (Vector3.Distance(transform.position, currentTarget) < 0.01f)
    {
        movingRight = !movingRight;
        currentTarget = movingRight ? rightTarget : leftTarget;
        Flip();
    }
}

void Flip()
{
    Vector3 scale = transform.localScale;
    scale.x = Mathf.Sign(currentTarget.x - transform.position.x) * Mathf.Abs(scale.x);
    transform.localScale = scale;
}
}
