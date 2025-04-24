using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BallMovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float minPickupSpeed;

    public Rigidbody2D rigidBody2D;

    [SerializeField]
    CircleCollider2D ballCollider;
    
    public SpriteRenderer spriteRenderer;
    
    public int holder; 
    public bool held;


    void Start()
    {
        spriteRenderer.color = new Color(255,255,255);
    }

    // Update is called once per frame
    void Update()
    {

        if (holder != -1 && rigidBody2D.velocity.sqrMagnitude < minPickupSpeed && !held){
            Debug.Log("back to -1");
            holder = -1;
            spriteRenderer.color = new Color(255,255,255);
        }
    }
}
