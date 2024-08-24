using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeCode : MonoBehaviour, IInteractable
{
    PlayerInteractionHandler playerInteractionHandler;
    public PlayerInteractionHandler interactionHandler { get => playerInteractionHandler; set => playerInteractionHandler = value; }

    public void OnInteracting()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("D");
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("1");
        }
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("2");
        }
    }
}
