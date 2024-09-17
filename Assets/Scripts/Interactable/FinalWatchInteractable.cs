using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinalWatchInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] UnityEvent OnWatchInteracted;
    public void OnInteractStart(PlayerInteractionHandler incomingHandler)
    {
        interactionHandler = incomingHandler;
        OnWatchInteracted?.Invoke();
        PlayerLevelSwitcher playerLevelSwitcher = interactionHandler.GetComponentInParent<PlayerLevelSwitcher>();
        if (playerLevelSwitcher != null)
        {
            SoundManager.Instance.PlaySoundAtLocation(transform.position, "Teleport", false);
            playerLevelSwitcher.FakeTransition();
        }
        SceneTransition.Instance?.ChangeScene(1, 1, "End Screen", null);
    }
    public PlayerInteractionHandler interactionHandler { get; set; }
    public bool ShouldStopMovement { get; set; }

    public void OnInteracting()
    {
        
    }
}
