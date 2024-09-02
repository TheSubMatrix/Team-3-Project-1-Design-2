using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour, IInteractable
{
   PlayerInteractionHandler myInteractionHandler;
   private  bool shouldStopMovement = false;

    private bool trunkIsOpen = false;
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
        Debug.Log("Open Trunk");
        ToggleTrunk();
        // OpenTrunk();

    }
    public void OnInteracting()
    {

       
    }

   public void OnInteractEnd()
    {
        Debug.Log("Closing");
        ToggleTrunk();
        //CloseTrunk();
    }


   /* public void OpenTrunk()
    {
        myAnimator.Play("Open");
        if (myAudioSource != null)
        {
            myAudioSource.PlayOneShot(trunkOpenSFX);
        }
        

    }
    public void CloseTrunk()
    {
        Debug.Log("Stop Interaction");
        myAnimator.Play("Close");
        if (myAudioSource != null)
        {
            myAudioSource.PlayOneShot(trunkCloseSFX);
        }
       
    }*/
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
        if(trunkIsOpen)
        {
            SoundManager.Instance.PlaySoundOnObject(gameObject, "Trunk Open", false);
        }
        else
        {
            SoundManager.Instance.PlaySoundOnObject(gameObject, "Trunk Close", false);
        }
    }

}
