using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallenTree : MonoBehaviour, IInteractable
{
    public PlayerInteractionHandler interactionHandler { get; set; }
    public bool ShouldStopMovement { get; set; } = false;
    [SerializeField] string objectToCheckForPass;

    public void OnInteracting()
    {

    }
    public void OnInteractStart(PlayerInteractionHandler incomingHandler)
    {
        interactionHandler = incomingHandler;
        if(interactionHandler.heldObject.gameObject.name == objectToCheckForPass) 
        {
            Destroy(gameObject);
            Debug.Log("Destroy");
            interactionHandler?.EndInteraction();
        }
        else 
        {
            Debug.Log(interactionHandler.heldObject.gameObject.name);
            interactionHandler?.EndInteraction();
        }
    }
}
