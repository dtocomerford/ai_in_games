using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class GenerateMap : MonoBehaviour
{
    public Node[,] grid = new Node[10, 10];
    public GameObject wallBlock;
    public GameObject groundPlane;
    public GameObject player;
    public GameObject enemy;
    public Node playerNode;
    public Node enemyNode;

    public int xPosition = 0;
    public int zPosition = 0;


    public int[,] layout = new int[10, 10] {
                                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                {0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                                {0, 1, 0, 0, 0, 1, 0, 1, 1, 0},
                                {0, 1, 0, 0, 0, 1, 0, 1, 0, 0},
                                {0, 1, 0, 0, 1, 1, 1, 1, 0, 0},
                                {0, 1, 1, 1, 1, 0, 0, 1, 0, 0},
                                {0, 1, 0, 0, 1, 0, 0, 1, 1, 0},
                                {0, 1, 0, 0, 1, 1, 1, 0, 1, 0},
                                {0, 1, 1, 1, 7, 0, 1, 1, 8, 0},
                                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
                                };
    public static Node[,] nodes = new Node[10, 10];
    public GameObject[,] gameObjects = new GameObject[10, 10];


    // Use this for initialization
    void Start ()
    {


        Player player = new Player(55, 45);
        Debug.Log(player.xPosition);

        /*
        float xDistance = playerNode.xPosition - enemyNode.xPosition;
        float zDistance = playerNode.zPosition - enemyNode.zPosition;
        
        if (xDistance < 0)
        {
            xDistance *= -1;
        }
        if (zDistance < 0)
        {
            zDistance *= -1;
        }

        Debug.Log("Player X: " + playerNode.xPosition);
        Debug.Log("Player Z: " + playerNode.zPosition);
        Debug.Log("Enemy X: " + enemyNode.xPosition);
        Debug.Log("Enemy Z: " + enemyNode.zPosition);


        Debug.Log("X: " + xDistance);
        Debug.Log("Z: " + zDistance);
        Debug.Log("Taxicab sum: " + (xDistance + zDistance));
        
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            DestroyGrid();
            Debug.Log("Called destroy");
        }

        if (Input.GetKeyDown("return"))
        {
            DrawGrid();
            GetNeighbours();
        }
        if (Input.GetKeyDown("a"))
        {
            Debug.Log("Called move");
            
        }

        
    }

    //Draw grid in 3D space
    public void DrawGrid()
    {
        xPosition = 0;
        zPosition = 0;


        //Loop through the 2d grid instantiate a gameobject for each position in the grid
        for (int i = 0; i < nodes.GetLength(0); i++)
        {
            for (int j = 0; j < nodes.GetLength(0); j++)
            {
                if (layout[i, j] == 0)
                {
                    Node node = new Node('0', false, j, i, false, false);
                    nodes[i, j] = node;
                    gameObjects[i,j]= Instantiate(wallBlock, new Vector3(xPosition, 0, zPosition), Quaternion.identity);
                }
                if (layout[i, j] == 1)
                {
                    Node node = new Node('1', true, j, i, false, false);
                    nodes[i, j] = node;
                }
                if (layout[i, j] == 7)
                {
                    playerNode = new Node('P', true, j, i, true, false);
                    gameObjects[i, j] = Instantiate(player, new Vector3(xPosition, 0, zPosition), Quaternion.identity);
                    nodes[i, j] = playerNode;
                }
                if (layout[i, j] == 8)
                {
                    enemyNode = new Node('E', true, j, i, false, true);
                    gameObjects[i, j] = Instantiate(enemy, new Vector3(xPosition, 0, zPosition), Quaternion.identity);
                    nodes[i, j] = enemyNode;
                }

                xPosition += 1;
            }
            xPosition = 0;
            zPosition += 1;

        }

        Instantiate(groundPlane, new Vector3(4.5f, -1, 4.5f), Quaternion.identity);
    }

    public void DestroyGrid()
    {
        Debug.Log("Called");
        for (int i = 0; i < nodes.GetLength(0); i++)
        {
            for (int j = 0; j < nodes.GetLength(0); j++)
            {
                Destroy(gameObjects[i,j]);
            }
        }
    }


    public void GetNeighbours()
    {
        for (int i = 0; i < nodes.GetLength(0); i++)
        {
            for (int j = 0; j < nodes.GetLength(0); j++)
            {
                nodes[i, j].GetNeighbours();


                Debug.Log("Node " + i + " - "  +j + ": " + nodes[i, j].neighbours.Count);
            }
        }
    }

}

	

    */
