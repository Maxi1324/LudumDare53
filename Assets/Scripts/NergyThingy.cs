using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NergyThingy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            FindObjectOfType<Joey>().EnergyThingyAufgehoben();
        }
    }
}
