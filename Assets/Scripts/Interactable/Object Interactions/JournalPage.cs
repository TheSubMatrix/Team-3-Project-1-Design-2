using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JournalPage : MonoBehaviour,IHoldable
{
    [SerializeField] Vector3 positionOffSet;
    [SerializeField] Quaternion rotationOffSet = Quaternion.identity;

    [SerializeField] SO_ImageDisplayChannel uiPopupChannel;
    [SerializeField] SO_RetrieveImage imageLocatorChannel;
    private float rotateSpeed = 10000;
    private float pickUpSpeed = 100;

    public Quaternion HoldRotationOffset => rotationOffSet;
    public Vector3 HoldPositionOffset => positionOffSet;

    public GameObject hands { get => myHands; set => myHands = value; }

    private float currentImageALpha;

    //  GameObject IHoldable.hands { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private GameObject myHands;
    //[SerializeField] GameObject myHands;

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if(imageLocatorChannel != null)
        {
            imageLocatorChannel.returnImage.AddListener(ChangeImageAlphaValue);
        }
        else
        {
            Debug.LogWarning("There is no imageLocatorChannel");
        }
     
    }

  

    public void OnHoldStart()
    {
        myHands.SetActive(false);
        Debug.Log("Holding journal page");
        uiPopupChannel.OnFadeImage.Invoke(new SO_ImageDisplayChannel.ImageDisplayInfo("Journal Pages", 0, .4f, 1, 0));
        ToggleJournalPage(gameObject.name);
    }

    public void OnHoldEnd(GameObject objectBeingLookedAt)
    {
        myHands.SetActive(true) ;
        uiPopupChannel.OnFadeImage.Invoke(new SO_ImageDisplayChannel.ImageDisplayInfo("Journal Pages", .4f, 0, 1, 0));
        ToggleJournalPage(gameObject.name);
    }

    public void OnHolding()
    {
       
        transform.position = Vector3.MoveTowards(transform.position, positionOffSet, Time.deltaTime * pickUpSpeed);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationOffSet, Time.deltaTime * rotateSpeed);
    }


    private void ToggleJournalPage(string journalPageName)
    {
        imageLocatorChannel.locateImage.Invoke(gameObject.name);
        if(currentImageALpha < 1)
        {
            uiPopupChannel.OnFadeImage.Invoke(new SO_ImageDisplayChannel.ImageDisplayInfo(journalPageName, 0, 1, 1, 0));
        }
        else
        {
            uiPopupChannel.OnFadeImage.Invoke(new SO_ImageDisplayChannel.ImageDisplayInfo(journalPageName, 1, 0, 1, 0));
        }
       
    }

    public void ChangeImageAlphaValue(Image image)
    {

        currentImageALpha = image.color.a;
    }
}
