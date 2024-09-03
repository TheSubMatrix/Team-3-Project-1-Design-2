using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundTrigger : MonoBehaviour
{
    [SerializeField]string soundToPlay;
    bool hasPlayed;
    private void OnTriggerEnter(Collider other)
    {
        if(!hasPlayed && other.gameObject.tag == "Player" && SoundManager.Instance != null) 
        {
            SoundManager.Instance.PlaySoundAtLocation(transform.position, soundToPlay, false);
        }
    }
}
