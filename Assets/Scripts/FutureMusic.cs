using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FutureMusic : MonoBehaviour
{
    private bool arcadePlaying, brokenPlaying;

    private void Start()
    {

        if (SoundManager.Instance != null && SceneManager.GetActiveScene().name == "Level Two Destroyed")
        {
            Debug.Log("Start Broken Music");
            SoundManager.Instance.PlaySoundAtLocation(transform.position, "Destroyed Music", true);

        }
        



    }

   
}
