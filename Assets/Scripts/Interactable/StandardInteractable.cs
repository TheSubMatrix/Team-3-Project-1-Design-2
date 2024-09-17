using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardInteractable : MonoBehaviour, IInteractable
{
    PlayerInteractionHandler m_interactionHandler;

   [SerializeField] bool myShouldStopMovement;
   [SerializeField] bool myHasAnimations;
    GameObject m_gameObject  { get; }
    public PlayerInteractionHandler interactionHandler { get => m_interactionHandler; set => m_interactionHandler = value ; }
    public bool ShouldStopMovement { get => myShouldStopMovement; set => myShouldStopMovement = value; }
    Animator animator;
    private void Awake()
    {
        if (myHasAnimations)
        {
           animator = gameObject.AddComponent<Animator>();
        }
    }

    public void OnIneractStart(PlayerInteractionHandler interactionHandler)
    {
        m_interactionHandler = interactionHandler;
        Debug.Log("ON Interact");
        if (animator)
        {

        }

    }
    public void OnInteracting()
    {
        m_interactionHandler.EndInteraction();
    }
   
    public void OnInteractEnd()
    {
        Debug.Log("End of interaction");
    }
}
