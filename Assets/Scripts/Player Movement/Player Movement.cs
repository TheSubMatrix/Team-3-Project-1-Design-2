using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]public bool movementStoped;
    

    [Header("Player's Speed")]
    [SerializeField] float speed = 12f;
    
   
    [Header("Player's Gravity")]
    [Space(1)]
    
    [Header("(Negative for normal gravity and positive for floating)")]
    [SerializeField] float gravity;
    [SerializeField] Vector3 gravityPull;

    [Header("Player Jump Height")]
    [SerializeField] float jumpHeight;

    private CharacterController characterController;

    private AudioSource characterAudioSource;

    private bool isJumping = false;
    [SerializeField] float jumpDelay = 5f;

    
    private void Start()
    {       
       
        characterController = GetComponent<CharacterController>();
        characterAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        
        PlayerGravity();
        if (movementStoped != true && isJumping != true && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DelayJump(jumpDelay));
            Debug.Log("Jump");
            Jump();
        }
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
        if(movementStoped != true)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            if ((x != 0 || z != 0) && characterController.isGrounded == true)
            {
                characterAudioSource.enabled = true;
            }
            else
            {
                characterAudioSource.enabled = false;
            }

            Vector3 characterMove = transform.right * x + transform.forward * z;

            characterController.Move(characterMove * speed * Time.deltaTime);
        }
        

    }    

    private void Jump()
    {
        Debug.Log("Start Jump");
        gravityPull.y = Mathf.Sqrt( gravity * -jumpHeight);
        Debug.Log("Start End");
    }

    IEnumerator DelayJump(float delay)
    {
        isJumping = true;
        yield return new WaitForSeconds(delay);
        isJumping = false;
    }

    private void PlayerGravity()
    {
        if(isJumping == false && characterController.isGrounded == false && gravityPull.y < 0)
        {
            gravityPull.y = -2;
        }
        
        gravityPull.y += gravity * Time.deltaTime;

        characterController.Move(gravityPull * Time.deltaTime);
    }

    public void MovementStopped()
    {
        if(movementStoped == false)
        {
            movementStoped = true;
        }
        else
        {
            movementStoped = false;
        }
    }

    
}
