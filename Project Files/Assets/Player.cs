using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{
    public Vector3 playerDirection = Vector3.zero;
    public Vector3 nextDirection = Vector3.zero;
    public RaycastPlayer raycastScript;
    public int raycastLayer;


    //Not being used
    public Player(int x, int z, GameObject prefab, Walls walls, float speed) : base(x, z, prefab, speed, walls)
    {
        //this.xPosition = x;
        //this.zPosition = z;
        //this.wallsInfo = walls;
        //this.speed = speed;
        //this.raycastLayer = 6;
        //raycastScript = this.gameObject.GetComponent<RaycastPlayer>();
    }
    

    //Called in Game Manger start method 
    public void Setup(int x, int z)
    {
        this.xPosition = x;
        this.zPosition = z;
        this.raycastScript = GetComponent<RaycastPlayer>();
    }


    public void UpdatePosition()
    {
        if (this.nextDirection != Vector3.zero)
        {
            SetDirection(this.nextDirection);
        }

        this.gameObject.transform.Translate(playerDirection * Time.deltaTime * this.speed);
        
    }



    public int[] ReturnPlayerPosition()
    {
        int[] agentPositions = new int[2];

        
        this.xPosition = this.gameObject.transform.position.x;
        this.zPosition = this.gameObject.transform.position.z;
        int roundedX = (int)System.Math.Round(xPosition, 0);
        int roundedZ = (int)System.Math.Round(zPosition, 0);


        agentPositions[0] = roundedX;
        agentPositions[1] = roundedZ;


        return agentPositions;
    }



    public void SetDirection(Vector3 inputDirection)
    {
       
        bool isNodeOccupied = Occupied(inputDirection);
        //Debug.Log("Occupied: " + isNodeOccupied);
        //Debug.Log("Next Direction: " + );
        if (isNodeOccupied == false)
        {
            this.playerDirection = inputDirection;
            this.nextDirection = Vector3.zero;
        }
        else
        {
            this.nextDirection = inputDirection;
        }
        
    }


    
    public bool Occupied(Vector3 inputDirection)
    {
        raycastScript.direction = inputDirection;
        RaycastHit hit;
        Physics.BoxCast(this.gameObject.transform.position, this.gameObject.transform.lossyScale / 2, inputDirection, out hit, this.gameObject.transform.rotation, 0.2f);
        
        return hit.collider != null;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Cherry" && this.gameObject.tag == "Player")        {
            GameManager.cherryEaten = true;
            Destroy(other.gameObject);
            Debug.Log("CALLED");
        }
    }
}
