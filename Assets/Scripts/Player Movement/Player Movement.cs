using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    private AudioSource characterAudioSource;
    
    private void Start()
    {       
        characterController = GetComponent<CharacterController>();
        characterAudioSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        PlayerMove();

        
    }

    /// <summary>
    /// Basic Player Movement with gravity, footstep sound affects
    /// </summary>
    private void PlayerMove() 
    { 

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if((x!= 0 || z!= 0) && characterController.isGrounded == true) 
        {
            Vector3 characterMove = transform.right * x + transform.forward * z;

            characterController.Move(characterMove * speed * Time.deltaTime);

            characterAudioSource.enabled = true;
        } 
        else
        {
            characterAudioSource.enabled = false;   
        }
        


        gravityPull.y += gravity * Time.deltaTime;
        characterController.Move(gravityPull * .1f * Time.deltaTime);
    }    
}
