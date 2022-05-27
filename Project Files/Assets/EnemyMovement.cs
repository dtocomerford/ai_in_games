using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
   
    public Node target;
    public int xPosition;
    public int zPosition;
    public double speed;




	// Use this for initialization
	void Start ()
    {
        //int distance = Math.Abs(x1 - x0) + Math.Abs(y1 - y0);
    }
	
	// Update is called once per frame
	void Update ()
    {

		
	}

    void MoveToTarget()
    {
        if (this.xPosition != target.xPosition && this.zPosition != target.zPosition && target != null)
        {

        }
        
    }
}
