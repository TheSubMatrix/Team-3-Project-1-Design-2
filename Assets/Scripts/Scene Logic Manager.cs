using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLogicManager : MonoBehaviour
{
    [SerializeField] SO_VoidChannel levelOneStartChannel;
    [SerializeField] SO_VoidChannel levelTwoStartChannel;
    [SerializeField] SO_BoolChannel playerMovementStateChannel;
    private void Awake()
    {
        CheckSceneSpecificLogic(SceneManager.GetSceneByName("Level One"), SceneManager.GetSceneByName("Main Menu"));
        SceneManager.activeSceneChanged += CheckSceneSpecificLogic;
    }
    void CheckSceneSpecificLogic(Scene newScene, Scene currentScene)
    {
        if (newScene == SceneManager.GetSceneByName("Level One"))
        {
            playerMovementStateChannel?.boolEvent?.Invoke(false);
            levelOneStartChannel?.myEvent?.Invoke();
        }
        if (newScene == SceneManager.GetSceneByName("Level Two") && currentScene != SceneManager.GetSceneByName("Level Two Destroyed"))
        {
            levelTwoStartChannel?.myEvent?.Invoke();
        }
    }
}
