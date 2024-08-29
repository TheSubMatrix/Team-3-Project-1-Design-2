using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    
   
    [SerializeField]  Animator animator;
    

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
            
            
        }
    }

    public void OnInteractEnd()
    {
        switchIsPowered = !switchIsPowered;
        Debug.Log("Switch Powered: " + switchIsPowered);
        switchStateChangedEvent.Invoke(switchIsPowered);

    }

    public void ToggleSwitch() ///Using your event if switchIsPowered is true the on animation will play, inverse if false
    {
        bool switchOn = switchIsPowered;
        if (switchOn)
        {
            animator.Play("Turn On");
        }
        else
        {
            animator.Play("Turn Off");
        }
    }
}
