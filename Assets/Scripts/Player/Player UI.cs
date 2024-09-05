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
        StartCoroutine(FadeOutPlayerUI(playerUIImages[0], 1, 0, 5, 10f));
    }


    IEnumerator FadeOutPlayerUI(Image image, int startAlpha, int endAlpha, float duration, float delay)
    {
        float elapsedTime = 0f;
        elapsedTime += Time.deltaTime;
        Debug.Log(elapsedTime);
        Color imageColor = image.color;
        if(elapsedTime > delay)
        {
            while (elapsedTime < duration)
            {
               
                Debug.Log(elapsedTime);
                imageColor.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
                image.color = imageColor;
                yield return null;
            }
        }
        
    }


    void HidePlayerIU(Image image, int startAlpha, int endAlhpa, float duration, bool fadeIn)
    {
       if(fadeIn)
        {
            StartCoroutine(FadeOutPlayerUI(image, 0, 1, 5f, 10f));
        }
        else
        {
            StartCoroutine(FadeOutPlayerUI(image, 1, 0, 5f, 10f));
        }
        
    }
}


