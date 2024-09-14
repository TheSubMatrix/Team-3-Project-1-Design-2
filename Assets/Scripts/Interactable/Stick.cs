using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour, IHoldable
{
    [field:SerializeField] public Quaternion HoldRotationOffset {get; private set; }

    [field: SerializeField] public Vector3 HoldPositionOffset { get; private set; }

    PlayerInteractionHandler myPlayerIntercationHandler;

    private GameObject myHands;

    public PlayerInteractionHandler playerInteractionHandler { get => myPlayerIntercationHandler; set => myPlayerIntercationHandler = value; }

    private float pickUpSpeed = 100f;
    private float rotateSpeed = 10000f;

    private Rigidbody rb;
    public GameObject hands { get => myHands; set => myHands = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnHoldStart( PlayerInteractionHandler incomingHandler)
    {
        myPlayerIntercationHandler = incomingHandler;
        myPlayerIntercationHandler.raycastDistance = 10;
        myHands.SetActive(false);

        if(rb != null)
        {
            rb.isKinematic = true;
            GetComponent<CapsuleCollider>().isTrigger = true;
        }
    }

    public void OnHolding(Vector3 desiredPos, Quaternion desiredRot)
    {
        transform.position = Vector3.MoveTowards(transform.position,desiredPos,Time.deltaTime * pickUpSpeed);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, Time.deltaTime * rotateSpeed);
        gameObject.layer = LayerMask.NameToLayer("Render On Top");
    }

    public void OnHoldEnd(GameObject objectBeingLookedAt)
    {
        myPlayerIntercationHandler.raycastDistance = 5;
        
        if(rb != null)
        {
            rb.isKinematic = false;
            GetComponent<CapsuleCollider>().isTrigger = true;
            myHands.SetActive(true);
        }
    }

}
