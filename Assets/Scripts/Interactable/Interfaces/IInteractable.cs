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
            Debug.Log(gameObject.name);

            if (gameObject.GetComponent<Animator>())
            {
                Debug.Log("Has an animator");
            }

            interactionHandler = incomingHandler;

        }
    }
    void OnInteracting();
    void OnInteractEnd()
    {
        interactionHandler = null;
    }
}
