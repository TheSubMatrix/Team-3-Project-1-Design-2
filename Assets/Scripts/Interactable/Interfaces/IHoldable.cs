using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoldable
{
    public Quaternion HoldRotationOffset { get; }
    public Vector3 HoldPositionOffset { get; }

    PlayerInteractionHandler playerInteractionHandler { get;set; }
    Transform transform {  get; }
    GameObject gameObject { get; }
    public GameObject hands { get; set; }
    void OnHoldStart(PlayerInteractionHandler incomingHandler)
    {
        playerInteractionHandler = incomingHandler;
    }
    void OnHolding(Vector3 desiredPosition, Quaternion desiredRotation)
    {
        transform.position = desiredPosition;
        transform.rotation = desiredRotation;
    }
    void OnHoldEnd(GameObject objectBeingLookedAt);
}
