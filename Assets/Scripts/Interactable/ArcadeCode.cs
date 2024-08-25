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
    [SerializeField] char[] code = { 'w', 'w', 's', 's', 'a', 'd', 'a', 'd', '0', '1' };
    char[] typedChars = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
    public void OnInteracting()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            UpdateChars('w');
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            UpdateChars('a');
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            UpdateChars('s');
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            UpdateChars('d');
        }
        if (Input.GetMouseButtonDown(0))
        {
            UpdateChars('0');
        }
        if (Input.GetMouseButtonDown(1))
        {
            UpdateChars('1');
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
