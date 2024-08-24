using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Walking Sounds")]

    [SerializeField] public List<AudioClip> walkingSFX;

    public AudioSource audioSource;

    /*public AudioSource AudioManagerReference
    {
        get
        {
            return audioSource;
        }
       
    }*/
    public AudioManager audioManagerReference
    {
        get
        {
            return this;
        }
    }
 
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
    }




}
