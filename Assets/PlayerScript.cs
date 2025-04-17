using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    float playerSpeed, friction;
    [SerializeField]
    Rigidbody2D rigidBody;

    private Vector3 velocity = new Vector3(0.0f,0.0f,0.0f);
    private GameObject holdingBall = null;
    
    private GameObject[] balls;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        
        SetGrabbingBall();
        
        if (holdingBall != null){
            Debug.Log(holdingBall);
        }
    }
    
    void PlayerMovement(){
        rigidBody.velocity *= friction;
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
        if (holdingBall != null){
        }

        balls = GameObject.FindGameObjectsWithTag("Ball"); 
        float minDist = Mathf.Infinity;

        foreach (GameObject ball in balls){
            float dist = Vector3.Distance(transform.position,ball.transform.position);
            if (dist < minDist){
                minDist = dist;
                holdingBall = ball;
            }
        }       
    }
}
