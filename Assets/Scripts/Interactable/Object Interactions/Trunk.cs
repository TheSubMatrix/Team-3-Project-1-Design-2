using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour, IInteractable
{
   PlayerInteractionHandler myInteractionHandler;
   private  bool shouldStopMovement = false;
  
    
    private Animator myAnimator;
    private AudioSource myAudioSource;
    [SerializeField] AudioClip trunkOpenSFX;
    [SerializeField] AudioClip trunkCloseSFX;
    public bool ShouldStopMovement { get => shouldStopMovement; set => shouldStopMovement = value; }
   
    PlayerInteractionHandler IInteractable.interactionHandler { get => myInteractionHandler; set => myInteractionHandler = value; }


    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();
    }
   public void OnInteractStart(PlayerInteractionHandler incomingHandler)
    {

        
        myInteractionHandler = incomingHandler;
        Debug.Log("Open Trunk");
        OpenTrunk();

    }
    public void OnInteracting()
    {
       
       
    }

   public void OnInteractEnd()
    {
        CloseTrunk();
    }


    public void OpenTrunk()
    {
        myAnimator.Play("Open");
        myAudioSource.PlayOneShot(trunkOpenSFX);

    }
    public void CloseTrunk()
    {
        myAnimator.Play("Close");
        myAudioSource.PlayOneShot(trunkCloseSFX);
    }

}
