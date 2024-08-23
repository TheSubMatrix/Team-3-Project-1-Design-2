using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteractionHandler : MonoBehaviour
{
    [Serializable]
    public class InteractionEvent : UnityEvent<GameObject> { }
    [Serializable]
    public class PickupEvent : UnityEvent<GameObject> { }
    [SerializeField]LayerMask interactableLayers;
    [SerializeField]IHoldable heldObject;
    [SerializeField]IInteractable interactingObject;
    [SerializeField] InteractionEvent InteractStarted;
    [SerializeField] InteractionEvent InteractEnded;
    [SerializeField] PickupEvent PickupStarted;
    [SerializeField] PickupEvent PickupEnded;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            const int raycastDistance = 5;
            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red, 1);
            Physics.Raycast(ray, out RaycastHit hitInfo, raycastDistance, interactableLayers, QueryTriggerInteraction.UseGlobal);
            if (heldObject == null && interactingObject == null)
            {
                IHoldable holdable = hitInfo.collider.gameObject.GetComponent<IHoldable>();
                IInteractable interactable = hitInfo.collider.gameObject.GetComponent<IInteractable>();
                if (holdable != null)
                {
                    StartPickup(holdable);
                }
                else if(interactable != null)
                {
                    StartIntreaction(interactable);
                }
            }
            if(interactingObject != null)
            {
                EndIntreaction();
            }
            if(heldObject != null)
            {
                EndPickup(hitInfo.collider.gameObject, heldObject);
            }

        }
        if(heldObject != null)
        {
            heldObject.OnHolding(transform.position + transform.forward * 2);
        }
    }
    public void EndIntreaction()
    {
        InteractEnded.Invoke(interactingObject.gameObject);
        interactingObject.OnInteractEnd();
        interactingObject = null;
    }
    public void StartPickup(IHoldable holdable)
    {
        PickupStarted.Invoke(holdable.gameObject);
        holdable.OnHoldStart();
        heldObject = holdable;
    } 
    public void StartIntreaction(IInteractable interactable)
    {
        InteractStarted.Invoke(interactable.gameObject);
        interactable.OnInteractStart();
        interactingObject = interactable;
    }
    public void EndPickup(GameObject objectForAttemptingPlace, IHoldable holdable)
    {
        if (objectForAttemptingPlace != null)
        {
            heldObject.OnHoldEnd(heldObject.gameObject);
        }
        else
        {
            heldObject.OnHoldEnd(null);
        }
        PickupEnded.Invoke(heldObject.gameObject);
        heldObject = null;
    }
}