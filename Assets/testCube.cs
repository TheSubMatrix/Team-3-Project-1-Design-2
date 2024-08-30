using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCube : MonoBehaviour
{
    Vector3 currentPos;
    [SerializeField] Vector3 newPos;

    private void Awake()
    {
        //currentPos = transform.position;
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        /*float x = transform.position.x;
        float y = Mathf.Sin(Time.time);
        float z = transform.position.z;*/

        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time), transform.position.z);

    }
}
