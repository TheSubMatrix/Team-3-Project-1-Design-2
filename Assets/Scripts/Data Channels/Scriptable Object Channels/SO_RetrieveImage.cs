using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Scriptable Objects/Channels/Start Retriece Image Channel")]
public class SO_RetrieveImage : ScriptableObject
{
    public UnityEvent<string> locateImage; //Subscribed in Player UI script
   
    public UnityEvent<Image> returnImage;


}
