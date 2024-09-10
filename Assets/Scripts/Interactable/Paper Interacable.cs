using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class PaperInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] SO_VoidChannel watchEnableChannel;
    [SerializeField] SO_ImageDisplayChannel.ImageDisplayInfo imageDisplayInfoEnable;
    [SerializeField] SO_ImageDisplayChannel.ImageDisplayInfo imageDisplayInfoDisable;
    [SerializeField] SO_ImageDisplayChannel uiPopupChannel;
    [SerializeField] SO_ImageDisplayChannel imageDisplayInfoShow;
    [SerializeField] SO_RetrieveImage retrieveImageChannel;
    public PlayerInteractionHandler interactionHandler { get; set; }
    public bool ShouldStopMovement { get; set; } = true;
    private float currentImageALpha;
   

    private void Awake()
    {
        retrieveImageChannel.returnImage.AddListener(ChangeImageAlphaValue);
    }
    public void OnInteractStart(PlayerInteractionHandler incomingHandler)
    {
        interactionHandler = incomingHandler;
        Debug.Log("Paper Picked Up");
        retrieveImageChannel.locateImage.Invoke("Find Journal Entries");
        if(currentImageALpha < 1)
        {
            uiPopupChannel.OnFadeImage.Invoke(new SO_ImageDisplayChannel.ImageDisplayInfo("Find Journal Entries", 0, 1, .5f, 0));
        }
        
        uiPopupChannel.OnFadeImage.Invoke(new SO_ImageDisplayChannel.ImageDisplayInfo("Journal Pages", 0, .8f, .5f, 0));
        imageDisplayInfoShow.OnFadeImage?.Invoke(imageDisplayInfoEnable);
        watchEnableChannel.myEvent?.Invoke();
    }
    public void OnInteracting()
    {

    }
    public void OnInteractEnd()
    {
        imageDisplayInfoShow.OnFadeImage?.Invoke(imageDisplayInfoDisable);
        uiPopupChannel.OnFadeImage.Invoke(new SO_ImageDisplayChannel.ImageDisplayInfo("Journal Pages", .8f, 0, .5f, 0));
        if(SoundManager.Instance != null)
        {
            JournalDialogueLine(gameObject.name);
        }
        else
        {
            Debug.LogError("No Sound Manager in current scene");
        }       
        Destroy(gameObject);
    }
    private void JournalDialogueLine(string journalEntry)
    {
        switch (journalEntry)
        {
            case "Journal 1":
                SoundManager.Instance.PlaySoundAtLocation(transform.position, "Journal 1 Dialogue", false);
                break;
            case "Journal 2":
                SoundManager.Instance.PlaySoundAtLocation(transform.position, "Journal 2 Dialogue", false);
                break;
            case "Journal 3":
                SoundManager.Instance.PlaySoundAtLocation(transform.position, "Journal 3 Dialogue", false);
                break;
            case "Journal 4":
                Debug.Log("Journal 4");
                break;
            case "Journal 5":
                Debug.Log("Journal 5");
                break;
            case "Journal 6":
                Debug.Log("Journal 6");
                break;
            default:
                Debug.LogError("Journal object doesn't mactch name for function");
                break;
        }
    }
    public void ChangeImageAlphaValue(Image image)
    {

        currentImageALpha = image.color.a;
    }
}
