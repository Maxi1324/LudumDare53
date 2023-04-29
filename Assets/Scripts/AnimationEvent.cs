using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public Joey joey;

    public void OnSchalter()
    {
        joey.AfterSchalterumgelegt();
    }
}
