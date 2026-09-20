using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideShipSpawner : MonoBehaviour
{
    public GameObject shipPrefab;

    [Header("Timing")]
    public float minDelay = 3f;
    public float maxDelay = 7f;

    [Header("Spawn area")]
    public float spawnX = 10f;     // how far off-screen left/right to spawn
    public float minY = -3f;       // random height range
    public float maxY = 3f;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    System.Collections.IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            SpawnShip();
        }
    }

    void SpawnShip()
    {
        // pick a side: -1 = spawn on left (moves right), 1 = spawn on right (moves left)
        float side = Random.value < 0.5f ? -1f : 1f;
        float spawnY = Random.Range(minY, maxY);

        // base spawn position off the camera so it stays off-screen as the player moves
        float camX = Camera.main.transform.position.x;
        Vector3 spawnPos = new Vector3(camX + side * spawnX, spawnY, 0f);
        GameObject ship = Instantiate(shipPrefab, spawnPos, Quaternion.identity);

        SideShip shipScript = ship.GetComponent<SideShip>();
        if (shipScript != null)
        {
            shipScript.direction = -side; // moves toward the opposite side it spawned on
        }
    }
}
