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


    LayerMask startingLayer;
    public GameObject hands { get; set; }
    public PlayerInteractionHandler playerInteractionHandler { get; set; }

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        startingLayer = gameObject.layer;
    }
    public void OnHoldUpdate()
    {

    }
    public void OnHoldEnd(GameObject objectBeingLookedAt)
    {

        if (m_rigidbody != null)
        {
            m_rigidbody.isKinematic = false;
        }
        hands?.SetActive(true);
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.layer = startingLayer;
    }

    public  void OnHoldStart()
    {
        if(m_rigidbody != null)
        {
            m_rigidbody.isKinematic = true;
        }
        hands?.SetActive(false);
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Render On Top");
    }

    public void OnHolding(GameObject pickUpObj)
    {
        throw new System.NotImplementedException();
    }
}
