using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] AudioClip buttonClickSFX;

    
   

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        SceneManager.LoadScene("Level Two Destroyed",LoadSceneMode.Additive);

    }
    private void Start()
    {
        audioSource.Play();   
    }

    private void Update()
    {
     if(SceneManager.GetActiveScene().name == "Level One")
        {
            Destroy(this);
        }   
    }
    public void WhenStartButtonisClicked()
    {
        audioSource.PlayOneShot(buttonClickSFX);

        SceneTransition.Instance.ChangeScene(1, 5, "Level One", "Player Scene", true);

        /*SceneManager.LoadScene("Level One");
        SceneManager.LoadScene("Player Scene", LoadSceneMode.Additive);*/
        

    }

    public void WhenHelpButtonisClicked()
    {
        audioSource.PlayOneShot(buttonClickSFX);
        SceneTransition.Instance.ChangeScene(1,1,"Help", null);
       // SceneManager.LoadScene("Help");

    }
    public void WhenCreditsButtonisClicked()
    {
        // SceneManager.LoadScene("Credits");
        audioSource.PlayOneShot(buttonClickSFX);
        SceneTransition.Instance.ChangeScene(1,1,"Credits", null);
    }
    public void WhenReplayButtonisClicked()
    {
        audioSource.PlayOneShot(buttonClickSFX);
        SceneTransition.Instance.ChangeScene(1,1,"MainMenu", null);
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
        SceneTransition.Instance.ChangeScene(1,1,"MainMenu", null);
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
   
}
