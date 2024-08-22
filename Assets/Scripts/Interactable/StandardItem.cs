using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardItem : MonoBehaviour, IPlacable, IInteractable
{
    bool m_isBeingInteractedWith;
    public void OnInteractEnd()
    {
        m_isBeingInteractedWith = false;
        Place(transform.position, Vector3.zero);
    }

    public void OnInteracting()
    {
        
    }

    public void OnInteractStart()
    {
        m_isBeingInteractedWith = true;
    }

    public void Place(Vector3 location, Vector3 velocity)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_isBeingInteractedWith)
        {
            OnInteracting();
        }
    }
}
