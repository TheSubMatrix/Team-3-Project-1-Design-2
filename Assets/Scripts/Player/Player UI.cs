using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] SO_ImageDisplayChannel objectiveDisplayChannel;
    [SerializeField] Image controls;
    private void Start()
    {
        objectiveDisplayChannel?.OnFadeImage.AddListener(FadePlayerUIImage);
        if(controls != null)
        {
            HidePlayerUI(controls, false, 20, 5);
        }
       
    }

    void FadePlayerUIImage(SO_ImageDisplayChannel.ImageDisplayInfo imageDisplayInfo)
    {
        foreach(Image image in GetComponentsInChildren<Image>()) 
        {
            if(image.gameObject.name == imageDisplayInfo.image) 
            {
                StartCoroutine(FadePlayerUI(image, imageDisplayInfo.startAlpha, imageDisplayInfo.endAlpha, imageDisplayInfo.duration, imageDisplayInfo.delay));
            }
        }
    }
    IEnumerator FadePlayerUI(Image image, int startAlpha, int endAlpha, float duration, float delay)
    {
        float elapsedTime = 0f;
        float delayHolder = 0f;      
        Color imageColor = image.color;
       
        while(delayHolder < delay)
        {
            delayHolder += Time.deltaTime;
            
            yield return null;
        }
       
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
               
                imageColor.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
                image.color = imageColor;
                yield return null;
            }
       
        
    }


   public void HidePlayerUI(Image image, bool fadeIn, float delay, float duration )
    {
       if(fadeIn)
        {
            StartCoroutine(FadePlayerUI(image, 0, 1, duration, delay));
        }
        else
        {
            StartCoroutine(FadePlayerUI(image, 1, 0, duration, delay));
        }
        
    }
}


