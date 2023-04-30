using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllDingsMitNehmen : MonoBehaviour
{
    public GameObject cont;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
            FindObjectOfType<Joey>().CotrollCollect();
            cont.SetActive(true);
        }
    }
}
