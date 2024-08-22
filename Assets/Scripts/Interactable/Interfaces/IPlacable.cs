using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlacable
{
    void Place(Vector3 location, Vector3 velocity);
}
