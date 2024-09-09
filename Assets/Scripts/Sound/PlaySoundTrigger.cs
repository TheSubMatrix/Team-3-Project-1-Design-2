using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class PlaySoundTrigger : MonoBehaviour
{
    [SerializeField] SO_ImageDisplayChannel imageChannel;
    [SerializeField] SO_ImageDisplayChannel.ImageDisplayInfo imageInfo;

    [System.Serializable]
    public class UpdateUIUnityEvent : UnityEvent<Image, bool, float, float> { } 

    public UpdateUIUnityEvent updateUI;

   // public Image updateImage;
   
    [SerializeField] public enum TriggerState
    {
        
        OnTriggerEnter,
        OnTriggerExit,
    }

    public TriggerState triggerState;
    
    
    private void Awake()
    {
        triggerState = new TriggerState();
    }

    [SerializeField]string soundToPlay;
    bool hasPlayed;




    private void OnTriggerEnter(Collider other)
    {
        if (triggerState == TriggerState.OnTriggerEnter)
        {
            if (!hasPlayed && other.gameObject.tag == "Player" && SoundManager.Instance != null)
            {
                if(soundToPlay == "Dialogue 5")
                {

                    Debug.Log("Next Scene");
                    if(SceneTransition.Instance == null)
                    {
                        Debug.LogWarning("No transition instance");
                        return;
                    }
                    imageChannel.OnFadeImage?.Invoke(imageInfo);
                    SoundManager.Instance.PlaySoundAtLocation(transform.position, soundToPlay, false);
                    SceneTransition.Instance.ChangeScene(4, 1, "Level Two Destroyed",null);
                    return;
                    
                }
                Debug.Log("Exit");     
                
                SoundManager.Instance.PlaySoundAtLocation(transform.position, soundToPlay, false);
                imageChannel.OnFadeImage?.Invoke(imageInfo);
                Debug.Log("Show Image");
               // updateUI?.Invoke(updateImage, true, 1, 2);
                hasPlayed = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {     
        if(triggerState == TriggerState.OnTriggerExit)
        {
            if (!hasPlayed && other.gameObject.tag == "Player" && SoundManager.Instance != null)
            {
                Debug.Log("Enter");
                SoundManager.Instance.PlaySoundAtLocation(transform.position, soundToPlay, false);
                hasPlayed = true;
            }
        }

    }

    
}
