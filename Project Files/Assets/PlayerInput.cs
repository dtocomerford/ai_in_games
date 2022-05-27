using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    public Vector3 ReturnDirection()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            return Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            return Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            return Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return Vector3.left;
        }

        return Vector3.zero;
       
    }
}
