using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardPickup : MonoBehaviour, IPlacable, IHoldable
{
    Rigidbody m_rigidbody;
    [SerializeField]
    Vector3 positionOffset;
    [SerializeField]
    Quaternion rotationOffset = Quaternion.identity;
    public Vector3 HoldPositionOffset => positionOffset;
    public Quaternion HoldRotationOffset => rotationOffset;

    public GameObject hands { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }
    public void OnHoldUpdate()
    {

    }
    public void OnHoldEnd(GameObject objectBeingLookedAt)
    {
        if (Place(objectBeingLookedAt))
        {
            Debug.Log("No");
        }
        else
        {
            if (m_rigidbody != null)
            {
                m_rigidbody.isKinematic = false;
            }
        }
    }

    public  void OnHoldStart()
    {
        if(m_rigidbody != null)
        {
            m_rigidbody.isKinematic = true;
        }
    }

    public bool Place(GameObject objectToTryAndPlaceOn)
    {
        return false;
    }

    public void OnHolding(GameObject pickUpObj)
    {
        throw new System.NotImplementedException();
    }
}
