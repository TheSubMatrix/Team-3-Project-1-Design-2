using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevelSwitcher : MonoBehaviour
{
    [SerializeField] float m_DelayBeforeTransition;
    [SerializeField] List<TransitionData> m_transitions = new List<TransitionData>();
    [System.Serializable]
    struct TransitionData
    {
        public string FromScene;
        public string ToScene;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(TransitionSceneAsync(m_transitions, m_DelayBeforeTransition));
        }
    }
    IEnumerator TransitionSceneAsync(List<TransitionData> scenesToParse, float timeToWait)
    {
        float elapsedTime = 0;
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
        while (!Mathf.Approximately(asyncSceneLoad.progress, 0.9f) || elapsedTime < timeToWait)
        {
            Debug.Log(asyncSceneLoad.progress);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        asyncSceneLoad.allowSceneActivation = true;
        yield return new WaitForEndOfFrame();
        yield return null;
        DontDestroyOnLoad(gameObject);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName(sceneName));
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(currentScene));
        Debug.Log("Done");
    }
}
