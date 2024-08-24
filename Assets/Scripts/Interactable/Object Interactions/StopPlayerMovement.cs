using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovementScript = new PlayerMovement();

    

    public void StopMovement()
    {
        playerMovementScript.MovementStopped();       
    }
}
