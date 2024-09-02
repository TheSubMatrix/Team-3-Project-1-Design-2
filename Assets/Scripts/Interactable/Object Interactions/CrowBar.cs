using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowBar : MonoBehaviour, IHoldable
{
    [SerializeField] Vector3 positionOffset;
     [SerializeField]Quaternion rotationOffset = Quaternion.identity;
    public Vector3 HoldPositionOffset => positionOffset;
    public GameObject hands;
    public GameObject objectPos;
    public Vector3 TransformToOffsetPositionFrom { get; set; }
    public Quaternion HoldRotationOffset => rotationOffset;

    private float pickUpSpeed = 1f;
    private float rotateSpeed = 1f;


    private Rigidbody rb;
   private bool isHolding;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
       
    }

    
    public void OnHoldStart() ///When holding crowbar it dissables the collider so it doesn't collide with player when moving camera with it
    {
        Debug.Log("Holding a crowbar");
        isHolding = true;
        if(rb != null)
        {
            rb.isKinematic = true;
            GetComponent<MeshCollider>().isTrigger = true;
            hands.SetActive(false);
            transform.parent = objectPos.transform;
        }
        
    }

    public void OnHolding(Vector3 desiredPos, Quaternion desiredRot)
    {
        transform.position = Vector3.MoveTowards(transform.position,desiredPos,Time.deltaTime * pickUpSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, Time.deltaTime * rotateSpeed);

        //transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * pickUpSpeed);
        //transform.rotation = Quaternion.Lerp(transform.rotation, desiredRot, Time.deltaTime * rotateSpeed);
    }

    public void OnHoldEnd(GameObject gameObject) ///Re-enables the collider when player drops it. Feel free to change
    {       

        if(rb != null)
        {
            isHolding = false;
            rb.isKinematic = false;
            GetComponent<MeshCollider>().isTrigger = false;
            hands.SetActive(true);
            transform.parent = null;
        }

        

    }


   

}
