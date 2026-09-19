using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.2f;
    public float yOffset = 0f;

    float velocity;

    void Start()
    {
        // Guaranteed to find the player instance
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (target == null) return;

        float targetY = target.position.y + yOffset;

        float newY = Mathf.SmoothDamp(
            transform.position.y,
            targetY,
            ref velocity,
            smoothSpeed
        );

        transform.position = new Vector3(
            transform.position.x,
            newY,
            transform.position.z
        );
    }
}
