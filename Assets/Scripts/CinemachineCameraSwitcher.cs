using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class CinemachineCameraSwitcher : MonoBehaviour
{
    [SerializeField] GameObject playerCamera;
    [SerializeField] List<GameObject> cameras = new List<GameObject>();
    private PlayerMovement playerMovementRef;
    private bool cameraOneAnimDone;
    Animator cameraAnimatorOne,cameraAnimatorTwo;
    bool cameraAnimationDone;
    private void Awake()
    {
        playerMovementRef = GameObject.Find("First Person Player").GetComponent<PlayerMovement>();

    }
    private void Start()
    {
        SwitchCamera(cameras[0], cameras[1], 5, 5, 5);
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.H))
        {
            SwitchCamera(cameras[0], cameras[1], 10,10,6);
            
        }*/
    } 
    void SwitchCamera(GameObject cameraOne, GameObject cameraTwo, float cameraOneAnimationOver,float cameraTwoAnimationOver, float defaultCameraDelay)
    {
        StartCoroutine(SwitchCameraCoroutine(cameraOne, cameraTwo, cameraOneAnimationOver,cameraTwoAnimationOver, defaultCameraDelay));
    }

    IEnumerator SwitchCameraCoroutine(GameObject cameraOne, GameObject cameraTwo, float cameraOneAnimationOver, float cameraTwoAnimationOver, float defaultCameraDelay)
    {
        cameraAnimatorOne = cameraOne.GetComponent<Animator>();
        cameraAnimatorTwo = cameraTwo.GetComponent<Animator>();
        if (cameraAnimatorOne != null && cameraAnimatorTwo != null)
        {                      
            playerCamera.SetActive(false);
            cameraOne.SetActive(true);
            yield return new WaitForSeconds(5f);
            Debug.Log("Camera1 Start");
            cameraAnimatorOne.SetTrigger("Camera1Scroll");           
            yield return new WaitForSeconds(cameraOneAnimationOver);
            cameraOne.SetActive(false);
            cameraTwo.SetActive(true);
            yield return new WaitForSeconds(5f);
            Debug.Log("Camera2 Start");
            cameraAnimatorTwo.SetTrigger("Camera2Rotate");
            yield return new WaitForSeconds(cameraTwoAnimationOver);
            cameraTwo.SetActive(false);
            playerCamera.SetActive(true);
            yield break;
        }
        else
        {
            playerCamera.SetActive(false);
            cameraOne.SetActive(true);
            yield return new WaitForSeconds(defaultCameraDelay);
            Debug.Log("Switch to camera 2");
            cameraOne.SetActive(false);
            cameraTwo.SetActive(true);
            yield return new WaitForSeconds(defaultCameraDelay);
            Debug.Log("Switch to Player");
            playerMovementRef.TogglePlayerMovement();
            cameraTwo.SetActive(false);
            playerCamera.SetActive(true);
        }

    }
    public void BackToPlayerCamera()
    {
        playerCamera.SetActive(true);
    }
}
