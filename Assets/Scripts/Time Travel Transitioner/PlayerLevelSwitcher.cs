using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevelSwitcher : MonoBehaviour
{
    bool shouldFinishTransition = false;
    [SerializeField] float m_DelayBeforeTransition;
    [SerializeField] List<TransitionData> m_transitions = new List<TransitionData>();
    [SerializeField] Animator animator;
    [System.Serializable]
    struct TransitionData
    {
        public string FromScene;
        public string ToScene;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !shouldFinishTransition)
        {
            animator.SetTrigger("Start Transition");
            StartCoroutine(TransitionSceneAsync(m_transitions));
        }
    }
    public void CompleteSceneTransition()
    {
        shouldFinishTransition = true;
    }
    public void ResetSceneTransitionStatus()
    {
        shouldFinishTransition = false;
    }
    IEnumerator TransitionSceneAsync(List<TransitionData> scenesToParse)
    {

        string sceneName = null;
        string currentScene = SceneManager.GetActiveScene().name;
        for (int i = 0; i < scenesToParse.Count; i++)
        {

            if (scenesToParse[i].FromScene == currentScene)
            {
                sceneName = scenesToParse[i].ToScene;
            }
        }
        if( sceneName == null ) 
        {
            yield break;
        }
        AsyncOperation asyncSceneLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        asyncSceneLoad.allowSceneActivation = false;
        while (!Mathf.Approximately(asyncSceneLoad.progress, 0.9f) || !shouldFinishTransition)
        {
            Debug.Log(asyncSceneLoad.progress);
            yield return null;
        }
        asyncSceneLoad.allowSceneActivation = true;
        yield return new WaitForEndOfFrame();
        yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(currentScene));
        animator.SetTrigger("End Transition");
    }
}
