using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardItem : MonoBehaviour, IPlacable, IHoldable
{
    bool m_isBeingInteractedWith;
    Rigidbody m_rigidbody;
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }
    public void OnHoldEnd(GameObject objectBeingLookedAt)
    {
        m_isBeingInteractedWith = false;
        if (Place(objectBeingLookedAt))
        {

        }
        else
        {
            if (m_rigidbody != null)
            {
                m_rigidbody.isKinematic = true;
            }
        }
    }

    public void OnHolding(Vector3 vector)
    {
        transform.position = vector;
    }

    public void OnHoldStart()
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
