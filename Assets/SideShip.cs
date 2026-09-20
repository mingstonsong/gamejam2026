using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SideShip : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 10f; // safety net in case it never gets cleaned up otherwise
    public string playerTag = "Player";

    // direction is set by whatever spawns this ship (1 = move right, -1 = move left)
    [HideInInspector] public float direction = 1f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
