using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ArcadeCode : MonoBehaviour, IInteractable
{
    [SerializeField]UnityEvent OnCodeCompleted = new UnityEvent();
    PlayerInteractionHandler playerInteractionHandler;
    public PlayerInteractionHandler interactionHandler { get => playerInteractionHandler; set => playerInteractionHandler = value; }
    public bool ShouldStopMovement { get; set; } = true;
    Animator animator;

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
       
    }


    [SerializeField] char[] code = { 'w', 'w', 's', 's', 'a', 'd', 'a', 'd', '0', '1' };
    char[] typedChars = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
    public void OnInteracting()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            UpdateChars('w');
            animator.Play("Up_Joystick");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            UpdateChars('a');
            animator.Play("Left_Joystick");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            UpdateChars('s');
            animator.Play("Down_Joystick");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            UpdateChars('d');
            animator.Play("Right_Joystick");
        }
        if (Input.GetMouseButtonDown(0))
        {
            UpdateChars('0');
            animator.Play("Button1 Push");
        }
        if (Input.GetMouseButtonDown(1))
        {
            UpdateChars('1');
            animator.Play("Button2 Push");
        }
    }
    void CheckCode()
    {
        if (typedChars.SequenceEqual(code))
        {
            OnCodeCompleted.Invoke();
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
}
