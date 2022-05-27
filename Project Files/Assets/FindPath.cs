using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FindPath : MonoBehaviour
{
    public Node[,] test;
    public Node[,] workableMap;
    public Walls wallsInfo;
    public List<Node> openSet;
    public List<Node> closedSet;
    public List<Node> path;
    public Node start;
    public Node goal;
    

    // Start is called before the first frame update
    public void Setup(Walls walls)
    {

        this.wallsInfo = walls;
        this.workableMap = new Node[wallsInfo.nodes.GetLength(0), wallsInfo.nodes.GetLength(0)];
        this.test = new Node[wallsInfo.nodes.GetLength(0), wallsInfo.nodes.GetLength(0)];


        for (int i = 0; i < this.wallsInfo.nodes.GetLength(0); i++)
        {
            for (int j = 0; j < this.wallsInfo.nodes.GetLength(0); j++)
            {
                Node nodeScript = this.wallsInfo.nodes[i, j].GetComponent<Node>();
                test[i, j] = nodeScript;
            }
        }


        for (int i = 0; i < this.test.GetLength(0); i++)
        {
            for (int j = 0; j < this.test.GetLength(0); j++)
            {
                workableMap[i, j] = test[i,j];
            }
        }
    }

    public double ReturnDistance(Node current, Node neighbour)
    {
        double distance;
        distance = Math.Sqrt(Math.Pow(current.xPosition - neighbour.xPosition, 2) + Math.Pow(current.zPosition - neighbour.zPosition, 2));
        return distance;
    }

   
    

    public void ReturnPath(Node goalNode)
    {
        Node currentNode = goalNode;

        
        while (currentNode != start)
        {
            path.Add(currentNode);
            currentNode = currentNode.previous;
        }
        //Debug.Log("Path size in FindPath " + path.Count);
        //DrawPath();
    }

    //Draw the path for debugging
    public void DrawPath()
    {
        foreach (Node node in path)
        {
            MeshRenderer mesh = node.GetComponent<MeshRenderer>();
            mesh.enabled = true;
        }
    }

    public void UndrawPath()
    {
        foreach (Node node in path)
        {
            MeshRenderer mesh = node.GetComponent<MeshRenderer>();
            mesh.enabled = false;
        }
    }


    public void ResetPath()
    {
        foreach (Node node in workableMap)
        {
            node.previous = null;
        }
        foreach (Node node in path)
        {
            MeshRenderer mesh = node.GetComponent<MeshRenderer>();
            mesh.enabled = false;
        }

        path.Clear();
        openSet.Clear();
        closedSet.Clear();
    }





    public List<Node> AStarSearch(int startX, int startZ, int goalX, int goalZ)
    {
        ResetPath();

        //Set the start and goal nodes
        start = workableMap[startZ, startX];
        goal = workableMap[goalZ, goalX];
        


        //Add starting node to list  
        openSet.Add(start);


        //Loop while the open set is not empty
        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            //path.Add(currentNode);
            Node nodeScriptOfCurrent = currentNode.GetComponent<Node>();

            //Getting the best node from the openset, first loop through there's only one to choose from which is the start node
            for (int i = 1; i < openSet.Count; i++)
            {
                //if two nodes have the same F score we pick the one which is closer to the goal, with the better H score
                if (openSet[i].fValue < currentNode.fValue || openSet[i].fValue == currentNode.fValue && openSet[i].hValue < currentNode.hValue)
                {
                    currentNode = openSet[i];
                    //path.Add(currentNode);
                }
            }

            //remove the current node from the open set as we're now done with it and add current node to the closed set 
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            //path.Add(currentNode);
           

            //If it's the goal you know what to do
            if (currentNode == goal)
            {
                ReturnPath(currentNode);
                return path;
            }


            //Loop through every neighbour of the current node 
            for (int i = 0; i < currentNode.neighbours.Count; i++)
            {
                Node neighbour = currentNode.neighbours[i];

                //Check the closed list to see if the neighbour is in that list, if so the loop breaks the current iteration and goes again 
                if (closedSet.Contains(neighbour))
                {
                    continue;
                }

                //Calculate the distance from the neighbour to the start 
                double newMovementCostToNeighbour = currentNode.gValue + ReturnDistance(currentNode, neighbour);

                //Check to see if distance from the start to the neighbour through the current node is a shorter distance than the G score of the neighbour currently 
                //Add check if the neighbour is in the open set
                if (newMovementCostToNeighbour < neighbour.gValue || !openSet.Contains(neighbour))
                {
                    neighbour.gValue = newMovementCostToNeighbour;
                    neighbour.hValue = ReturnDistance(neighbour, goal);
                    neighbour.previous = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
        return path;
    }
}
