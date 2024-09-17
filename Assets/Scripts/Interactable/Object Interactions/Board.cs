using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;
public class Board : MonoBehaviour, IInteractable
{
    PlayerInteractionHandler myInteractionHandler;
    public PlayerInteractionHandler interactionHandler { get => myInteractionHandler; set => myInteractionHandler = value; }

    private Animator animator;  

    [SerializeField] GameObject dustVFX;
    public bool ShouldStopMovement { get => stopMovement; set => stopMovement = value; }

    
    private bool stopMovement = false;

    [SerializeField] FrontDoor frontDoorRef;
   
    [SerializeField] UnityEvent checkBoardCountEvent = new UnityEvent();

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    
    public void OnInteractStart(PlayerInteractionHandler playerInteractionHandler)
    {
        myInteractionHandler = playerInteractionHandler;

        if (playerInteractionHandler.heldObject != null)
        {
            if (playerInteractionHandler.heldObject.gameObject.name == "CrowBar")
            {               
                BoardsFall(gameObject.name);
               
                checkBoardCountEvent.Invoke();
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
        Destroy(gameObject);      
    }

    void BoardsFall(string boardName)  ///Depending on which board is interacted with will play it's respective animation
    {
        Debug.Log("This boards name is" +  boardName);
        switch(boardName)
        {
            case "DoorPlank5":
                animator.Play("Board_5");
                break;
            case "DoorPlank4":
                animator.Play("Board_4");
                break;
            case "DoorPlank3":
                animator.Play("Board_3");
                break;
            case "DoorPlank2":
                animator.Play("Board_2");
                break;
            case "DoorPlank1":
                animator.Play("Board_1");
                break;
            default:
                Debug.Log("No board animation");
                break;
        }       

    }


    public void ShowDust()
    {
        Debug.Log("Show dust");

        Instantiate(dustVFX, transform.position, transform.rotation);
   
        SoundManager.Instance.PlaySoundAtLocation(transform.position, "Board Fall", false);
    }


}

