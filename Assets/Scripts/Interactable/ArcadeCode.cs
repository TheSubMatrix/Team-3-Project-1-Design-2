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

        }
        if (Input.GetKeyDown(KeyCode.A))
        {

        }
        if (Input.GetKeyDown(KeyCode.S))
        {

        }
        if (Input.GetKeyDown(KeyCode.D))
        {

        }
        if (Input.GetMouseButtonDown(1))
        {

        }
        if (Input.GetMouseButtonDown(2))
        {

        }
    }
}
