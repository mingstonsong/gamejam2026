
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
    public Vector2 lastCheckpointPos;

    private void Awake()
    {
        // Ensures only one GameManager exists across the game
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
