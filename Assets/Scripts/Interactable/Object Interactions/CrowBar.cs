using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class CrowBar : MonoBehaviour, IHoldable
{
    [SerializeField] Vector3 positionOffset;
    [SerializeField] Quaternion rotationOffset = Quaternion.identity;
    [SerializeField] SO_ImageDisplayChannel uiPopupChannel;
    [SerializeField] SO_RetrieveImage imageLocaterChannel;

    
    public Vector3 HoldPositionOffset => positionOffset;

    private PlayerInteractionHandler myPlayerInteractionHandler;
    public PlayerInteractionHandler playerInteractionHandler { get => myPlayerInteractionHandler; set => myPlayerInteractionHandler = value; }
    public Vector3 TransformToOffsetPositionFrom { get; set; }
    public Quaternion HoldRotationOffset => rotationOffset;

    public PlayerUI PlayerUI { get; set; }
   
    private float pickUpSpeed = 100f;
    private float rotateSpeed = 10000f;

    float currentImageALpha = 0;

    [SerializeField] FrontDoor frontDoorRef;

    [SerializeField] GameObject grabCrowBarTrigger;
    
    bool pickedUp, brokeInside;

    private GameObject myHands;
    public GameObject hands { get => myHands; set => myHands = value; }

    

    private Rigidbody rb;
    private void Awake()
    {
       imageLocaterChannel.returnImage.AddListener(ChangeImageAlphaValue);
        rb = GetComponent<Rigidbody>();
       


    }
    public void OnHoldStart(PlayerInteractionHandler incomingHandler) ///When holding crowbar it dissables the collider so it doesn't collide with player when moving camera with it
    {
        myPlayerInteractionHandler = incomingHandler;

            myHands.SetActive(false);

        grabCrowBarTrigger.SetActive(false); //If the player grabs the crowbar before heading to the front door
                                            // it will turn off the trigger asking the player to grab it from the trunk
        Debug.Log("Holding a crowbar");
      //  isHolding = true;
        if(rb != null)
        {
            rb.isKinematic = true;
            GetComponent<MeshCollider>().isTrigger = true;
            pickedUp = true;
            // transform.SetParent(GameObject.Find("Put Object Here").transform);
            // hands.SetActive(false);
            // GameObject.Find("Hands").SetActive(false);

            imageLocaterChannel.locateImage.Invoke("Break Inside");
            if(currentImageALpha < 1 && !brokeInside) /// Displays the Break Inside UI panel once if alpha is less than 1
            {
                uiPopupChannel.OnFadeImage.Invoke(new SO_ImageDisplayChannel.ImageDisplayInfo("Break Inside", 0, 1, .5f, 0));
            }
            
            imageLocaterChannel.locateImage.Invoke("Grab Crow Bar");
            if(currentImageALpha > 0 && pickedUp) ///Fades out the Grab Crow Bar UI panel only on the first pick up and if alpha is already at 1
            {
                uiPopupChannel.OnFadeImage.Invoke(new SO_ImageDisplayChannel.ImageDisplayInfo("Grab Crow Bar", (int)currentImageALpha, 0, .5f, 0));
               
            }
                   
        }
        
    }

    public void OnHolding(Vector3 desiredPos, Quaternion desiredRot)
    {
        transform.position = Vector3.MoveTowards(transform.position,desiredPos,Time.deltaTime * pickUpSpeed);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, Time.deltaTime * rotateSpeed);
        gameObject.layer = LayerMask.NameToLayer("Render On Top");
        if (frontDoorRef.boardCount == 5 && !brokeInside) ///When the player has took down all the boards and brokInside is false it will fade out once
        {
            uiPopupChannel.OnFadeImage.Invoke(new SO_ImageDisplayChannel.ImageDisplayInfo("Break Inside", 1, 0, .5f, 0));
            brokeInside = true;
        }
       
    }

    public void OnHoldEnd(GameObject gameObject) ///Re-enables the collider when player drops it. Feel free to change
    {
        
        if (rb != null)
        {          
            rb.isKinematic = false;
            GetComponent<MeshCollider>().isTrigger = false;

           // transform.parent = null;
           hands.SetActive(true);
            RaycastHit hit;
            this.gameObject.layer = LayerMask.NameToLayer("Default");
            Physics.Raycast(transform.position, Vector3.down, out hit, 5);
            if (hit.collider.gameObject.layer == 8)
            {
                Debug.Log("Touching grass");
            } 
           // SoundManager.Instance.PlaySoundAtLocation(transform.position, "Crowbar Drop on Grass", false);
        }       
    }

    public void ChangeImageAlphaValue(Image image)
    {

        currentImageALpha = image.color.a;              
    }
}
