using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class death : MonoBehaviour
{
    public string playerTag = "Player";
    public float deathY = -12f;
 
    private GameObject player;
 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
    }
 
    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
 
        if (player.transform.position.y < deathY)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
