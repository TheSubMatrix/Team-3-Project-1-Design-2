using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void WhenStartButtonisClicked()
    {

        SceneManager.LoadScene("Test Scene");

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
