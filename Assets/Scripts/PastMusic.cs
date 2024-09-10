using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PastMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if ( SoundManager.Instance != null)
        {
            Debug.Log("Start Arcade Music");
            // SoundManager.Instance.PlaySoundAtLocation(transform.position, "Arcade Music", true);
            StartCoroutine(DelayBackgroundMusic());
        }
    }

    IEnumerator DelayBackgroundMusic()
    {
        yield return new WaitForSeconds(1);
        SoundManager.Instance.PlaySoundAtLocation(transform.position, "Arcade Music", true);

    }
    
}
