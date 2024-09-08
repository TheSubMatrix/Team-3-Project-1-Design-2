using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/Channels/Image Display Channel")]
public class SO_ImageDisplayChannel : ScriptableObject
{
    [System.Serializable]
    public class ImageDisplayInfo 
    {
        public ImageDisplayInfo(string image, int startAlpha, int endAlpha, float duration, float delay) 
        {
            this.image = image;
            this.startAlpha = startAlpha;
            this.endAlpha = endAlpha;
            this.duration = duration;
            this.delay = delay;
        }
        public string image;
        public int startAlpha;
        public int endAlpha;
        public float duration;
        public float delay;
    }
    [System.Serializable]
    public class ChangeImageEvent : UnityEvent<ImageDisplayInfo>
    { }
    public ChangeImageEvent OnFadeImage;
}
