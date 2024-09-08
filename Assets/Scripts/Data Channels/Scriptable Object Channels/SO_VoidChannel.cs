using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/Channels/Void Channel")]
public class SO_VoidChannel : ScriptableObject
{
    public UnityEvent myEvent;
}
