using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float ballSpeed, friction;

    [SerializeField]
    Rigidbody2D rigidBody;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity *= friction;
    }
}
