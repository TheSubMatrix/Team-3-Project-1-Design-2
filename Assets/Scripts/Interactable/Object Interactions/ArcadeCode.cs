using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ArcadeCode : MonoBehaviour, IInteractable
{
    [SerializeField]UnityEvent OnCodeCompleted = new UnityEvent();
    [SerializeField]UnityEvent SwitchCameraView = new UnityEvent(); 
    PlayerInteractionHandler playerInteractionHandler;
    public PlayerInteractionHandler interactionHandler { get => playerInteractionHandler; set => playerInteractionHandler = value; }

    [SerializeField] SO_ImageDisplayChannel uiPopupChannel;
    public bool ShouldStopMovement { get; set; } = true;
    private Animator animator;
    [SerializeField] Animator hiddenDoorAnimator;

    private CinemachineVirtualCamera arcadeCameraView;
    private void Awake()
    {
        if(GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }
        else
        {
            Debug.LogWarning("No animator found");
        }
        if(GetComponentInChildren<CinemachineVirtualCamera>() != null)
        {
            arcadeCameraView = GetComponentInChildren<CinemachineVirtualCamera>();
           arcadeCameraView.enabled = false;
            
        }
        else
        {
            Debug.LogWarning("There is no virtual camera attached to Arcade Controls");
        }
    }

    public void OnInteractStart(PlayerInteractionHandler incomingInteractionHandler)
    {
        playerInteractionHandler = incomingInteractionHandler;
        SwitchCameraView.Invoke();

    }
   
    [SerializeField] char[] code = { 'w', 'w', 's', 's', 'a', 'd', 'a', 'd', '0', '1' };
    char[] typedChars = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
    public void OnInteracting()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Up");
            UpdateChars('w');
            animator.SetTrigger("Up_Joystick");

            StartCoroutine(ResetTrigger());
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            UpdateChars('a');
            animator.SetTrigger("Left_Joystick");
            StartCoroutine(ResetTrigger());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            UpdateChars('s');
            animator.SetTrigger("Down_Joystick");
            StartCoroutine(ResetTrigger());
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            UpdateChars('d');
            animator.SetTrigger("Right_Joystick");
            StartCoroutine(ResetTrigger());
        }
        if (Input.GetMouseButtonDown(0))
        {
            UpdateChars('0');
            animator.SetTrigger("Push_Button1");
            StartCoroutine(ResetTrigger());
        }
        if (Input.GetMouseButtonDown(1))
        {
            UpdateChars('1');
            animator.SetTrigger("Push_Button2");
            StartCoroutine(ResetTrigger());
        }
    }

    public void OnInteractEnd()
    {
        SwitchCameraView.Invoke();
    }
    void CheckCode()
    {
        if (typedChars.SequenceEqual(code))
        {
            OnCodeCompleted.Invoke();          
            uiPopupChannel.OnFadeImage.Invoke(new SO_ImageDisplayChannel.ImageDisplayInfo("Enter Code", 1, 0, 0.5f, 0));
            uiPopupChannel.OnFadeImage.Invoke(new SO_ImageDisplayChannel.ImageDisplayInfo("Check Secret Door", 0, 1, 0.5f, 0));
            Debug.Log("Code Accepted");
        }
    }
    void UpdateChars(char incomingChar)
    {
        for (int i = 0; i < typedChars.Length - 1; i++)
        {
            typedChars[i] = typedChars[i+1];
        }
        typedChars[typedChars.Length - 1] = incomingChar;

        string sequence = string.Empty;
        for(int i = 0; i < typedChars.Length; i++)
        {
            sequence += typedChars[i];
        }
        Debug.Log(sequence);

        CheckCode();
    }


    public void OpenHiddenDoor()
    {
        hiddenDoorAnimator.Play("Open_Door");
    }


    IEnumerator ResetTrigger()
    {
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Finish");
        
        Debug.Log("Trigger Reseted");
    }

    public void ArcadeInteractionCamera()
    {
        if (!arcadeCameraView.enabled)
        {
            arcadeCameraView.enabled = true;
        }
        else if (arcadeCameraView.enabled)
        {
            arcadeCameraView.enabled = false;
        }
       
    }

}
