using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    GameObject gameObject { get; }
    PlayerInteractionHandler interactionHandler { get; set; }

    //ObjectAnimations animations { get; set; }
    void OnInteractStart(PlayerInteractionHandler incomingHandler)
    {
        {
           // Debug.Log(gameObject.name);

            

            interactionHandler = incomingHandler;

        }
    }
    void OnInteracting();
    void OnInteractEnd()
    {
        interactionHandler = null;
    }
}
