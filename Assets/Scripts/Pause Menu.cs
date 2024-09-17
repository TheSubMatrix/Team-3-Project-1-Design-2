using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;
    public GameObject pausePanel ;


    // Start is called before the first frame update
    void Start()
    {
        Resume();
    }
    
    public void WhenContinueButtonCliked()
    {
        Resume();
    }

    public void WhenQuitButtonClicked()
    {Debug.Log("QuitButton");
        Application.Quit();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (gameIsPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
       gameIsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
       gameIsPaused = true;
    }
}
