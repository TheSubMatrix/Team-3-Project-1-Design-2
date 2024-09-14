using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour, IHoldable
{
    [field:SerializeField] public Quaternion HoldRotationOffset {get; private set; }

    [field: SerializeField] public Vector3 HoldPositionOffset { get; private set; }

    public GameObject hands { get; set; }

    public void OnHoldEnd(GameObject objectBeingLookedAt)
    {

    }

    public void OnHoldStart()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
