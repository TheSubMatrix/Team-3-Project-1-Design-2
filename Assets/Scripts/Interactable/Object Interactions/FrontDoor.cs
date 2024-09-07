using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class FrontDoor : MonoBehaviour, IInteractable, IUsesUI
{


    Animator animator;
    PlayerInteractionHandler currentInteractable;
    public PlayerInteractionHandler interactionHandler { get => currentInteractable; set => currentInteractable = value; }
    public bool ShouldStopMovement { get => shouldStopMovement; set => shouldStopMovement = value; }

    bool shouldStopMovement = false;
    bool showingPowerSwitchUI = false;
    BoxCollider doorCollider;

    [SerializeField] Image powerSwitchUIImage => PlayerUI?.GetComponentsInChildren<Image>().Where(o => o.gameObject.name == "gen").First();

    public bool switchEnabled = false;
   [SerializeField] public int boardCount { get;set; }
    public PlayerUI PlayerUI { get; set; }

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
        
        if(switchEnabled || boardCount >= 5)
        {
            /*if(switchEnabled)
            {
                playerUI.HidePlayerUI(powerSwitchUIImage, false, 0, 2);
            }*/
            doorCollider.enabled = true;
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
        if(boardCount >= 5 && switchEnabled)
        {
            animator.Play("Open");
        }
        else
        {
            Debug.Log("Need all boards down and switch enabled");
            SoundManager.Instance.PlaySoundAtLocation(transform.position, "Dialogue 4 ", false);

            if (!showingPowerSwitchUI && switchEnabled == false)
            {
                PlayerUI?.HidePlayerUI(powerSwitchUIImage, true, 0, 2);
                showingPowerSwitchUI = true;
            }
           
        }
       
    }
    public void UpdateSwitchState(bool newSwitchState) 
    {
        switchEnabled = newSwitchState;
    }
}
