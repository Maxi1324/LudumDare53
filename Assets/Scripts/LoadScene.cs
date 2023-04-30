using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    private void Update()
    {
        var ob = FindObjectOfType<PlayerMovement1>();
        if(ob != null)
        {
            if(Vector3.Distance(ob.gameObject.transform.position, transform.position) < 10)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
