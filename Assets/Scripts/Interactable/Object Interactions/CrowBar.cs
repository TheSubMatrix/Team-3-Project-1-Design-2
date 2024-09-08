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
    
    
    public Vector3 TransformToOffsetPositionFrom { get; set; }
    public Quaternion HoldRotationOffset => rotationOffset;

    public PlayerUI PlayerUI { get; set; }

    private float pickUpSpeed = 100f;
    private float rotateSpeed = 10000f;

    float currentImageALpha = 0;

    bool firstTimePickUp;

    private Rigidbody rb;
    private void Awake()
    {
       imageLocaterChannel.returnImage.AddListener(ChangeImageAlphaValue);
        rb = GetComponent<Rigidbody>();
       
    }
    public void OnHoldStart() ///When holding crowbar it dissables the collider so it doesn't collide with player when moving camera with it
    {
        Debug.Log("Holding a crowbar");
      //  isHolding = true;
        if(rb != null)
        {
            rb.isKinematic = true;
            GetComponent<MeshCollider>().isTrigger = true;
            GameObject.Find("Hands").SetActive(false);
            
            
            // uiPopupChannel.OnFadeImage.Invoke(new SO_ImageDisplayChannel.ImageDisplayInfo("Break Inside", 0, 1, .5f, 0));

            imageLocaterChannel.locateImage.Invoke("Grab Crow Bar");

            if(currentImageALpha > 0 && !firstTimePickUp)
            {
                uiPopupChannel.OnFadeImage.Invoke(new SO_ImageDisplayChannel.ImageDisplayInfo("Grab Crow Bar", (int)currentImageALpha, 0, .5f, 0));
                firstTimePickUp = true;
            }
           

           
           
        }
        
    }

    public void OnHolding(Vector3 desiredPos, Quaternion desiredRot)
    {
        transform.position = Vector3.MoveTowards(transform.position,desiredPos,Time.deltaTime * pickUpSpeed);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, Time.deltaTime * rotateSpeed);

       
    }

    public void OnHoldEnd(GameObject gameObject) ///Re-enables the collider when player drops it. Feel free to change
    {
        GameObject.Find("Hands").SetActive(true);
        if (rb != null)
        {
           // isHolding = false;
            rb.isKinematic = false;
            GetComponent<MeshCollider>().isTrigger = false;
            //hands.SetActive(true);
            transform.parent = null;
        }
    }

    public void ChangeImageAlphaValue(Image image)
    {

        currentImageALpha = image.color.a;              
    }
}
