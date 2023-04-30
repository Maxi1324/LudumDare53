using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectScrews : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
            ScrewManager.instance.inc();
        }
    }
}
