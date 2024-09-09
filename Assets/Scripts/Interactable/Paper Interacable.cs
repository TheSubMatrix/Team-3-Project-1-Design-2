using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PaperInteracable : MonoBehaviour, IInteractable
{
    [SerializeField] SO_ImageDisplayChannel.ImageDisplayInfo imageDisplayInfoEnable;
    [SerializeField] SO_ImageDisplayChannel.ImageDisplayInfo imageDisplayInfoDisable;
    [SerializeField] SO_ImageDisplayChannel imageDisplayInfoShow;
    public PlayerInteractionHandler interactionHandler { get; set; }
    public bool ShouldStopMovement { get; set; } = true;
    public void OnInteractStart(PlayerInteractionHandler incomingHandler)
    {
        interactionHandler = incomingHandler;
        Debug.Log("Paper Picked Up");
        imageDisplayInfoShow.OnFadeImage?.Invoke(imageDisplayInfoEnable);
    }
    public void OnInteracting()
    {

    }
    public void OnInteractEnd()
    {
        imageDisplayInfoShow.OnFadeImage?.Invoke(imageDisplayInfoDisable);
        Destroy(gameObject);
    }

}
