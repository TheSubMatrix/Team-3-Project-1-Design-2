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
    bool stayOn = false;
    public PlayerInteractionHandler interactionHandler { get => currentInteractor; set => currentInteractor = value; }
    public bool ShouldStopMovement { get => shouldStopMovement; set => shouldStopMovement = value; }

    [SerializeField] GameObject sparksVFX;

    [SerializeField] List<Transform> sparkSpawnPoints = new List<Transform>();

    [SerializeField]  Animator animator;
    

    public void OnInteracting()
    {
        
        
        Debug.Log("Switch Powered: " + switchIsPowered);
       
        interactionHandler.EndInteraction();

       
    }
    public void OnInteractStart(PlayerInteractionHandler incomingHandler)
    {
        Debug.Log("Interact Start");
        currentInteractor = incomingHandler;
        StartCoroutine(StartSparks());
        
        //Instantiate(sparksVFX, transform.position, Quaternion.identity);                              
    }

    public void OnInteractEnd()
    {
        if(!stayOn)
        {
            switchIsPowered = !switchIsPowered;
            Debug.Log("Switch Powered: " + switchIsPowered);
            switchStateChangedEvent.Invoke(switchIsPowered);
            Debug.Log("Interact End");
        }
        

    }

    public void ToggleSwitch() ///Using your event if switchIsPowered is true the on animation will play, inverse if false
    {
        bool switchOn = switchIsPowered;
        if (switchOn)
        {
            animator.Play("Turn On");
            stayOn = true;
        }
        else
        {
            animator.Play("Turn Off");
        }
    }


    IEnumerator StartSparks()
    {
        for (int i = 0; i <= sparkSpawnPoints.Count-1; i++)
        {
            Instantiate(sparksVFX, sparkSpawnPoints[i].position, Quaternion.identity);
            yield return new WaitForSeconds(1f);

        }
      

    }
}
