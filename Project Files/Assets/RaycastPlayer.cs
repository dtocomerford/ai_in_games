using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPlayer : MonoBehaviour
{

    public Vector3 direction;
    private void OnDrawGizmos()
    {
        RaycastHit hit;

        bool isHit = Physics.BoxCast(this.transform.position, this.transform.lossyScale / 2, direction, out hit, this.transform.rotation, 0.5f);
        //Gizmos.DrawWireCube(transform.position + transform.forward * 1, transform.lossyScale);


        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, direction * hit.distance);
            Gizmos.DrawWireCube(transform.position + direction * hit.distance, transform.lossyScale);

        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward * 1);

        }
    }

}
