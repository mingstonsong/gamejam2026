using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
   public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Timing")]
    public float minDelay = 1f;     // shortest wait between volleys
    public float maxDelay = 3f;     // longest wait between volleys

    [Header("Burst")]
    public int shotsPerBurst = 3;   // set to 1 for single shots
    public float burstSpacing = 0.15f;

    void Start()
    {
        StartCoroutine(FireLoop());
    }

    System.Collections.IEnumerator FireLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            for (int i = 0; i < shotsPerBurst; i++)
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                yield return new WaitForSeconds(burstSpacing);
            }
        }
    }
}
