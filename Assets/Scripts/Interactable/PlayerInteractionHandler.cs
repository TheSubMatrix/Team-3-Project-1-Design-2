using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour
{
    [SerializeField] LayerMask interactableLayers;
    [SerializeField]IHoldable heldObject;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                const int raycastDistance = 5;
                Ray ray = new Ray(transform.position, transform.forward);
                Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red, 1);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, raycastDistance, interactableLayers, QueryTriggerInteraction.UseGlobal))
                {
                    IHoldable holdable = hitInfo.collider.gameObject.GetComponent<IHoldable>();
                    if (holdable != null)
                    {
                        holdable.OnHoldStart();
                        heldObject = holdable;
                    }
                }
            }
            else
            {
                Ray ray = new Ray(transform.position, transform.forward);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, 5, interactableLayers, QueryTriggerInteraction.UseGlobal))
                {
                    heldObject.OnHoldEnd(hitInfo.collider.gameObject);
                }
                else
                {
                    heldObject.OnHoldEnd(null);
                }
                heldObject = null;
            }

        }
        if(heldObject != null)
        {
            heldObject.OnHolding(transform.position + transform.forward * 2);
        }
    }
}
