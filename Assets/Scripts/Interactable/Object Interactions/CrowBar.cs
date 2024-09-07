using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class CrowBar : MonoBehaviour, IHoldable, IUsesUI
{
    [SerializeField] Vector3 positionOffset;
     [SerializeField]Quaternion rotationOffset = Quaternion.identity;
    public Vector3 HoldPositionOffset => positionOffset;
    //public GameObject hands;
    //public GameObject objectPos;
    public Vector3 TransformToOffsetPositionFrom { get; set; }
    public Quaternion HoldRotationOffset => rotationOffset;

    public PlayerUI PlayerUI { get; set; }

    private float pickUpSpeed = 100f;
    private float rotateSpeed = 10000f;

    [SerializeField] Image crowbarUIImage => PlayerUI?.GetComponentsInChildren<Image>().Where(o => o.gameObject.name == "Break Inside").First();

    private Rigidbody rb;
    private void Awake()
    {
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
            //hands.SetActive(false);
            //transform.parent = objectPos.transform;
            PlayerUI?.HidePlayerUI(crowbarUIImage, false, 0, 2);
        }
        
    }

    public void OnHolding(Vector3 desiredPos, Quaternion desiredRot)
    {
        transform.position = Vector3.MoveTowards(transform.position,desiredPos,Time.deltaTime * pickUpSpeed);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, Time.deltaTime * rotateSpeed);

        //transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * pickUpSpeed);
        //transform.rotation = Quaternion.Lerp(transform.rotation, desiredRot, Time.deltaTime * rotateSpeed);
    }

    public void OnHoldEnd(GameObject gameObject) ///Re-enables the collider when player drops it. Feel free to change
    {       

        if(rb != null)
        {
           // isHolding = false;
            rb.isKinematic = false;
            GetComponent<MeshCollider>().isTrigger = false;
            //hands.SetActive(true);
            transform.parent = null;
        }
    }
}
