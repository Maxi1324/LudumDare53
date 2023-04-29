using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageHandler : MonoBehaviour
{
    public UnityEvent e;

    public void handle()
    {
        Debug.Log("asd");
        e.Invoke();
    }
}
