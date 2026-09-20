using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
    Debug.Log("Something hit the death floor: " + other.name);

        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<PlayerRespawn>(out PlayerRespawn player))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
            else
            {
                Debug.LogError("Found the Player, but the Player is missing the PlayerRespawn script!");
            }
        }
    }
}