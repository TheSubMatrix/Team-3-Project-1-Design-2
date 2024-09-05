using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> displayedText;

   // float fadeDuration = 5f;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(FadeOutPlayerUI(displayedText[0], 1,0,2));
        
        
        }

            
    }

    IEnumerator FadeOutPlayerUI(TMP_Text text, int startAlpha, int endAlpha, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            text.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime/duration);
            Debug.Log(elapsedTime);
            yield return null;
        }

    }
}
