using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CinemachineCameraSwitcher : MonoBehaviour
{
    [SerializeField] List<GameObject> cameras = new List<GameObject>();
    private PlayerMovement playerMovementRef;

    private void Awake()
    {
        playerMovementRef = GameObject.Find("First Person Player").GetComponent<PlayerMovement>();
    }
    private void Start()
    {
        StartCoroutine(SwitchBetweenCameras(cameras));
    }

    IEnumerator SwitchBetweenCameras(List<GameObject> cameraList)
    {
        for (int i = 0; i <= cameras.Count-1; i++)
        {
            yield return new WaitForSeconds(6f);
            cameraList[i].SetActive(false);
            
        }
        playerMovementRef.movementStopped = false;
    }
}
