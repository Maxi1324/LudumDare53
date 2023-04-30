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

    public GameObject giIntoRobot;

    public GameObject Wand1Eingang;

    private int Schalter = 0;

    void Start()
    {
        anim.SetBool("talk", true);
        TextManager.instance.ShowText("Dude, you finally arrived", 1);
        TextManager.instance.ShowText("You know we have some work to do?", 1);
        TextManager.instance.ShowText("Take that energy thingy",1);
    }

    private void LateUpdate()
    {
    }

    public void EnergyThingyAufgehoben()
    {
        TextManager.instance.ShowText("Well done, now follow me", 1,() =>
        {
            anim.SetBool("talk", false);
            Agent.SetDestination(Schalter1.position);
            StartCoroutine(WaitTillThere(() =>
            {
                StartCoroutine(PressSchalter());

            }, Schalter1.position));
        });
    }

    public void ScrewsCollected()
    {
        TextManager.instance.ShowText("Well done", 1);
        TextManager.instance.ShowText("Come back", 1);
        StartCoroutine(LastTimeWaitiForPlayer());
    }

    IEnumerator PressSchalter()
    {
        anim.SetBool("PressSchalter", true);
        yield return null;
        anim.SetBool("PressSchalter", false);
    }

    public void CotrollCollect()
    {
        if (!Door2Weg)
        {
            TextManager.instance.ShowText("Well done don't forget to destroy the Button", 1);
        }
        ControllCollect = true;

        if (Door2Weg)
        {
            TextManager.instance.ShowText("You've done it, come back", 1);
            StartCoroutine(WaitiForPlayer());
        }
    }

    public bool Door2Weg;
    public bool ControllCollect;

    public void Door2Destroy()
    {
        Door2Weg = true;
        if (!ControllCollect)
        {
            TextManager.instance.ShowText("Well done, don't forget to collect the controll Module", 1);
        }

        if (ControllCollect)
        {
            TextManager.instance.ShowText("You've done it, come back", 1);
            StartCoroutine(WaitiForPlayer());
        }
    }

    public IEnumerator WaitiForPlayer()
    {
        Transform trans = FindObjectOfType<PlayerMovement>().transform;
        while (Vector3.Distance(Vec3dTo2d(transform.position), Vec3dTo2d(trans.position)) > 25)
        {
            yield return null;
        }
        TextManager.instance.ShowText("We need more parts", 1);
        TextManager.instance.ShowText("We need 8 screws", 1);
        TextManager.instance.ShowText("They are all over the place", 1);


    }


    public IEnumerator LastTimeWaitiForPlayer()
    {
        giIntoRobot.SetActive(true);
        Transform trans = FindObjectOfType<PlayerMovement>().transform;
        while (Vector3.Distance(Vec3dTo2d(transform.position), Vec3dTo2d(trans.position)) > 25)
        {
            yield return null;
        }
        TextManager.instance.ShowText("We have repaired the Robot", 1);
        TextManager.instance.ShowText("Go into it", 1);
    }


    public void AfterSchalterumgelegt()
    {
        if(Schalter == 0)
        {
            if(Wand1Eingang != null)Wand1Eingang.SetActive(false);
            Agent.SetDestination(Roboter.position);
            StartCoroutine(WaitTillThere(() =>
            {
                anim.SetBool("talk", true);
                TextManager.instance.ShowText("This is a Roboter", 1);
                TextManager.instance.ShowText("We need to repair it, in order to deliver the Engergy thingy", 1);
                TextManager.instance.ShowText("Search for the Controller Module", 1);

                TextManager.instance.ShowText("Pleace remember, that you can defende your self", 1);
                TextManager.instance.ShowText("You have a Sword", 1);
                TextManager.instance.ShowText("Press the Left Mouse Button, or hold it.", 1);
                TextManager.instance.ShowText("By the way you need to destroy the Button next to the Controller module", 1);
                TextManager.instance.ShowText("Just Hit it with you Sword", 1, () =>
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
        while (Vector3.Distance(Vec3dTo2d(transform.position), Vec3dTo2d(position)) > 2)
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
