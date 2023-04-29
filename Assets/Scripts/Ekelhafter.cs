using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ekelhafter : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;
    public float dis = 20;
    public bool drawSphere = false;

    public float playerNear = 50;

    private IEnumerator normaBev;
    private IEnumerator attackBev;

    private void Start()
    {
        normaBev = Routine();
        StartCoroutine(normaBev);
    }

    private void OnDrawGizmos()
    {
        if (!drawSphere)
        {
            return;
        }
        Gizmos.DrawWireSphere(transform.position, dis);
    }

    private void Update()
    {
        var player = FindObjectOfType<PlayerMovement>().gameObject;
        if (Vector3.Distance(player.transform.position, transform.position)< playerNear)
        {
            if (normaBev != null)
            {
                StopCoroutine(normaBev);
                normaBev = null;
                attackBev = Attack(player.transform);
                StartCoroutine(attackBev);

            }
        }
        else
        {
             if(attackBev != null)
            {
                StopCoroutine(attackBev);
                attackBev = null;
                normaBev = Routine();
                StartCoroutine(normaBev);
            }
        }

        if (agent.velocity.magnitude>0.1)
        {
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }   

        if (Vector3.Distance(Vec3dTo2d(transform.position), Vec3dTo2d(player.transform.position)) < 5)
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }

    public IEnumerator Routine()
    {
        while (true)
        {
            float a = Random.Range(0, 360);
            Vector3 target = transform.position + new Vector3(Mathf.Cos(a), Mathf.Sin(a)) * dis;
            agent.SetDestination(target);
            while (Vector3.Distance(Vec3dTo2d(transform.position), Vec3dTo2d(target)) > 5)
            {
                yield return null;
            }
            yield return null;
        }
    }

    public IEnumerator Attack(Transform player)
    {
        while (true)
        {
            agent.SetDestination(player.position);
         
        }
    }

    public static Vector3 Vec3dTo2d(Vector3 vec)
    {
        return new Vector3(vec.x, 0, vec.z);
    }
}
