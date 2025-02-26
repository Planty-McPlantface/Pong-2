using System;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(0, 4 * Mathf.Sin(Time.time * 2) * Time.deltaTime, 0);
    }
}
