using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardPickup : MonoBehaviour, IPlacable, IHoldable
{
    bool m_isBeingInteractedWith;
    Rigidbody m_rigidbody;
    [SerializeField]
    Vector3 positionOffset;
    public Vector3 holdPositionOffset => positionOffset;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }
    public void OnHoldEnd(GameObject objectBeingLookedAt)
    {
        m_isBeingInteractedWith = false;
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
        m_isBeingInteractedWith = true;
        if(m_rigidbody != null)
        {
            m_rigidbody.isKinematic = true;
        }
    }

    public bool Place(GameObject objectToTryAndPlaceOn)
    {
        return false;
    }

    
}
