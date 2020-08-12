using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour {

    public Transform target; //Initate target
    public Vector3 offset; //Keep track of the target

    void Update () { //Update function

        if (target == null) //If the target doesn't exist
            return;         //Return nothing.

        transform.position = target.position + offset;  //Update the current position with the target position plus the offset.
    }
   
}
