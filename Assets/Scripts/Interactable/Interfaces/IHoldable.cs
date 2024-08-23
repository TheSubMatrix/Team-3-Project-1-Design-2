using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoldable
{
    Transform transform {  get; }
    GameObject gameObject { get; }
    void OnHoldStart();
    void OnHolding(Vector3 desiredPosition)
    {
        transform.position = desiredPosition;
    }
    void OnHoldEnd(GameObject objectBeingLookedAt);
}
