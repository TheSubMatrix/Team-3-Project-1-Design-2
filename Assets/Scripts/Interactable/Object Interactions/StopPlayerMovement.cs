using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovementScript;

    public void ChangePlayerMovement()
    {
        //playerMovementScript.MovementStopped();
       if( playerMovementScript.movementStoped == false)
        {
            playerMovementScript.movementStoped = true;
        }
        else
        {
            playerMovementScript.movementStoped = false; 
        }
        
    }


}
