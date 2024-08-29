using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardInteractable : MonoBehaviour, IInteractable
{
    PlayerInteractionHandler m_interactionHandler;

    bool myShouldStopMovement;
    bool myHasAnimations;
    GameObject m_gameObject  { get; }
    public PlayerInteractionHandler interactionHandler { get; set ; }
    public bool ShouldStopMovement { get => myShouldStopMovement; set => myShouldStopMovement = value; }
    

    public void OnInteracting()
    {
        
    }
    public void OnIneractStart(PlayerInteractionHandler interactionHandler)
    {
        m_interactionHandler = interactionHandler;
        Debug.Log("ON Interact");
        
    }
    public void OnInteractEnd()
    {

    }
}
