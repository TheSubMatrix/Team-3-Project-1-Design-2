using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionAnimatonEventHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerLevelSwitcher playerLevelSwitcher;
    public void OnStartAnimationCompleted() 
    {
        playerLevelSwitcher.CompleteSceneTransition();
    }
    public void OnEndAnimationCompleted()
    {
        playerLevelSwitcher.ResetSceneTransitionStatus();
    }
}
