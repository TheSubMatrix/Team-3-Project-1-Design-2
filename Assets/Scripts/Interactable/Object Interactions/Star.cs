using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour, IInteractable
{
    private bool stopPlayerMovement = false;

    private PlayerInteractionHandler myPlayerInteractionHandler;
    public PlayerInteractionHandler interactionHandler { get => myPlayerInteractionHandler; set => myPlayerInteractionHandler = value; }
    public bool ShouldStopMovement { get =>stopPlayerMovement; set =>   stopPlayerMovement = value; }

    public void OnInteractStart(PlayerInteractionHandler playerInteractionHandler)
    {
        myPlayerInteractionHandler = playerInteractionHandler;
        
        Debug.Log("Touch");
    }



    public void OnInteracting()
    {
        myPlayerInteractionHandler.EndInteraction();
    }

    public void OnInteractEnd()
    {
        Debug.Log("Fall");
    }
}
