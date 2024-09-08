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

    public void ChangeScene(float fadoutSeconds, float fadeInSeconds, string sceneNameOne, string sceneNameTwo, bool startOfGame = false)
    {
        if(startOfGame)
        {
            StartCoroutine(StartLevelOneScene(fadoutSeconds,fadeInSeconds, sceneNameOne, sceneNameTwo));
        }
        else
        {
            StartCoroutine(DelayChangeScene(fadoutSeconds, fadeInSeconds, sceneNameOne));
        }
            
                    
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


    IEnumerator StartLevelOneScene(float fadeOutSeconds, float fadeInSeconds, string sceneOneName,string sceneTwoName)
    {

        Debug.Log("FadeOut");
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(fadeOutSeconds);
        SceneManager.LoadScene(sceneOneName);
        SceneManager.LoadScene(sceneTwoName, LoadSceneMode.Additive);
        yield return new WaitForSeconds(fadeInSeconds);
        animator.SetTrigger("FadeIn");
        Debug.Log("FadeIn");
    }


}
