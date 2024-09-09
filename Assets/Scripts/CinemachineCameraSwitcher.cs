using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CinemachineCameraSwitcher : MonoBehaviour
{
    [SerializeField] SO_BoolChannel playerMovementUpdateChannel;
    [SerializeField] GameObject playerCamera;
    [SerializeField] List<GameObject> cameras = new List<GameObject>();
    private bool cameraOneAnimDone;
    Animator cameraAnimatorOne,cameraAnimatorTwo;
    bool cameraAnimationDone;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Level One")
        {
            SwitchCamera(cameras[0], cameras[1], 5, 5, 5);
        }
        else
        {
            playerMovementUpdateChannel?.boolEvent?.Invoke(true);
        }
       
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
            playerMovementUpdateChannel?.boolEvent?.Invoke(true);
            cameraTwo.SetActive(false);
            playerCamera.SetActive(true);
        }

    }
    public void BackToPlayerCamera()
    {
        playerCamera.SetActive(true);
    }
}
