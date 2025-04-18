using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    float playerSpeed;
    [SerializeField]
    Rigidbody2D rigidBody;
    
    private Vector3 velocity = new Vector3(0.0f,0.0f,0.0f);
    private GameObject holdingBall = null;
    
    private GameObject[] balls;
    private Vector3 pointingDirection;
    
    private float pickupCooldown = 1.0f;
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
            Rigidbody2D ballRigidBody = holdingBall.GetComponent<Rigidbody2D>();
            holdingBall = null;
            pickupCounter += Time.deltaTime;

            ballRigidBody.velocity = pointingDirection * throwStrength * ballRigidBody.mass;
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
        GameObject tempHolding = null;
        if (holdingBall != null || pickupCounter != 0){
            return;
        }

        balls = GameObject.FindGameObjectsWithTag("Ball"); 
        float minDist = Mathf.Infinity;

        foreach (GameObject ball in balls){
            float dist = Vector3.Distance(transform.position,ball.transform.position);
            if (dist < minDist){
                minDist = dist;
                tempHolding = ball;
            }
        }       
        
        if (minDist < transform.localScale.x * 2.0){
            holdingBall = tempHolding;
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
