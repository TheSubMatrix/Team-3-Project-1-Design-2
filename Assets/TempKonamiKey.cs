using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempKonamiKey : MonoBehaviour,IInteractable
{
    PlayerInteractionHandler playerInteractionHandler;
    public PlayerInteractionHandler interactionHandler { get => playerInteractionHandler; set => playerInteractionHandler = value; }
    public bool ShouldStopMovement { get => shouldMove; set => shouldMove = value; }
    private bool shouldMove = false;
    public void OnInteracting()
    {
        playerInteractionHandler.EndInteraction();
    }

    public void OnInteractStart(PlayerInteractionHandler incomingHandler)
    {
        playerInteractionHandler = incomingHandler;
        Debug.Log("Key");
    }

    public void OnInteractEnd()
    {
        if(SceneTransition.Instance != null)
        {
            SceneTransition.Instance.ChangeScene(1f, 1f, "To be Continued",null);
        }
    }
}
