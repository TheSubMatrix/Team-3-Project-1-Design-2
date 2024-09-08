using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PaperInteracable : MonoBehaviour, IInteractable
{
    [SerializeField] Texture2D imageForUI;
    [SerializeField] UnityEvent<GameObject, Texture2D> OnPaperInteracted;
    public PlayerInteractionHandler interactionHandler { get; set; }
    public bool ShouldStopMovement { get; set; }
    public void OnInteractStart(PlayerInteractionHandler incomingHandler)
    {
        interactionHandler = incomingHandler;
        Debug.Log("Paper Picked Up");
        OnPaperInteracted?.Invoke(gameObject, imageForUI);
        interactionHandler.EndInteraction();
        Destroy(gameObject);
    }
    public void OnInteracting()
    {

    }
}
