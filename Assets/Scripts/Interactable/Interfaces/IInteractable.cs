using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    GameObject gameObject { get; }
    PlayerInteractionHandler interactionHandler { get; set; }
    void OnInteractStart(PlayerInteractionHandler incomingHandler)
    {
        {
            interactionHandler = incomingHandler;
        }
    }
    void OnInteracting();
    void OnInteractEnd()
    {
        interactionHandler = null;
    }
}
