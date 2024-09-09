using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalPage : MonoBehaviour,IHoldable
{
    [SerializeField] Vector3 positionOffSet;
    [SerializeField] Quaternion rotationOffSet = Quaternion.identity;

    public Quaternion HoldRotationOffset => rotationOffSet;
    public Vector3 HoldPositionOffset => positionOffSet;

    GameObject IHoldable.hands { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    [SerializeField] GameObject hands;

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

  

    public void OnHoldStart()
    {
        Debug.Log("Holding journal page");
    }

    public void OnHoldEnd(GameObject objectBeingLookedAt)
    {
        throw new System.NotImplementedException();
    }

    public void OnHolding()
    {

    }
}
