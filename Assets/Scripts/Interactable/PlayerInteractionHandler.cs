using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour
{
    [SerializeField] LayerMask interactableLayers;
    IInteractable currentInteractable;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractable == null)
            {
                Ray ray = new Ray(transform.position, transform.forward);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, 5, interactableLayers, QueryTriggerInteraction.UseGlobal))
                {
                    IInteractable interactable = hitInfo.collider.gameObject.GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        interactable.OnInteractStart();
                        currentInteractable = interactable;
                    }
                }
            }
            else
            {
                currentInteractable.OnInteractEnd();
                currentInteractable= null;
            }

        }
    }
}
