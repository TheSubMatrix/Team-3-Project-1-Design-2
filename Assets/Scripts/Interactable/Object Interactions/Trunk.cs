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
   
    PlayerInteractionHandler IInteractable.interactionHandler { get => myInteractionHandler; set => myInteractionHandler = value; }


    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        //myAudioSource = GetComponent<AudioSource>();
    }
   public void OnInteractStart(PlayerInteractionHandler incomingHandler)
    {

        
        myInteractionHandler = incomingHandler;
        
        
       
        

    }
    public void OnInteracting()
    {
       myInteractionHandler.EndIntreaction();
       
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
