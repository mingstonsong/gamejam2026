using System;
using UnityEngine;

public class CarcinogenScript : MonoBehaviour
{
    // Static event: any script can subscribe without needing a direct reference to this object
    public static event Action OnCarcinogenCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnCarcinogenCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}