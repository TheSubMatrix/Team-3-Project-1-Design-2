using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
   


    public void WhenStartButtonisClicked()
    {

        SceneManager.LoadScene("MileStone1");

    }

    public void WhenHelpButtonisClicked()
    {

        SceneManager.LoadScene("Help");

    }
    public void WhenCreditsButtonisClicked()
    {
        SceneManager.LoadScene("Credits");

    }
    public void WhenReplayButtonisClicked()
    {

        SceneManager.LoadScene("MainMenu");

    }
    public void WhenQuitButtonisClicked()
    {

        Application.Quit();

        

        Debug.Log("You have quit the game");

    }

    public void WhenBackButtonisClicked()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

   
}
