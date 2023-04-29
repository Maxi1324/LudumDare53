using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Joey : MonoBehaviour
{
    public NavMeshAgent Agent;
    public Animator anim;

    public Transform Schalter1;
    public Transform Roboter;


    public GameObject Wand1Eingang;

    private int Schalter = 0;

    void Start()
    {
        anim.SetBool("talk", true);
        TextManager.instance.ShowText("Dude, you finally arrived", 1);
        TextManager.instance.ShowText("You know we have some work to do?", 1);
        TextManager.instance.ShowText("Take that energy thingy and follow me", 1,()=>
        {
            anim.SetBool("talk", false);
            Agent.SetDestination(Schalter1.position);
            StartCoroutine(WaitTillThere(() =>
           {
               anim.SetBool("Schalter", true);
               anim.SetBool("Schalter", false);
               Wand1Eingang.SetActive(false);
           }, Schalter1.position));
        });
    }

    public void AfterSchalterumgelegt()
    {
        if(Schalter == 0)
        {
            Agent.SetDestination(Roboter.position);
            StartCoroutine(WaitTillThere(() =>
            {
                anim.SetBool("talk", true);
                TextManager.instance.ShowText("This is a Roboter", 1);
                TextManager.instance.ShowText("We need to repair it, in order to deliver the Engergy thingy", 1);
                TextManager.instance.ShowText("Search for the Controller Module", 1);

                TextManager.instance.ShowText("Pleace remember, that you can defende your self", 1);
                TextManager.instance.ShowText("You have a Sword", 1);
                TextManager.instance.ShowText("Press the Left Mouse Button, or hold it.", 1, () =>
                {
                    anim.SetBool("talk", false);
                });


            }, Roboter.position));
        }
        Schalter++;
    }

    void Update()
    {
        if(Agent.velocity.magnitude > 0.01)
        {
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }

    IEnumerator WaitTillThere(Action action, Vector3 position)
    {
        while (Vector3.Distance(Vec3dTo2d(transform.position), Vec3dTo2d(position)) > 5)
        {
            yield return null;
        }

        action();
    }


    public static Vector3 Vec3dTo2d(Vector3 vec)
    {
        return new Vector3(vec.x, 0, vec.z);
    }
}
