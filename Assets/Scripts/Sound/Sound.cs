using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip sound;
    public bool isMusic;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.3f, 3f)]
    public float pitch = 1f;
    public string associatedText;
}
