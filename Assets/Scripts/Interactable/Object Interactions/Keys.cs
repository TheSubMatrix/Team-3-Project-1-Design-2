using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class Keys : MonoBehaviour, IHoldable
{

    [SerializeField] Vector3 positionOffset;
    [SerializeField] Quaternion rotationOffSet = Quaternion.identity;
    public Quaternion HoldRotationOffset => rotationOffSet;

    public Vector3 HoldPositionOffset => positionOffset;

    PlayerInteractionHandler myInteractionHandler;

    private GameObject myHands;
    public PlayerInteractionHandler playerInteractionHandler { get => myInteractionHandler; set => myInteractionHandler = value; }
    public GameObject hands { get => myHands; set => myHands = value; }
    private float pickUpSpeed = 100f;
    private float rotateSpeed = 10000f;
    private Rigidbody rb;
    private bool hasKey = false;
    private bool firstTimePickup = true;
    [System.Serializable]
    public class CheckForKeyInHand: UnityEvent<bool> { }

   [SerializeField]CheckForKeyInHand checkForKeyInHandEvent;

    [SerializeField] Animator childAnimator;

    private Animator parentAnimator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        parentAnimator = GetComponent<Animator>();
        
    }
   
 
    public void OnHoldStart(PlayerInteractionHandler incomingHandler)
    {
        myInteractionHandler = incomingHandler;
        if (rb != null)
        {
            rb.isKinematic = true;
            GetComponent<Collider>().isTrigger = true;
            myHands.SetActive(false);
           
        }    
        if(firstTimePickup && SceneManager.GetActiveScene().name == "Level One")
        {
            SoundManager.Instance.PlaySoundOnObject(gameObject, "Found Key", false);
            firstTimePickup= false;
        }
    }
    
    public void OnHolding(Vector3 desiredPos,Quaternion desiredRot)
    {
        transform.position = Vector3.MoveTowards(transform.position, desiredPos, Time.deltaTime * pickUpSpeed);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, Time.deltaTime * rotateSpeed);
        gameObject.layer = LayerMask.NameToLayer("Render On Top");
        hasKey = true;
       
        checkForKeyInHandEvent.Invoke(hasKey);
        

    }
    
    public void OnHoldEnd(GameObject gameObject)
    {
        
        if(rb != null)
        {
            rb.isKinematic = false;
            GetComponent<Collider>().isTrigger = false;
            myHands.SetActive(true);
            hasKey = false;
            checkForKeyInHandEvent.Invoke(hasKey);
            this.gameObject.layer = LayerMask.NameToLayer("Default");
        }

    }

    public void DeleteAnimator()
    {
        Destroy(parentAnimator);
    }
    public void UseKeyAnimation()
    {
        if(childAnimator != null)
        {
            childAnimator.Play("UseKey");
        }
        else
        {
            return;
        }
      
    }

    public void Level2KeyAnimation()
    {
        parentAnimator.Play("Open");
    }
}
