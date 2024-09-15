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
    bool generatorOn;
    public PlayerInteractionHandler interactionHandler { get => currentInteractor; set => currentInteractor = value; }
    public bool ShouldStopMovement { get => shouldStopMovement; set => shouldStopMovement = value; }

    [SerializeField] GameObject sparksVFX;

    [SerializeField] List<Transform> sparkSpawnPoints = new List<Transform>();

    [SerializeField]  Animator animator;

    [SerializeField] Transform switchLocation;

    public void OnInteracting()
    {
        
        
        Debug.Log("Switch Powered: " + switchIsPowered);
        
            interactionHandler.EndInteraction();
        
        

       
    }
    public void OnInteractStart(PlayerInteractionHandler incomingHandler)
    {
        Debug.Log("Interact Start");
        currentInteractor = incomingHandler;
        if(!switchIsPowered)
        {
            StartCoroutine(StartSparks());
        }
        
        
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


    void Update()
    {
        if (switchIsPowered && !generatorOn )
        {
            StartCoroutine(GeneratorStart());
            generatorOn = true;
        }
    }
    public void ToggleSwitch() ///Using your event if switchIsPowered is true the on animation will play, inverse if false
    {
        bool switchOn = switchIsPowered;
        if (switchOn)
        {
            animator.Play("Turn On");
            SoundManager.Instance.PlaySoundAtLocation(switchLocation.position, "SwitchPull", false);
            stayOn = true;
        }
        /*else
        {
            animator.Play("Turn Off");
        }*/
    }

    private string SparkSoundRandomizer(int sparkIndex)
    {
        string sparkString = null;
        switch(sparkIndex)
        {
            case 0:
                sparkString = "Spark1";
                break;
            case 1:
                sparkString = "Spark2";
                break;
            case 2:
                sparkString = "Spark3";
                break;
            case 3:
                sparkString = "Spark4";
                break;
        }
        return sparkString;
    }

    IEnumerator StartSparks()
    {
        for (int i = 0; i <= sparkSpawnPoints.Count-1; i++)
        {
            Instantiate(sparksVFX, sparkSpawnPoints[i].position, Quaternion.identity);
            SoundManager.Instance.PlaySoundAtLocation(sparkSpawnPoints[i].position, SparkSoundRandomizer(Random.Range(0,4)),false);
            yield return new WaitForSeconds(.4f);

        }
      

    }

    IEnumerator GeneratorStart()
    {
        yield return new WaitForSeconds(5f);
        SoundManager.Instance.PlaySoundOnObject(gameObject, "GeneratorOn", true);
    }
}
