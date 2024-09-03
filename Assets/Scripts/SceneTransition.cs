using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransition : MonoBehaviour
{

    int sceneCount;
    static SceneTransition instance;
    Animator animator;

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
        sceneCount = SceneManager.sceneCountInBuildSettings;
    }

    private void Start()
    {
        Debug.Log(sceneCount);
    }

    public void ChangeScene(string sceneName)
    {
        
            StartCoroutine(DelayChangeScene(1f, sceneName));
                    
    }

    IEnumerator DelayChangeScene(float seconds, string sceneName)
    {
        Debug.Log("FadeOut");
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName);
       
        Debug.Log("FadeIn");
       
    }

}
