using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuSounds : MonoBehaviour
{
    static MenuSounds instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level One")
        {            
            Destroy(gameObject);
        }
    }
}
