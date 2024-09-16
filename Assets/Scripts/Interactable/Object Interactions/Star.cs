using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class Star : MonoBehaviour, IInteractable
{
    private bool stopPlayerMovement = false;

    private PlayerInteractionHandler myPlayerInteractionHandler;
    public PlayerInteractionHandler interactionHandler { get => myPlayerInteractionHandler; set => myPlayerInteractionHandler = value; }
    public bool ShouldStopMovement { get =>stopPlayerMovement; set =>   stopPlayerMovement = value; }

    [SerializeField] UnityEvent onPipeAnimationPlay = new UnityEvent();

    [SerializeField] GameObject key;
    public void OnInteractStart(PlayerInteractionHandler playerInteractionHandler)
    {       
        myPlayerInteractionHandler = playerInteractionHandler;

        if(myPlayerInteractionHandler.heldObject != null)
        {
            if (myPlayerInteractionHandler.heldObject.gameObject.name == "Metal Pipe")
            {
                onPipeAnimationPlay.Invoke();
                Debug.Log("Swing");
            }
        }
        else
        {
            Debug.Log("Need something to knock it down");
        }
      
    }

    public void OnInteractEnd()
    {
        Debug.Log("Fall");
        key.SetActive(true);
    }

    public void OnInteracting()
    {
        myPlayerInteractionHandler.EndInteraction();
    }
}
