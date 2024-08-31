using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public class DialogueEvent : UnityEvent<DialogueInfo> { }
    DialogueEvent m_DialogueEvent;

    [field:SerializeField] public List<DialogueInfo> DialogueOptions { get; private set; }
    public void StartDialogue(string dialogueToStart)
    {
        DialogueInfo infoToPlay = DialogueOptions.Find(e => e.name == dialogueToStart);
        if (infoToPlay != null)
        {
            m_DialogueEvent.Invoke(infoToPlay);
        }
    }
}
[System.Serializable]
public class DialogueInfo
{
    public string name;
    public AudioClip soundToPlay;
    public float timeToDisplayFor;
    public string textForClip;
}