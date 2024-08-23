using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevelSwitcher : MonoBehaviour
{
    [SerializeField] float m_transitionDelay = 1f;
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
            StartCoroutine(TransitionSceneAsync(m_transitions, m_transitionDelay));
        }
    }
    IEnumerator TransitionSceneAsync(List<TransitionData> scenesToParse, float transitionDelay)
    {
        string sceneName = null;
        string currentScene = SceneManager.GetActiveScene().name;
        for(int i = 0;  i < scenesToParse.Count; i++)
        {
            
            if(scenesToParse[i].FromScene == currentScene)
            {
                sceneName = scenesToParse[i].ToScene;
            }
        }
        AsyncOperation asyncSceneLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncSceneLoad.allowSceneActivation = false;
        float elapsedTime = 0;
        while(!asyncSceneLoad.isDone && elapsedTime < transitionDelay)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        DontDestroyOnLoad(gameObject);
        asyncSceneLoad.allowSceneActivation = true;
    }
}
