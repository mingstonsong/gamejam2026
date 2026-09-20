using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jiggle : MonoBehaviour
{
    // Start is called before the first frame update
public float jiggleAmount = 100f; // how far it moves side to side
    public float jiggleSpeed = 5f;    // how fast it jiggles

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // remember original position
    }

    // Update is called once per frame
    void Update()
    {
        float xOffset = Mathf.Sin(Time.time * jiggleSpeed) * jiggleAmount;
        transform.position = new Vector3(startPos.x + xOffset, startPos.y, startPos.z);
    }
}
