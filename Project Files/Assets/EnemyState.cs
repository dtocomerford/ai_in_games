using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public enum State
    {
        Idle,
        Chase,
        Scatter,
        Scared
    }

    public State currentState;


    /*
    // Start is called before the first frame update
    public void Start()
    {
        currentState = State.Idle;
    }
    */


    //Function which takes in a state and the command to transition from current state to new state
    public void StateTransition(State newState)
    {
        this.currentState = newState;
    }


}
