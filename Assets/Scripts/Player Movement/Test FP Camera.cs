using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFPCamera : MonoBehaviour
{
    public float mouseSensitiy = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitiy * Time.deltaTime; 
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitiy * Time.deltaTime;

        xRotation -= mouseY;
        
        xRotation = Mathf.Clamp(xRotation, -90, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX ) ;
    }
}
