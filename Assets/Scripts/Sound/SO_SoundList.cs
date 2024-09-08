using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound List", menuName = "Scriptable Objects/SoundList")]
public class SO_SoundList : ScriptableObject
{
    public Sound[] soundList;
}