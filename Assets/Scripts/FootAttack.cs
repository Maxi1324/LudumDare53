using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FootAttack : MonoBehaviour
{
    public ParticleSystem pat;
    public ParticleSystem pat1;

    public bool gross = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mao")
        {
            if (gross)
            {
                pat1.Play();
                gross = false;
                new List<Ekelhafter>(FindObjectsOfType<Ekelhafter>()).Where(e => Vector3.Distance(transform.position, e.transform.position) < 13).ToList().ForEach(e=>e.DamageHandle());
            }
            else
            {
                new List<Ekelhafter>(FindObjectsOfType<Ekelhafter>()).Where(e => Vector3.Distance(transform.position, e.transform.position) < 5).ToList().ForEach(e => e.DamageHandle());
                pat.Play();
            }
        }

        if(other.tag == "Checki")
        {
            FindObjectOfType<PlayerMovement1>().Checkpoint();
        }
    }
}
