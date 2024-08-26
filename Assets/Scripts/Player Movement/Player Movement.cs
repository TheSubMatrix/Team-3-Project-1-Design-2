using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private AudioManager audioManager;

    [SerializeField] private LayerMask currentTerrain;

    public bool movementStopped;



    [Header("Player's Speed")]
    [SerializeField] float speed = 12f;

    [Space(1)]

    [Header("Player's Gravity")]
    [Tooltip("(Negative for normal gravity and positive for floating)")]
    [SerializeField] private float gravity;
    [SerializeField] private Vector3 gravityPull;

    [Header("Player Jump")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpDelay = 5f;

    [Header("Walkable Terrain")]
    [SerializeField] private LayerMask grassTerrain;
    [SerializeField] private LayerMask gravelTerrain;
    [SerializeField] private LayerMask futureArcadeTerrain;

    private LayerMask lastLayer;

    [SerializeField] private Transform terrainChecker;

    private CharacterController characterController;

    private bool isJumping = false;

    public bool audioPlaying;

    private void Awake()
    {
        //seeting varaibles should always be in awake
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerGravity();
        CheckTerrain();

        if (movementStopped != true && characterController.isGrounded && !isJumping && Input.GetKeyDown(KeyCode.Space))
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
        if (movementStopped != true)
        {
            //you need to normalize this so you dont get weird values when x and z are at their peak together.
            //think of this as a square. you go further when you go from the center to the corner rather than from the center to the top.
            //the further you are from center, the faster you go.
            Vector2 playerMovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

            if ((playerMovementInput.magnitude > 0) && characterController.isGrounded == true && audioPlaying != true)
            {
                audioPlaying = true;
                audioManager.audioManagerReference.audioSource.Play();
            }

            if ((playerMovementInput.magnitude == 0 || !characterController.isGrounded) && audioPlaying != false)
            {
                audioPlaying = false;
                audioManager.audioManagerReference.audioSource.Stop();
            }
            Vector3 characterMove = transform.right * playerMovementInput.x + transform.forward * playerMovementInput.y;

            characterController.Move(characterMove * speed * Time.fixedDeltaTime);
        }
    }

    private void Jump()
    {
        //do you need 2 logs here?
        Debug.Log("Start Jump");
        gravityPull.y = Mathf.Sqrt(gravity * -jumpHeight);
        Debug.Log("Start End");
    }

    IEnumerator DelayJump(float delay)
    {
        //i feel like this could be solved with a collision instead. something to look into?
        isJumping = true;
        yield return new WaitForSeconds(delay);
        isJumping = false;
    }

    private void PlayerGravity()
    {
        //where does the magic number -2 come from? plz comment and make it a const
        if (isJumping == false && characterController.isGrounded == false && gravityPull.y < 0)
        {
            gravityPull.y = -2;
        }

        gravityPull.y += gravity * Time.deltaTime;

        characterController.Move(gravityPull * Time.deltaTime);
    }

    public void MovementStopped()
    {
        if (movementStopped == false) 
        {
            movementStopped = !movementStopped; 
        }
    }

    void CheckTerrain()
    {
        if (Physics.CheckSphere(terrainChecker.position, .4f, futureArcadeTerrain) && currentTerrain != futureArcadeTerrain)
        {
            currentTerrain = futureArcadeTerrain;
            PlayUpdatedSound(0);
        }
        if (Physics.CheckSphere(terrainChecker.position, .4f, grassTerrain) && currentTerrain != grassTerrain)
        {
            currentTerrain = grassTerrain;
            PlayUpdatedSound(2);
        }
        if (Physics.CheckSphere(terrainChecker.position, .4f, gravelTerrain) && currentTerrain != gravelTerrain)
        {
            currentTerrain = gravelTerrain;
            PlayUpdatedSound(1);
        }
    }

    //move your logic out WHENEVER YOU CAN.
    private void PlayUpdatedSound(int index)
    {
        audioManager.audioManagerReference.audioSource.Stop();
        audioManager.audioManagerReference.audioSource.clip = audioManager.audioManagerReference.walkingSFX[index];
        audioManager.audioManagerReference.audioSource.Play();
    }
}
