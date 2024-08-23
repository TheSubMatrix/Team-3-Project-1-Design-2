using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardInteractable : MonoBehaviour, IInteractable
{
    PlayerInteractionHandler m_interactionHandler;
    public PlayerInteractionHandler interactionHandler { get => m_interactionHandler; set => m_interactionHandler = value; }
    public void OnInteracting()
    {
        
    }
}
