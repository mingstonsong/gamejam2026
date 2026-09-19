using UnityEngine;

public class WaterCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.2f;
    public float yOffset = 0f;
    public float minY = -10f; // camera view won't show below this world y

    private float velocity;
    private Camera cam;

    void Start()
    {
        // Guaranteed to find the player instance
        target = GameObject.FindGameObjectWithTag("Player").transform;
        cam = GetComponent<Camera>();
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

        // Prevent the bottom edge of the camera's view from going below minY
        float halfHeight = cam.orthographicSize;
        float clampedY = Mathf.Max(newY, minY + halfHeight);

        transform.position = new Vector3(
            transform.position.x,
            clampedY,
            transform.position.z
        );
    }
}