using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject obi;
    private bool jalol;

    private void Update()
    {
        if(!jalol && Vector3.Distance(FindObjectOfType<PlayerMovement1>().transform.position,transform.position)< 20)
        {
            obi.SetActive(true);
            jalol = true;
            TextManager.instance.ShowText("We did it", 1);
            TextManager.instance.ShowText("We delivered this stuipd thing", 1);
            TextManager.instance.ShowText("I have no idea why", 1);
            TextManager.instance.ShowText("but well, we did it", 1);

            TextManager.instance.ShowText("Dev: Thanks for playing", 1,()=>
            {
                SceneManager.LoadScene("Start");
            });

        }
    }
}
