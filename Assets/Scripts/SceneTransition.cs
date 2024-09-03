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

    public void ChangeScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex < sceneCount-1)
        {
            StartCoroutine(DelayChangeScene(1f, currentSceneIndex));
        }             
    }

    IEnumerator DelayChangeScene(float seconds, int sceneIndex)
    {
        Debug.Log("FadeOut");
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneIndex + 1);
       
        Debug.Log("FadeIn");
       
    }

}
