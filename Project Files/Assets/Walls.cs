using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    public GameObject[,] nodes;
    public int xPosition = 0;
    public int zPosition = 0;


    public void Setup(int[,] layout, GameObject wallPrefab, GameObject groundPrefab, GameObject walkableNodePrefab)
    {
        //A 2D array of object of the Node class
        nodes = new GameObject[layout.GetLength(0), layout.GetLength(0)];

        //A function to go through the array of Node class objects and instantiate a GameObject for each
        InstantiateWalls(wallPrefab, layout, groundPrefab, walkableNodePrefab);
        //A function to get the neighbours of each node and store them on a list which each node has
        GetNeighbours();
    }




    public void InstantiateWalls(GameObject wallPrefab, int[,] grid, GameObject groundPrefab, GameObject walkableNodePrefab)
    {
        for (int i = 0; i < nodes.GetLength(0); i++)
        {
            for (int j = 0; j < nodes.GetLength(0); j++)
            {
                if (grid[i, j] == 0)
                {
                    GameObject wall = Instantiate(wallPrefab, new Vector3(xPosition, 0, zPosition), Quaternion.identity);
                    nodes[i, j] = wall;
                    Node nodeScript = wall.GetComponent<Node>();
                    nodeScript.Setup(i, j, false, false);
                    
                    
                }
                if (grid[i, j] == 1)
                {
                    GameObject empty = Instantiate(walkableNodePrefab, new Vector3(xPosition, 0, zPosition), Quaternion.identity);
                    nodes[i, j] = empty;
                    Node nodeScript = empty.GetComponent<Node>();
                    nodeScript.Setup(i, j, true, false);
                    
                }
                xPosition += 1;
            }
            xPosition = 0;
            zPosition += 1;
        }
        float groundSize = nodes.GetLength(0) / 2;
        Instantiate(groundPrefab, new Vector3(groundSize, -1, groundSize), Quaternion.identity);
    }






    public void GetNeighbours()
    {
        //Debug.Log("Length of nodes array " + nodes.GetLength(0));
        //Go through each node object in the 2D array of nodes
        for (int i = 0; i < nodes.GetLength(0); i++)
        {
            for (int j = 0; j < nodes.GetLength(0); j++)
            {
                Node nodeScript = nodes[i, j].GetComponent<Node>();
                

                //If the node has a node next to it that is walkable, add it to taht node list of neighbours
                if (nodeScript.zPosition < nodes.GetLength(0) - 1)
                {
                    //Get the script on the gameobject next to the current node to check to see if the node is walkable
                    Node nodeScriptUpOne = nodes[i, j+1].GetComponent<Node>();
                    if (nodeScriptUpOne.isWalkable == true)
                    {
                        nodeScript.neighbours.Add(nodeScriptUpOne);
                    }
                }

                if (nodeScript.zPosition > 1)
                {
                    Node nodeScriptDownOne = nodes[i, j - 1].GetComponent<Node>();
                    if (nodeScriptDownOne.isWalkable == true)
                    {
                        nodeScript.neighbours.Add(nodeScriptDownOne);
                    }
                }

                if (nodeScript.xPosition < nodes.GetLength(0) - 1)
                {
                    Node nodeScriptOneRight = nodes[i + 1, j].GetComponent<Node>();
                    if (nodeScriptOneRight.isWalkable == true)
                    {
                        nodeScript.neighbours.Add(nodeScriptOneRight);
                    }

                }
                if (nodeScript.xPosition > 1)
                {
                    Node nodeScriptOneLeft = nodes[i - 1, j].GetComponent<Node>();
                    if(nodeScriptOneLeft.isWalkable == true)
                    {
                        nodeScript.neighbours.Add(nodeScriptOneLeft);
                    }
                }
            }
        }
    }
}
