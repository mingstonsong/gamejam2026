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
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        leftTarget = transform.position + Vector3.left * moveDistance;
        rightTarget = transform.position + Vector3.right * moveDistance;

        currentTarget = leftTarget;
        sr.flipX = false; // matches "moving left" at the start — flip if it looks backwards
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentTarget) < 0.01f)
        {
            if (currentTarget == rightTarget)
            {
                currentTarget = leftTarget;
                sr.flipX = false;
            }
            else
            {
                currentTarget = rightTarget;
                sr.flipX = true;
            }
        }
    }
}
