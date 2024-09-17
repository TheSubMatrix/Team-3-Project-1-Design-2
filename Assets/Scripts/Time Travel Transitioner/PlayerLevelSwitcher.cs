using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevelSwitcher : MonoBehaviour
{
    bool m_shouldFinishTransition = false;
    [SerializeField] SO_VoidChannel m_updatePageCountChannel;
    [SerializeField] SO_ImageDisplayChannel imageDisplayChannel;
    [SerializeField] SO_BoolChannel disablePlayerMovementChannel;
    [SerializeField] float m_DelayBeforeTransition;
    [SerializeField] SO_ImageDisplayChannel.ImageDisplayInfo imageDisplayInfoEnable;
    [SerializeField] SO_ImageDisplayChannel.ImageDisplayInfo imageDisplayInfoDisable;
    [SerializeField] List<TransitionData> m_transitions = new List<TransitionData>();
    [SerializeField] Animator m_animator;
    int m_journalCount = 0;
    GameObject teleportSFX;
    [System.Serializable]
    struct TransitionData
    {
        public string FromScene;
        public string ToScene;
    }
    private void Awake()
    {
      m_updatePageCountChannel.myEvent.AddListener(UpdatePageCount);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !m_shouldFinishTransition && m_journalCount >= 3)
        {
            StartTransition();
           teleportSFX = SoundManager.Instance.PlaySoundOnObject(gameObject, "Teleport", true);
        }
    }
    public void StartTransition() 
    {
        m_animator.SetTrigger("Start Transition");
        StartCoroutine(TransitionSceneAsync(m_transitions));
    }
    public void FakeTransition()
    {
        m_animator.SetTrigger("Start Transition");
    }
    public void CompleteSceneTransition()
    {
        m_shouldFinishTransition = true;
    }
    public void ResetSceneTransitionStatus()
    {
        m_shouldFinishTransition = false;
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
        disablePlayerMovementChannel.boolEvent?.Invoke(false);
        AsyncOperation asyncSceneLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        asyncSceneLoad.allowSceneActivation = false;
        while (!Mathf.Approximately(asyncSceneLoad.progress, 0.9f) || !m_shouldFinishTransition)
        {
            Debug.Log(asyncSceneLoad.progress);
            yield return null;
        }
        asyncSceneLoad.allowSceneActivation = true;
        yield return new WaitForEndOfFrame();
        yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(currentScene));
        m_animator.SetTrigger("End Transition");
        disablePlayerMovementChannel.boolEvent?.Invoke(true);
        Destroy(teleportSFX);
    }
    void UpdatePageCount()
    {
        m_journalCount++;
        if( m_journalCount >= 3) 
        {
            imageDisplayChannel.OnFadeImage?.Invoke(imageDisplayInfoEnable);
            imageDisplayChannel.OnFadeImage?.Invoke(imageDisplayInfoDisable);
            imageDisplayChannel.OnFadeImage.Invoke(new SO_ImageDisplayChannel.ImageDisplayInfo("Find Journal Entries", 1, 0, .5f, 0));
            //imageDisplayChannel.OnFadeImage.Invoke(new SO_ImageDisplayChannel.ImageDisplayInfo("Time Travel Near Watch", 0, 1, .5f, 0));

        }
    }
}
