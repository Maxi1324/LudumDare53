using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllDingsMitNehmen : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
            FindObjectOfType<Joey>().CotrollCollect();
        }
    }
}
