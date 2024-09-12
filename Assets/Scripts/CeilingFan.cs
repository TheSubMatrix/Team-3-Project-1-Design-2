using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingFan : MonoBehaviour
{
    [SerializeField] float fanSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 1 * fanSpeed, 0);
    }
}
