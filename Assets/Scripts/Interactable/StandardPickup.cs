using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardPickup : MonoBehaviour, IHoldable
{
    Rigidbody m_rigidbody;
   
    [SerializeField]
    Vector3 positionOffset;
    [SerializeField]
    Quaternion rotationOffset = Quaternion.identity;
  
    public Vector3 HoldPositionOffset => positionOffset;
    public Quaternion HoldRotationOffset => rotationOffset;


    private float pickUpSpeed = 100f;
    private float rotateSpeed = 10000f;

    LayerMask startingLayer;
    public GameObject hands { get; set; }
    public PlayerInteractionHandler playerInteractionHandler { get => myPlayerInteractionHandler; set => myPlayerInteractionHandler = value; }

    private PlayerInteractionHandler myPlayerInteractionHandler;
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        startingLayer = gameObject.layer;
    }
    public void OnHoldStart( PlayerInteractionHandler incomingHandler)
    {
        myPlayerInteractionHandler = incomingHandler;
        if (m_rigidbody != null)
        {
            m_rigidbody.isKinematic = true;
            gameObject.GetComponent<Collider>().enabled = false;
           
        }
        hands?.SetActive(false);
       
    }
    public void OnHolding(Vector3 desiredPos, Quaternion desiredRot)
    {
        transform.position = Vector3.MoveTowards(transform.position, desiredPos, Time.deltaTime * pickUpSpeed);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, Time.deltaTime * rotateSpeed);
        gameObject.layer = LayerMask.NameToLayer("Render On Top");
    }
    public void OnHoldEnd(GameObject objectBeingLookedAt)
    {

        if (m_rigidbody != null)
        {
            m_rigidbody.isKinematic = false;
            gameObject.GetComponent<Collider>().enabled = true;
        }
      
        hands?.SetActive(true);
      
        gameObject.layer = startingLayer;
    }

    

    
}
