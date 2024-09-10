using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/Channels/Bool Channel")]
public class SO_BoolChannel : ScriptableObject
{
    public UnityEvent<bool> boolEvent;
}