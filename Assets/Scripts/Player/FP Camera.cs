using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Camera : MonoBehaviour
{
    public SO_BoolChannel playerMovementActivationChannel;
    [Header("Camera Settings")]
    [SerializeField] Transform cameraOrientation;
    [SerializeField] float sensitivityX, sensitivityY;

    bool canMoveCamera = false;
    private float xRotation, yRotation;

   private GameObject defaultCamera;

    private void Awake()
    {
        playerMovementActivationChannel.boolEvent.AddListener(UpdatePlayerMovementState);
    }
    void Start()
    {
        defaultCamera = gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void UpdatePlayerMovementState(bool newState) 
    {
        canMoveCamera = newState;
    }
    // Update is called once per frame
    void Update()
    {
        if (canMoveCamera)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

            yRotation += mouseX;
            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);



            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            cameraOrientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        

       // transform.position = new Vector3(Mathf.Sin(Time.time) * 0.5f, transform.position.y, transform.position.z);

        // transform.position = transform.position + new Vector3(transform.position.x,Mathf.Sin(Time.time) * 0.5f, transform.position.y);
    }

    public void ToggleActiceCamera(GameObject switchCamera)
    {
        if(switchCamera != null)
        {
            Debug.Log("There is no camera for arcade machine");
            if (defaultCamera.activeSelf)
            {
                defaultCamera.SetActive(false);
                switchCamera.SetActive(true);
            }
            else
            {
                switchCamera.SetActive(false);
                defaultCamera.SetActive(true);
            }
        }
        
    }
}
