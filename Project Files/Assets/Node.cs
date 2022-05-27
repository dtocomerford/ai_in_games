using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int xPosition;
    public int zPosition;
    public bool isWalkable;
    public bool isPlayerHere;
    public List<Node> neighbours;
    public Node previous;
    public double gValue;
    public double hValue;
    public double fValue;
    public bool isScaredNode;

    public void Setup(int _xPosition, int _zPosition, bool _isWalkable, bool _isPlayerHere)
    {
        this.xPosition = _xPosition;
        this.zPosition = _zPosition;
        this.isWalkable = _isWalkable;
        this.isPlayerHere = _isPlayerHere;
        this.neighbours = new List<Node>();
        this.gValue = 0;
        this.hValue = 0;
        this.fValue = 0;
}
}
