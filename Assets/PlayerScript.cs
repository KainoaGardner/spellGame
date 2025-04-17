using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Vector3 velocity = new Vector3(0.0f,0.0f,0.0f);
    
    [SerializeField]
    float playerSpeed, playerFriction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //change use the ridgid body velocity 
        velocity *= playerFriction;
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W)){
            move.y += playerSpeed;
        }
        if (Input.GetKey(KeyCode.S)){
            move.y -= playerSpeed;
        }
        if (Input.GetKey(KeyCode.D)){
            move.x += playerSpeed;
        }
        if (Input.GetKey(KeyCode.A)){
            move.x -= playerSpeed;
        }
        
        move = move.normalized;
        
        velocity += move * playerSpeed;
        transform.position += velocity * Time.deltaTime;
    }
}
