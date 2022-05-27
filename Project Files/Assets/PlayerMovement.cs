using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Grid grid;

	// Use this for initialization
	void Start ()
    {
        grid = GetComponentInParent<Grid>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
