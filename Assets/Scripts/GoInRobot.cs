using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoInRobot : MonoBehaviour
{
    public GameObject RobotDing;
    public GameObject cami;
    public GameObject RobotMove;
    public GameObject wandi;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            RobotMove.SetActive(true);
            Destroy(RobotDing);
            Destroy(gameObject);
            Destroy(FindObjectOfType<PlayerMovement>().gameObject);
            Destroy(FindObjectOfType<Joey>().gameObject);
            cami.SetActive(true);
            Destroy(wandi);

            TextManager.instance.ShowText("No go forward", 1);

        }
    }
}
