using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

public class PaddleMovement : MonoBehaviour
{
    //WIP turn this into a universal variable
    [Header("CONSTANT")]
    [SerializeField] private float arenaSize = 10;

    [Header("References")]
    [SerializeField] private Input playerInput;
    [SerializeField] private Transform graphicsTransform;
    [SerializeField] private Transform colliderTransform;
    
    [Header("Paddle Stuff")]
    [SerializeField] private float paddleSize = 2f;
    [SerializeField] private float paddleSpeed = 10.0f;

    [SerializeField] private float growRate = 5;
    [SerializeField] private float colliderBuffer = 0.1f;
    [Header("World")]
    [SerializeField] private float worldSpeed = 1.0f;

    [Header("Debug")]
    [SerializeField] private float newSize;
    
    private void Awake() {
        graphicsTransform.localScale = new Vector3(paddleSize, 0.15f, 1 );
        colliderTransform.localScale = new Vector3(paddleSize + 0.1f, 0.15f, 1 );

    }
    void Start()
    {
        //DO NOT REMOVE or else things break.
        newSize = paddleSize;
    }

    // Update is called once per frame
    void Update()
    {
        //This order is important somehow to reduce visual bugs, don't fucking ask why cuz I don't know
        HandleSizeChange();
        MovePlayer(playerInput.GetMoveDir());
    }

    private void MovePlayer(float axisVal){
        //Shortest vector construction
        transform.position =  new Vector3(
            Math.Clamp(transform.position.x + axisVal * paddleSpeed * Time.deltaTime * worldSpeed, (paddleSize- arenaSize)/2, (arenaSize - paddleSize)/2),
            transform.position.y,
            transform.position.z
            );
    }

    private void HandleSizeChange(){
            if(newSize != paddleSize){
            newSize = Math.Clamp(newSize, 0, arenaSize);
            int sign = Math.Sign(newSize - paddleSize);
            paddleSize += worldSpeed * growRate * Time.deltaTime * Math.Sign(newSize - paddleSize);
            if(sign == 1){
                paddleSize = Math.Clamp(paddleSize, 0, newSize);
            }else if(sign == -1){   
                paddleSize = Math.Clamp(paddleSize, newSize, newSize+100);
            }
            graphicsTransform.localScale = new Vector3(paddleSize, graphicsTransform.localScale.y, graphicsTransform.localScale.z);
            colliderTransform.localScale = new Vector3(paddleSize + colliderBuffer, colliderTransform.localScale.y, colliderTransform.localScale.z);
        }
    }

    //Code Clarity and cuz newSize is private
    public void ChangeSize(float size){
        newSize = size;
    }

    //Probably Useless Function cuz other code automatically did what I intended.
    public int isTouchingWall(){
        return (Math.Abs(transform.position.x) >= arenaSize/2 - paddleSize/2? 1 : 0)* Math.Sign(transform.position.x);
    }


}
