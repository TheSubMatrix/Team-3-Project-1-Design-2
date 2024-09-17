using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour, IInteractable
{
   PlayerInteractionHandler myInteractionHandler;
   private  bool shouldStopMovement = false;

    private bool trunkIsOpen;
    private Animator myAnimator;
    //private AudioSource myAudioSource;
    [SerializeField] AudioClip trunkOpenSFX;
    [SerializeField] AudioClip trunkCloseSFX;
    public bool ShouldStopMovement { get => shouldStopMovement; set => shouldStopMovement = value; }
    private bool firstTimePickup = true;
    PlayerInteractionHandler IInteractable.interactionHandler { get => myInteractionHandler; set => myInteractionHandler = value; }


    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        //myAudioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        SoundManager.Instance.PlaySoundAtLocation(transform.position,"Car Door Shut",false);
    }
    public void OnInteractStart(PlayerInteractionHandler incomingHandler)
    {

        
        myInteractionHandler = incomingHandler;
        if(firstTimePickup)
        {
            SoundManager.Instance.PlaySoundAtLocation(transform.position,"Found Crowbar",false );
            firstTimePickup= false;
        }
        
       
        

    }
    public void OnInteracting()
    {
       myInteractionHandler.EndInteraction();
       
    }

   public void OnInteractEnd()
    {

        ToggleTrunk();
        Debug.Log("Trunk open is " + trunkIsOpen);
    }


   
    private void ToggleTrunk()
    {
        if (!trunkIsOpen)
        {
            trunkIsOpen = !trunkIsOpen;
            myAnimator.Play("Open");
        }
        else
        {
            trunkIsOpen = !trunkIsOpen;
            myAnimator.Play("Close");
        }
    }

    public void PlayTrunkSoundSFX()
    {
        if(SoundManager.Instance != null)
        {
            if (trunkIsOpen)
            {
                SoundManager.Instance.PlaySoundOnObject(gameObject, "Trunk Open", false);
            }
            else
            {
                SoundManager.Instance.PlaySoundOnObject(gameObject, "Trunk Close", false);
            }
        }
        
    }

}
