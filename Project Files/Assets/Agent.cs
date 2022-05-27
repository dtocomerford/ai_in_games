using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public float xPosition;
    public float zPosition;
    public float speed;
    public Walls wallsInfo;


    public Agent(int x, int z, GameObject prefab, float speed, Walls walls)
    {
        this.xPosition = x;
        this.zPosition = z;
        this.speed = speed;
        this.wallsInfo = walls;
    }


    

}
