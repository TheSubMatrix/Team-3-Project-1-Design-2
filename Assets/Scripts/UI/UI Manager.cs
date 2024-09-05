using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : MonoBehaviour
{

    private AudioSource audioSource;
    [SerializeField] AudioClip buttonClickSFX;

   

    private void Awake()
    {
        
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        audioSource.Play();   
    }

    private void Update()
    {
     if(SceneManager.GetActiveScene().name == "LevelOne")
        {
            Destroy(this);
        }   
    }
    public void WhenStartButtonisClicked()
    {
        audioSource.PlayOneShot(buttonClickSFX);
        SceneTransition.Instance.ChangeScene(1,5,"Level One");
       // SceneManager.LoadScene("MileStone1");

    }

    public void WhenHelpButtonisClicked()
    {
        audioSource.PlayOneShot(buttonClickSFX);
        SceneTransition.Instance.ChangeScene(1,1,"Help");
       // SceneManager.LoadScene("Help");

    }
    public void WhenCreditsButtonisClicked()
    {
        // SceneManager.LoadScene("Credits");
        audioSource.PlayOneShot(buttonClickSFX);
        SceneTransition.Instance.ChangeScene(1,1,"Credits");
    }
    public void WhenReplayButtonisClicked()
    {
        audioSource.PlayOneShot(buttonClickSFX);
        SceneTransition.Instance.ChangeScene(1,1,"MainMenu");
        //SceneManager.LoadScene("MainMenu");

    }
    public void WhenQuitButtonisClicked()
    {
        audioSource.PlayOneShot(buttonClickSFX);
        Application.Quit();

        

        Debug.Log("You have quit the game");

    }

    public void WhenBackButtonisClicked()
    {
        audioSource.PlayOneShot(buttonClickSFX);
        SceneTransition.Instance.ChangeScene(1,1,"MainMenu");
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

   
}
