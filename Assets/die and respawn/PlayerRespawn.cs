using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{

    private void Start()
    {
        // Set initial spawn position as the first checkpoint
        if (GameManager.instance != null)
        {
            GameManager.instance.lastCheckpointPos = transform.position;
        }
    }
    public void Respawn()
    {
        Debug.Log("Respawn function was successfully called!");
        
        // Teleport the player
        transform.position = GameManager.instance.lastCheckpointPos;
        
        // Fully reset physics so they don't instantly fall again
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false; // Temporarily pause physics
            rb.simulated = true;  // Turn physics back on to drop smoothly
        }
    }

}
