using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player's Speed")]
    [SerializeField] float speed = 12f;
    
   
    [Header("Player's Gravity")]
    [Space(1)]
    [Header("(Negative for normal gravity and positive for floating)")]
    [SerializeField] float gravity;
    private Vector3 gravityPull;

    private CharacterController characterController;
    private void Start()
    {      
        characterController = GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        PlayerMove();
       
        
    }

    private void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 characterMove = transform.right * x + transform.forward * z;

        characterController.Move(characterMove * speed * Time.deltaTime);

        gravityPull.y += gravity * Time.deltaTime;
        
        characterController.Move(gravityPull * .1f * Time.deltaTime);
    }   
}
