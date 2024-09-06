using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransition : MonoBehaviour
{

    
    static SceneTransition instance;
    private Animator animator;
   
    public static SceneTransition Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        animator = GetComponent<Animator>();
        
    }

    private void Start()
    {
       
    }

    public void ChangeScene(float fadoutSeconds, float fadeInSeconds, string sceneName)
    {
        
            StartCoroutine(DelayChangeScene(fadoutSeconds, fadeInSeconds, sceneName));
                    
    }

    IEnumerator DelayChangeScene(float fadeOutSeconds, float fadeInSeconds,  string sceneName)
    {

        Debug.Log("FadeOut");
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(fadeOutSeconds);
        SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(fadeInSeconds);
        animator.SetTrigger("FadeIn");
        Debug.Log("FadeIn");




    }

}
