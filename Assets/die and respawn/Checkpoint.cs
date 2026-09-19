using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Update the GameManager with this checkpoint's position
            GameManager.instance.lastCheckpointPos = transform.position;
            Debug.Log("Checkpoint Activated!");
        }
    }
}