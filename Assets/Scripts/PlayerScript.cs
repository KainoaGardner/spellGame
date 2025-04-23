using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    float playerSpeed;
    [SerializeField]
    Rigidbody2D rigidBody;
    
    private Vector3 velocity = new Vector3(0.0f,0.0f,0.0f);
    private BallMovementScript holdingBall = null;
    public List<BallMovementScript> ballScripts;
    private Vector3 pointingDirection;
    
    [SerializeField]
    float pickupCooldown;
    
    [SerializeField]
    int playerId,colorIndex;
    
    private float pickupCounter = 0;
    private float throwStrength = 30;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SetPointingDireciton();
        PlayerMovement();
        SetGrabbingBall();
        HoldingBall();
        
        if (pickupCounter != 0){
            pickupCounter += Time.deltaTime;
        }
        if (pickupCounter >= pickupCooldown){
            pickupCounter = 0;
        }
        
        if (holdingBall != null && Input.GetMouseButtonDown(0)){
            holdingBall = null;
            pickupCounter += Time.deltaTime;

            holdingBall.rigidBody2D.velocity = pointingDirection * throwStrength * holdingBall.rigidBody2D.mass;
        }
        
        
    }
    
    void PlayerMovement(){
        Vector2 move = Vector2.zero;

        if (Input.GetKey(KeyCode.W)){
            move.y += 1.0f;
        }
        if (Input.GetKey(KeyCode.S)){
            move.y -= 1.0f;
        }
        if (Input.GetKey(KeyCode.D)){
            move.x += 1.0f;
        }
        if (Input.GetKey(KeyCode.A)){
            move.x -= 1.0f;
        }
        move = move.normalized;
        rigidBody.velocity += move * playerSpeed;       
    }
    
    void SetGrabbingBall(){
        BallMovementScript tempHolding = null;
        if (holdingBall != null || pickupCounter != 0){
            return;
        }

        float minDist = Mathf.Infinity;
        foreach (BallMovementScript ball in ballScripts){
            float dist = Vector3.Distance(transform.position,ball.transform.position);
            if (dist < minDist && ball.holder == -1){
                minDist = dist;
                tempHolding = ball;
            }
        }       
        
        if (minDist < transform.localScale.x * 2.0){
            holdingBall = tempHolding;
            holdingBall.holder = playerId;
        }
    }
    
    void HoldingBall(){
        if (holdingBall == null){
            return;
        }

        float holdDistance = (holdingBall.transform.localScale.x / 2.0f) + (transform.localScale.x / 2.0f);
        holdingBall.transform.position = pointingDirection * holdDistance + transform.position;
    }
    
    void SetPointingDireciton(){
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pointingDiff = mousePos - transform.position;
        float holdAngle = Mathf.Atan2(pointingDiff.y,pointingDiff.x);
        pointingDirection = new Vector3(Mathf.Cos(holdAngle),Mathf.Sin(holdAngle));
        pointingDirection = pointingDiff.normalized;
    }
}
