using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{


   

    public void WhenStartButtonisClicked()
    {
        SceneTransition.Instance.ChangeScene("MileStone1");
       // SceneManager.LoadScene("MileStone1");

    }

    public void WhenHelpButtonisClicked()
    {

        SceneTransition.Instance.ChangeScene("Help");
        //SceneManager.LoadScene("Help");

    }
    public void WhenCreditsButtonisClicked()
    {
       // SceneManager.LoadScene("Credits");
        SceneTransition.Instance.ChangeScene("Credits");
    }
    public void WhenReplayButtonisClicked()
    {
        SceneTransition.Instance.ChangeScene("MainMenu");
       // SceneManager.LoadScene("MainMenu");

    }
    public void WhenQuitButtonisClicked()
    {

        Application.Quit();

        

        Debug.Log("You have quit the game");

    }

    public void WhenBackButtonisClicked()
    {
        SceneTransition.Instance.ChangeScene("MainMenu");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

   
}
