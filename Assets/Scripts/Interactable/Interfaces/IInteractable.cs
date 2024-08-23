using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    GameObject gameObject { get; }
    void OnInteractStart();
    void OnInteracting();
    void OnInteractEnd();
}
