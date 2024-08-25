using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] AudioManager audioManager;

    public LayerMask currentTerrain,oldTerrain;
       
    [SerializeField]public bool movementStoped;

   
    
    [Header("Player's Speed")]
    [SerializeField] float speed = 12f;
    
   
    [Header("Player's Gravity")]
    [Space(1)]
    
    [Header("(Negative for normal gravity and positive for floating)")]
    [SerializeField] float gravity;
    [SerializeField] Vector3 gravityPull;

    [Header("Player Jump")]
    [SerializeField] float jumpHeight;
    [SerializeField] float jumpDelay = 5f;

    [Header("Walkable Terrain")]
    [SerializeField] LayerMask grassTerrain;
    [SerializeField] LayerMask gravelTerrain;
    [SerializeField] LayerMask futureArcadeTerrain;

   
    [SerializeField] Transform terrainChecker;

    private bool terrainChecked;

    private CharacterController characterController;

    private bool isJumping = false;

   public bool audioPlaying;

    private void Start()
    {       
       
        characterController = GetComponent<CharacterController>();
        
        //audioManager.AudioManagerReference.Play();

    }

    private void Update()
    {
       
        //Debug.Log();
        PlayerGravity();
        

        if (movementStoped != true && characterController.isGrounded  && !isJumping && Input.GetKeyDown(KeyCode.Space))
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
    private void LateUpdate()
    {
        
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

            

            if ((x != 0 || z != 0) && characterController.isGrounded == true && audioPlaying != true)
            {
                CheckTerrain();
                
                

                audioPlaying = true;
                
                
                
                          
            }
           
             if(((x == 0 && z == 0)  || !characterController.isGrounded) && audioPlaying != false)
            {
                Debug.Log("Stop");
                
                audioPlaying = false;
                terrainChecked = false;
                audioManager.audioManagerReference.audioSource.Stop();
                
                
            }

            Vector3 characterMove = transform.right * x + transform.forward * z;

            characterController.Move(characterMove * speed * Time.fixedDeltaTime);
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
        if (movementStoped == false)
        {
            movementStoped = true; 
            
        }
        else
        {
            movementStoped = false;
            
        }

        

    }

    public void CheckTerrain()
    {

        
        
            if (Physics.CheckSphere(terrainChecker.position, .4f, futureArcadeTerrain))
            {

                currentTerrain = futureArcadeTerrain;
                Debug.Log("Changing to arcade");
               
               audioManager.audioManagerReference.audioSource.clip = audioManager.audioManagerReference.walkingSFX[0];
                audioManager.audioManagerReference.audioSource.Play();
                Debug.Log("Changed to arcade");
  
            }
            if (Physics.CheckSphere(terrainChecker.position, .4f, grassTerrain))
            {
                currentTerrain = grassTerrain;
                Debug.Log("Changing to grass");
               
               audioManager.audioManagerReference.audioSource.clip = audioManager.audioManagerReference.walkingSFX[2];
                Debug.Log("Changed to grass");
               audioManager.audioManagerReference.audioSource.Play();
                
            }
            if (Physics.CheckSphere(terrainChecker.position, .4f, gravelTerrain))
            {

                currentTerrain = gravelTerrain;
                Debug.Log("Changing to gravel");
               
                audioManager.audioManagerReference.audioSource.clip = audioManager.audioManagerReference.walkingSFX[1];
                Debug.Log("Changed to gravel");
                 audioManager.audioManagerReference.audioSource.Play();
                
            }
        
        
        
        
    }
    


}
