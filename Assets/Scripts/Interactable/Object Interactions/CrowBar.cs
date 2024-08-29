using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowBar : MonoBehaviour, IHoldable
{
    Vector3 positionOffset;
    public Vector3 holdPositionOffset => positionOffset;
    [SerializeField] Collider breakableObject;
    private Rigidbody rb;
   private bool isHolding;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
       
    }
    public void OnHoldStart()
    {
        Debug.Log("Holding a crowbar");
        isHolding = true;
        if(rb != null)
        {
            rb.isKinematic = true;
        }
    }
   
    public void OnHolding()
    {
        
        
    }

    public void OnHoldEnd(GameObject gameObject)
    {
        Debug.Log($"Stopped {gameObject.name}");

        if(rb != null)
        {
            isHolding = false;
            rb.isKinematic = false;
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if(isHolding)
        {
            if(other == breakableObject)
            {
                Destroy(other.gameObject);
            }
            else
            {
                Debug.Log(other.gameObject.name);
            }
        }
        
        
    }

}
