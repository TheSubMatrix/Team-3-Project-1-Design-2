using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoor : MonoBehaviour, IInteractable
{
    Animator animator;
    PlayerInteractionHandler currentInteractable;
    public PlayerInteractionHandler interactionHandler { get => currentInteractable; set => currentInteractable = value; }
    public bool ShouldStopMovement { get => shouldStopMovement; set => shouldStopMovement = value; }

    bool shouldStopMovement = false;
    

    public bool switchEnabled = false;
   [SerializeField] public int boardCount { get;set; }

    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        
    }
    public void OnInteracting()
    {
       interactionHandler.EndIntreaction();
    }

   public void OnInteractStart( PlayerInteractionHandler incomingInteractionHandler)
    {
        currentInteractable = incomingInteractionHandler;

        
    }

    public void OnInteractEnd()
    {
        if(boardCount >= 5 && switchEnabled)
        {
            animator.Play("Open");
        }
        else
        {
            Debug.Log("Need all boards down and switch enabled");
        }
       
    }
    public void UpdateSwitchState(bool newSwitchState) 
    {
        switchEnabled = newSwitchState;
    }
}
