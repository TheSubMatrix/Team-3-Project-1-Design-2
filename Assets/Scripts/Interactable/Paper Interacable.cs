using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PaperInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] SO_VoidChannel watchEnableChannel;
    [SerializeField] SO_ImageDisplayChannel.ImageDisplayInfo imageDisplayInfoEnable;
    [SerializeField] SO_ImageDisplayChannel.ImageDisplayInfo imageDisplayInfoDisable;
    [SerializeField] SO_ImageDisplayChannel uiPopupChannel;
    [SerializeField] SO_ImageDisplayChannel imageDisplayInfoShow;
    public PlayerInteractionHandler interactionHandler { get; set; }
    public bool ShouldStopMovement { get; set; } = true;

    public void OnInteractStart(PlayerInteractionHandler incomingHandler)
    {
        interactionHandler = incomingHandler;
        Debug.Log("Paper Picked Up");
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
        JournalDialogueLine(gameObject.name);
        //SoundManager.Instance.PlaySoundAtLocation(transform.position, "Dialogue 7", false);
        Destroy(gameObject);
    }
    private void JournalDialogueLine(string journalEntry)
    {
        switch (journalEntry)
        {
            case "Journal 1":
                Debug.Log("Journal 1");
                break;
            case "Journal 2":
                Debug.Log("Journal 2");
                break;
            case "Journal 3":
                Debug.Log("Journal 3");
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
}
