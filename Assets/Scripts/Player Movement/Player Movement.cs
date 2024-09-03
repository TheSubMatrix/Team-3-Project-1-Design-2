using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    

     private LayerMask currentTerrain;
   
    private const float defaultGravityForce = -9.8f;

    [Header("Player's Speed")]
    [SerializeField] float speed = 12f;

    [Space(1)]

    [Header("Player's Gravity")]
    [Tooltip("(Negative for normal gravity and positive for floating)")]
    [SerializeField] private float gravity;
     private Vector3 gravityPull;

    [Header("Player Jump")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpDelay = 5f;

    [Header("Walkable Terrains")]
    [SerializeField] private LayerMask grassTerrain;
    [SerializeField] private LayerMask gravelTerrain;
    [SerializeField] private LayerMask futureArcadeTerrain;
    [SerializeField] private LayerMask defaultTerrain;

    [SerializeField] private Transform terrainChecker;

    private CharacterController characterController;

    private bool isJumping = false;

    private bool audioPlaying;

    public bool movementStopped { get;set; }
    //private bool movementStopped = false;

    private bool activateTerrainChecker = false;

    private string soundName;
    

    

    private void Awake()
    {
        //seeting varaibles should always be in awake
        characterController = GetComponent<CharacterController>();
        movementStopped = true;
        

    }

    private void Start()
    {
        StartCoroutine(StartDialogue());
        CheckTerrain();
       
    }
    private void Update()
    {

        /*if (Input.GetKeyDown(KeyCode.V ))
        {
            SceneTransition.Instance.ChangeScene();
        }*/
        CheckTerrain();
        
        PlayerGravity();
                
        if ( characterController.isGrounded && !isJumping && Input.GetKeyDown(KeyCode.Space))
        {

            StartCoroutine(DelayJump(jumpDelay));
            Debug.Log("Jump");
            Jump();
        }

    }
    private void FixedUpdate()
    {
        //movement is often interpolated, do you REALLY want it here?
        PlayerMove();

    }
    
    /// <summary>
    /// Basic Player Movement with gravity, footstep sound affects
    /// </summary>
    private void PlayerMove()
    {
        
            
           
            //you need to normalize this so you dont get weird values when x and z are at their peak together.
           //think of this as a square. you go further when you go from the center to the corner rather than from the center to the top.
           //the further you are from center, the faster you go.
           Vector2 playerMovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        if (!movementStopped)
        {
            if ((playerMovementInput.magnitude > 0) && characterController.isGrounded == true && audioPlaying != true)
            {
                
                activateTerrainChecker = true;
                movementStopped = false;
                audioPlaying = true;
               
                PlayUpdatedSound(soundName);



            }

            if ((playerMovementInput.magnitude == 0 || !characterController.isGrounded) && audioPlaying != false)
            {
               
                audioPlaying = false;
               
                if(SoundManager.Instance != null)
                {
                    if (SoundManager.Instance.newSoundGO != null)
                    {
                        SoundManager.Instance.StopSoundAffect(SoundManager.Instance.newSoundGO);
                    }
                    else
                    {
                        Debug.Log("Nothing to delete");
                    }
                }
                
                
            }
            Vector3 characterMove = transform.right * playerMovementInput.x + transform.forward * playerMovementInput.y;

            characterController.Move(characterMove * speed * Time.fixedDeltaTime);
        }
        

    }

    private void Jump()
    {
        gravityPull.y = Mathf.Sqrt(gravity * -jumpHeight);
        //gravityPull.y = Mathf.Sqrt(gravity * -2 * jumpHeight);       
    }

    IEnumerator DelayJump(float delay)
    {
        
        isJumping = true;
        yield return new WaitForSeconds(delay);
        isJumping = false;
    }

    private void PlayerGravity()
    {
        //where does the magic number -2 come from? plz comment and make it a const
        if (isJumping == false && characterController.isGrounded == false && gravityPull.y < 0)
        {
            gravityPull.y = defaultGravityForce;
            
        }

        gravityPull.y += gravity * Time.deltaTime;

        characterController.Move(gravityPull * Time.deltaTime);
    }
    
    void CheckTerrain()
    {
        if (!movementStopped && activateTerrainChecker)
        {
          
            if (Physics.CheckSphere(terrainChecker.position, .4f, futureArcadeTerrain) && currentTerrain != futureArcadeTerrain)
            {
                currentTerrain = futureArcadeTerrain;
                soundName = "Carpet Walking";
                PlayUpdatedSound(soundName);

            }
            
            if (Physics.CheckSphere(terrainChecker.position, .4f, grassTerrain) && currentTerrain != grassTerrain )
            {
                currentTerrain = grassTerrain;
                
                soundName = "Grass Walking";
                PlayUpdatedSound(soundName);
            }
            if (Physics.CheckSphere(terrainChecker.position, .4f, gravelTerrain) && currentTerrain != gravelTerrain )
            {
                currentTerrain = gravelTerrain;
                soundName = "Gravel Walking";
                PlayUpdatedSound(soundName);
                
            }

            if (Physics.CheckSphere(terrainChecker.position, .4f, defaultTerrain) && currentTerrain != defaultTerrain ) // defaultTerrain is just the default Layer
            {                                                                                                          // it plays a concrete walking sound
                currentTerrain = defaultTerrain;
                soundName = "Concrete Walking";
                PlayUpdatedSound(soundName);
                
            }
            
        }
    
    }

    //move your logic out WHENEVER YOU CAN.
    private void PlayUpdatedSound(string soundName)
    {
        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.StopSoundAffect(SoundManager.Instance.newSoundGO);
            SoundManager.Instance.PlaySoundOnObject(gameObject, soundName, true);
        }
              
    }

    public void TogglePlayerMovement()
    {
        if (movementStopped)
        {
            movementStopped = false;
        }
        else
        {
            movementStopped = true;
        }
    }


    IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(5f);
        SoundManager.Instance.PlaySoundAtLocation(transform.position, "Dialogue 1", false);
    }
}

