 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Agent
{
    public EnemyState enemyState;
    public float timeInState;
    public int targetPositionX;
    public int targetPositionZ;
    public List<Node> walkableNodes;
    public List<GameObject> pathGameObjects;
    //public List<GameObject> nodeGameObjects;
    public int positonOfEnemyOnPath;
    public FindPath findPath;
    public Walls walls;
    public bool chaser;
    public Coroutine UpdatePositionIE;
    public bool followingPath = false;
    public int timer;
    public int targetX;
    public int targetZ;
    public List<Node> pathToFollow;
    public List<GameObject> scaredNodes;
    
    public Enemy(int x, int z, GameObject prefab, float speed, Walls walls, FindPath findPath) : base(x, z, prefab, speed, walls)
    {
        //Instantiate prefab from super class
        /*
        this.xPosition = x;
        this.zPosition = z;
        this.enemyState = this.gameObject.GetComponent<EnemyState>();
        this.timeInState = 400;
        */
    }

    public void Setup(int x, int z, Walls walls, bool isChaser)
    {
        this.xPosition = x;
        this.zPosition = z;
        this.enemyState = GetComponent<EnemyState>();
        this.timeInState = 400;
        this.speed = 2;
        this.positonOfEnemyOnPath = 0;
        this.timer = 0;
        this.findPath = this.gameObject.GetComponent<FindPath>();
        this.enemyState.currentState = EnemyState.State.Chase;


        //Set up the four corner nodes as targets for the enemies to run to when scared
        scaredNodes.Add(walls.nodes[1, 15]);
        scaredNodes.Add(walls.nodes[1, 1]);
        scaredNodes.Add(walls.nodes[15, 15]);
        scaredNodes.Add(walls.nodes[15, 1]);
        for (int i = 0; i < scaredNodes.Count; i++)
        {
            Node temp = scaredNodes[i].GetComponent<Node>();
            temp.isScaredNode = true;
        }



        findPath.Setup(walls);



        //Loop through all the nodes in the game world
        foreach (GameObject obj in walls.nodes)
        {
            //Check to see if the node is walkable, if it is store the node in a list
            //When the enemy is in the scatter state a random node from walkable nodes will be chosen as the target
            Node node = obj.GetComponent<Node>();
            if (node.isWalkable)
            {
                walkableNodes.Add(node);
            }
        }
    }


   


    public void GhostBehaviour(GameObject player)
    {
        timer++;
        this.xPosition = (int)this.gameObject.transform.position.x;
        this.zPosition = (int)this.gameObject.transform.position.z;

        
        //Check to see if the player is in the game
        //If not then the timer is frozen so states don't change and the enemy will continue in scatter mode
        if (GameManager.playerInGame == true)
        {
            pathToFollow = findPath.AStarSearch((int)this.gameObject.transform.position.x, (int)this.gameObject.transform.position.z, targetX, targetZ);
        }
        else
        {
            this.enemyState.currentState = EnemyState.State.Scatter;
        }

        if (GameManager.cherryEaten == true)
        {
            this.enemyState.currentState = EnemyState.State.Scared;
            GameManager.cherryEaten = false;
        }


        //State machine which dictates the enemies behaviour 
        switch (this.enemyState.currentState)
        {
            case EnemyState.State.Chase:
                Debug.Log("Chase");

                Run(pathToFollow);
                pathToFollow.Clear();
                if (timer > 1000)
                {
                    timer = 0;
                    this.enemyState.currentState = EnemyState.State.Scatter;   
                }
                break;

            case EnemyState.State.Scatter:
                Debug.Log("Scatter");
                Node target = GetRandomNode();

                pathToFollow = findPath.AStarSearch((int)this.xPosition, (int)this.zPosition, target.xPosition, target.zPosition);
                Run(pathToFollow);
                
                if (timer > 1000)
                {
                    timer = 0;
                    this.enemyState.currentState = EnemyState.State.Chase;
                }
                break;



            case EnemyState.State.Scared:
                Debug.Log("Scared");
                target = GetScaredNode(player);
                pathToFollow = findPath.AStarSearch((int)this.xPosition, (int)this.zPosition, target.xPosition, target.zPosition);
                Run(pathToFollow);
                if (this.transform.position.x == target.xPosition && this.transform.position.z == target.zPosition)
                {
                    this.enemyState.currentState = EnemyState.State.Chase;
                }
                break;



            default:
                Debug.Log("Idle");
                break;
        }
    }


    public void Run(List<Node> pathToFollow)
    {
        if (followingPath == false)
        {
            pathToFollow.Reverse();

            followingPath = true;

            //Get gameobject the Node script is connected to
            foreach (Node node in pathToFollow)
            {
                GameObject nodeGameObject = node.gameObject;
                pathGameObjects.Add(nodeGameObject);
            }
            int pathSize = pathGameObjects.Count;

            //Start coroutine
            StartCoroutine(UpdatePosition());
        }  
    }


    //Take position of enemy on path as the value for the for loop, so on later loops the loop starts from where it left off
    IEnumerator UpdatePosition()
    {
        //pathGameObjects.Reverse();
        for (int i =positonOfEnemyOnPath; i < pathGameObjects.Count; i++)
        {
            positonOfEnemyOnPath++;
            UpdatePositionIE = StartCoroutine(Moving(i));
            yield return UpdatePositionIE;
        }
        followingPath = false;
    }




    IEnumerator Moving(int currentPosition)
    {
        float distance = speed * Time.deltaTime;

        while (this.transform.position != pathGameObjects[currentPosition].transform.position)
        {
            targetPositionX = (int)pathGameObjects[currentPosition].transform.position.x;
            targetPositionZ = (int)pathGameObjects[currentPosition].transform.position.z;


            Node node = pathGameObjects[currentPosition].GetComponent<Node>();
            this.transform.position = Vector3.MoveTowards(transform.position, pathGameObjects[currentPosition].transform.position, distance);
            yield return null;
        }
    }



    public int[] ReturnEnemyPosition()
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.SetActive(false);
            GameManager.playerInGame = false;
            this.enemyState.currentState = EnemyState.State.Scatter;
        }
    }

    public Node GetRandomNode()
    {
        Node target;
        int index = (Random.Range(0, walkableNodes.Count));

        target = walkableNodes[index];

        return target;
    }

    public Node GetScaredNode(GameObject player)
    {
        GameObject furthestNode = null;
        Node target;
        float distanceToBeat = 0;
        for (int i = 0; i < scaredNodes.Count; i++)
        {
            GameObject temp = scaredNodes[i];
            float distance = Vector3.Distance(temp.transform.position, player.transform.position);

            if (distance > distanceToBeat)
            {
                furthestNode = temp;
                distanceToBeat = distance;
            }
        }
        target = furthestNode.GetComponent<Node>();

        return target;
    }

    public void GetPlayerPositon(int x, int z)
    {
        targetX = x;
        targetZ = z;
    }
}
