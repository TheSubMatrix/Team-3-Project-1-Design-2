using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class FrontDoor : MonoBehaviour, IInteractable
{

    [SerializeField] SO_ImageDisplayChannel uiPopupChannel;
    Animator animator;
    PlayerInteractionHandler currentInteractable;
    public PlayerInteractionHandler interactionHandler { get => currentInteractable; set => currentInteractable = value; }
    public bool ShouldStopMovement { get => shouldStopMovement; set => shouldStopMovement = value; }

    
    bool shouldStopMovement = false;
    bool showingPowerSwitchUI = false;
    BoxCollider doorCollider;

    public bool switchEnabled = false;
    public int boardCount = 0;
    public bool keyInHand = false;
    public PlayerUI PlayerUI { get; set; }

    [SerializeField] UnityEvent useKeyAnimationEvent = new UnityEvent();
    private void Awake()
    {
        animator = GetComponent<Animator>();
        doorCollider = GetComponent<BoxCollider>();
        
    }

    private void Start()
    {
        doorCollider.enabled = false;
    }
    private void Update()
    {
        if(switchEnabled || boardCount == 5)
        {
            
            doorCollider.enabled = true;
        }
        if(switchEnabled && showingPowerSwitchUI)
        {
            uiPopupChannel.OnFadeImage.Invoke(new SO_ImageDisplayChannel.ImageDisplayInfo("Turn On Generator", 1, 0, 0.5f, 0));
        }
    }
    public void OnInteracting()
    {
       interactionHandler.EndInteraction();
    }

   public void OnInteractStart( PlayerInteractionHandler incomingInteractionHandler)
    {
        currentInteractable = incomingInteractionHandler;
        
      


    }

    public void OnInteractEnd()
    {
       
        if(boardCount >= 5  && keyInHand)
        {
            useKeyAnimationEvent.Invoke();
            animator.Play("Open");
        }
        else
        {
            Debug.Log("Need all boards down and switch enabled");
            SoundManager.Instance.PlaySoundAtLocation(transform.position, "Dialogue 4", false);

            /*if (!showingPowerSwitchUI && switchEnabled == false)
            {
                uiPopupChannel.OnFadeImage.Invoke(new SO_ImageDisplayChannel.ImageDisplayInfo("Turn On Generator", 0, 1, 0.5f, 0));
               // uiPopupChannel.OnFadeImage.Invoke(new SO_ImageDisplayChannel.ImageDisplayInfo("Turn On Generator", 1, 0, 0.5f, 5));
                showingPowerSwitchUI = true;
            }*/
           
        }
       
    }

    public void CheckIfHasKey(bool hasKey)
    {
        keyInHand = hasKey;
    }
    public void UpdateSwitchState(bool newSwitchState) 
    {
        switchEnabled = newSwitchState;
    }

    public void UpdateBoardCount(int incomingBoardCount)
    {
        boardCount += incomingBoardCount;
    }

    
}
