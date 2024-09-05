using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour, IInteractable
{
    PlayerInteractionHandler myInteractionHandler;
    public PlayerInteractionHandler interactionHandler { get => myInteractionHandler; set => myInteractionHandler = value; }

    Animator animator;
    
    public bool ShouldStopMovement { get => stopMovement; set => stopMovement = value; }

    private bool stopMovement = false;

   [SerializeField] FrontDoor frontDoorRef;

   
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void OnInteractStart(PlayerInteractionHandler playerInteractionHandler)
    {
        myInteractionHandler = playerInteractionHandler;
       
        if(playerInteractionHandler.heldObject != null)
        {
            if (playerInteractionHandler.heldObject.gameObject.name == "CrowBar")
            {
                BoardsFall(gameObject.name);
            }
        }
        else
        {
            Debug.Log("I need a crowbar");
        }
        
        
       
        
        
        
        
        
        
        myInteractionHandler.interactingObject = null;
    }
    
    public void OnInteracting()
    {
        myInteractionHandler.EndInteraction();
    }
    

    public void OnInteractEnd()
    {
        frontDoorRef.boardCount++;
        Debug.Log(frontDoorRef.boardCount);
        Destroy(gameObject);
        
        //BoardsFall(gameObject.name);
    }

    void BoardsFall(string boardName)  ///Depending on which board is interacted with will play it's respective animation
    {
        if(boardName == "DoorPlank5")
        {
            animator.Play("Board_5");
        }
         if(boardName == "DoorPlank4")
        {
            animator.Play("Board_4");
        }
         if (boardName == "DoorPlank3")
        {
            animator.Play("Board_3");
        }
         if (boardName == "DoorPlank2")
        {
            animator.Play("Board_2");
        }
         if (boardName == "DoorPlank1")
        {
            animator.Play("Board_1");
        }

    }

}
