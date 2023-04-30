using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor2 : MonoBehaviour
{
    public GameObject Door;


    public void open()
    {
        Destroy(Door);
        Destroy(gameObject);
        FindObjectOfType<Joey>().Door2Destroy();
    }
}
