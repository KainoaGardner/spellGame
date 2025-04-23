using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScript : MonoBehaviour
{
    [SerializeField]
    GameObject Ball,Walls;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnBall(new Vector3(0.0f,-3.0f,1.0f));
        SpawnBall(new Vector3(0.0f,0.0f,1.0f));
        SpawnBall(new Vector3(0.0f,3.0f,1.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SpawnBall(Vector3 spawnPos){
        Instantiate(Ball,spawnPos,new Quaternion());
    }
}
