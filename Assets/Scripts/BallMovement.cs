using System;
using Unity.Mathematics;
using UnityEngine;

public class BallMovement : MonoBehaviour
{   
    [SerializeField] private float spinAmount; //right spin is positive spin
    [SerializeField] private Vector2 currentVelocity;
    [SerializeField] private float speed = 3;
    [SerializeField] private float ballRadius;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D forwardRay= Physics2D.Raycast(transform.position + new Vector3(currentVelocity.x, currentVelocity.y, 0).normalized * ballRadius,
            currentVelocity + Vector2.Perpendicular(currentVelocity)*Time.deltaTime*spinAmount, speed * Time.deltaTime );
        
        if(forwardRay.collider != null){
            Debug.Log("hit");
            Bounce(forwardRay.normal, forwardRay.point);
        }else{
            Move();
        }

        
        //transform.position = transform.position + new Vector3(4 * Mathf.Cos(Time.time * 2) * Time.deltaTime, 4 * Mathf.Sin(Time.time * 2) * Time.deltaTime, 0);
    }

    private void Bounce(Vector2 normal, Vector2 bouncePoint){
        Vector3 snapVector = new Vector3(bouncePoint.x, bouncePoint.y, 0) - transform.position;
        transform.position += snapVector - snapVector.normalized*ballRadius; 
        currentVelocity = currentVelocity - Vector2.Dot(normal, currentVelocity) * 2 * normal ;
    }

    private void Move(){
        Vector2 newVelocity = (currentVelocity + Vector2.Perpendicular(currentVelocity)*Time.deltaTime*spinAmount).normalized * speed;
        transform.position = transform.position + new Vector3(newVelocity.x, newVelocity.y, 0)*Time.deltaTime;
        currentVelocity = newVelocity;

        //Debug
        Debug.DrawLine(transform.position + new Vector3(currentVelocity.x, currentVelocity.y, 0).normalized * ballRadius, transform.position + new Vector3(newVelocity.x, newVelocity.y, 0).normalized * speed * Time.deltaTime, Color.red);
    }

}