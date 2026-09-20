using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
   public GameObject bulletPrefab;
    public Transform[] firePoints;

    [Header("Timing")]
    public float minDelay = 1f;     
    public float maxDelay = 3f;    

    [Header("Burst")]
    public int shotsPerBurst = 1;  
    public float burstSpacing = 0.15f;

    public enum FireMode { AllAtOnce, Cycle, Random }
    public FireMode fireMode = FireMode.AllAtOnce;

    private int cycleIndex = 0;

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
                FireShot();
                yield return new WaitForSeconds(burstSpacing);
            }
        }
    }

    void FireShot()
    {
        if (firePoints.Length == 0) return;

        switch (fireMode)
        {
            case FireMode.AllAtOnce:
                foreach (Transform fp in firePoints)
                    Instantiate(bulletPrefab, fp.position, fp.rotation);
                break;

            case FireMode.Cycle:
                Transform point = firePoints[cycleIndex];
                Instantiate(bulletPrefab, point.position, point.rotation);
                cycleIndex = (cycleIndex + 1) % firePoints.Length;
                break;

            case FireMode.Random:
                Transform randPoint = firePoints[Random.Range(0, firePoints.Length)];
                Instantiate(bulletPrefab, randPoint.position, randPoint.rotation);
                break;
        }
    }
}
