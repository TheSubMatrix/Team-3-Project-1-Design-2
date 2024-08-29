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
   
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void OnInteractStart(PlayerInteractionHandler playerInteractionHandler)
    {
        myInteractionHandler = playerInteractionHandler;
        Debug.Log(gameObject.name);
        Debug.Log("Start animation");
        BoardsFall(gameObject.name);
        OnInteractEnd();
       
    }
    
    public void OnInteracting()
    {

    }
    

    public void OnInteractEnd()
    {
        Debug.Log("Doom");
        Debug.Log("Delete board");
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
