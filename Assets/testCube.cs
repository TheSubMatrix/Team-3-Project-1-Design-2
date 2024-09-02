using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCube : MonoBehaviour
{
    Vector3 currentPos;
    [SerializeField] Vector3 newPos;

    [SerializeField] float speed = 0.5f;
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

        //transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time), transform.position.z);

        if(Input.GetKeyDown(KeyCode.G))
        {
            RotateMe();
        }

        transform.rotation = Quaternion.Euler(new Vector3(Mathf.Sin(Time.time * speed) * 45,0,0));
       
        // transform.Rotate(Mathf.Sin(Time.deltaTime * speed) * 45, 0, 0);
    }

    public void RotateMe()
    {
        transform.Rotate(Mathf.Sin(Time.deltaTime * speed) * 45, 0, 0);
        //transform.Rotate(45, 0, 0, Space.Self);
    }
}
