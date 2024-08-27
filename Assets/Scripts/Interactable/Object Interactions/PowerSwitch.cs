using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerSwitch : MonoBehaviour, IInteractable
{
    PlayerInteractionHandler currentInteractor;
    [System.Serializable]
    public class SwitchStateChanged : UnityEvent<bool> { }

    [SerializeField] SwitchStateChanged switchStateChangedEvent;
    bool shouldStopMovement = false;
    bool switchIsPowered = false;
    public PlayerInteractionHandler interactionHandler { get => currentInteractor; set => currentInteractor = value; }
    public bool ShouldStopMovement { get => shouldStopMovement; set => shouldStopMovement = value; }

    public void OnInteracting()
    {

    }
    public void OnInteractStart(PlayerInteractionHandler incomingHandler)
    {
        {
            currentInteractor = incomingHandler;
            switchIsPowered = !switchIsPowered;
            Debug.Log("Switch Powered: " + switchIsPowered);
            switchStateChangedEvent.Invoke(switchIsPowered);
            interactionHandler.EndIntreaction();
        }
    }
}
