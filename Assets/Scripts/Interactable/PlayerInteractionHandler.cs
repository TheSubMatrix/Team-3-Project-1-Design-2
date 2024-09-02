using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteractionHandler : MonoBehaviour
{

    [Serializable]
    public class InteractionEvent : UnityEvent<GameObject> { }
    [Serializable]
    public class PickupEvent : UnityEvent<GameObject> { }
    [SerializeField]
    UnityEvent StopPlayerMovement;
    [SerializeField]
    UnityEvent RestartPlayerMovement;
    [SerializeField] LayerMask interactableLayers;
    [SerializeField] public IHoldable heldObject;
    [SerializeField] public IInteractable interactingObject;
    [SerializeField] InteractionEvent InteractStarted;
    [SerializeField] InteractionEvent InteractEnded;

    [SerializeField] PickupEvent PickupStarted;
    [SerializeField] PickupEvent PickupEnded;
    

    private IHoldable holdable;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            const int raycastDistance = 5;
            Ray ray = new Ray(transform.position, transform.forward);
            Physics.Raycast(ray, out RaycastHit hitInfo, raycastDistance, interactableLayers, QueryTriggerInteraction.UseGlobal);
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1);
            
            if(interactingObject!= null)   //I moved this up here so that way it will check to EndIntreaction and if it does
            {                             // it will return not triggering the rest. Also check spelling on EndIntreaction for future use
                Debug.Log("End Interaction");
                EndIntreaction();
                return;
            }
            
            
            if ((heldObject == null || interactingObject == null) && hitInfo.collider != null)  // If the && is replaced with || then by pressing E it wont EndInteraction
            {
                holdable = hitInfo.collider.gameObject.GetComponent<IHoldable>();
                //IHoldable holdable = hitInfo.collider.gameObject.GetComponent<IHoldable>();
                IInteractable interactable = hitInfo.collider.gameObject.GetComponent<IInteractable>();
                
                if (holdable != null)
                {
                    StartPickup(holdable);
                }
                else if (interactable != null)
                {
                    Debug.Log("here");
                    StartIntreaction(interactable);
                }
            }
            /*else if (interactingObject != null)
            {
                Debug.Log("End Interaction");
                EndIntreaction();
            }*/

            


        }
        if(heldObject != null && Input.GetKeyDown(KeyCode.Q))
        {
            const int raycastDistance = 5;
            Ray ray = new Ray(transform.position, transform.forward);
            Physics.Raycast(ray, out RaycastHit hitInfo, raycastDistance, interactableLayers, QueryTriggerInteraction.UseGlobal);
            Debug.Log("Drop");
            if (hitInfo.collider != null)
            {
                EndPickup(hitInfo.collider.gameObject);
            }
            else
            {
                EndPickup(null);
            }
        }
        if (heldObject != null)
        {
            heldObject.OnHolding
            (
                transform.position + 
                (transform.forward * heldObject.HoldPositionOffset.z) +
                (transform.right * heldObject.HoldPositionOffset.x) +
                (transform.up * heldObject.HoldPositionOffset.y),
                transform.rotation * heldObject.HoldRotationOffset
            );

        }
        if (interactingObject != null)
        {
            WhileInteracting(interactingObject);
        }
    }
    /// <summary>
    /// Ends the current ongoing interaction
    /// </summary>
    public void EndIntreaction()
    {
        if (interactingObject.ShouldStopMovement)
        {
            RestartPlayerMovement.Invoke();
        }
        InteractEnded.Invoke(interactingObject.gameObject);
        Debug.Log("End Interaction");
        interactingObject.OnInteractEnd();
        interactingObject = null;
    }
    /// <summary>
    /// Starts to pickup a given holdable
    /// </summary>
    /// <param name="holdable">The holdable to pickup</param>
    public void StartPickup(IHoldable holdable)
    {
        PickupStarted.Invoke(holdable.gameObject);
        holdable.OnHoldStart();
        heldObject = holdable;
    }
    /// <summary>
    /// Starts an interaction with a given interactable
    /// </summary>
    /// <param name="interactable">A reference to the holdable object the pickup interaction will be ended with</param>
    public void StartIntreaction(IInteractable interactable)
    {
        interactingObject = interactable;
        Debug.Log("Start Interaction");
        InteractStarted.Invoke(interactable.gameObject);
        if (interactable.ShouldStopMovement)
        {
            StopPlayerMovement.Invoke();
        }


        interactable.OnInteractStart(this);
    }
    /// <summary>
    /// Ends the pickup interaction with the current pickup
    /// </summary>
    /// <param name="objectForAttemptingPlace">the gameobject the user was aiming at when the object was dropped. Used for placement of objects for puzzles</param>
    public void EndPickup(GameObject objectForAttemptingPlace)
    {
        heldObject.OnHoldEnd(objectForAttemptingPlace);
        PickupEnded.Invoke(heldObject.gameObject);
        heldObject = null;
    }
    /// <summary>
    /// Updates an interactable
    /// </summary>
    /// <param name="interactable">The interactable to update</param>
    public void WhileInteracting(IInteractable interactable)
    {

        interactable.OnInteracting();
    }
}