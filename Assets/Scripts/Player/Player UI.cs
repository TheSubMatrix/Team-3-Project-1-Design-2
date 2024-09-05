using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private List<Image> playerUIImages;

    float elapsedTime = 0f;
    bool controlsImageDisappear;


    private void Start()
    {
        
    }


    IEnumerator FadeOutPlayerUI(Image image, int startAlpha, int endAlpha, float duration, float delay)
    {
        float elapsedTime = 0f;
        float delayHolder = 0f;      
        Color imageColor = image.color;
       
        while(delayHolder < delay)
        {
            delayHolder += Time.deltaTime;
            Debug.Log(delayHolder);
            yield return null;
        }
       
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                Debug.Log(elapsedTime);
                imageColor.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
                image.color = imageColor;
                yield return null;
            }
       
        
    }


    void HidePlayerUI(Image image, bool fadeIn, float delay)
    {
       if(fadeIn)
        {
            StartCoroutine(FadeOutPlayerUI(image, 0, 1, 5f, delay));
        }
        else
        {
            StartCoroutine(FadeOutPlayerUI(image, 1, 0, 5f, delay));
        }
        
    }
}


