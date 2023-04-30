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

    public GameObject Parikel;

    public float AttackDistanceHit;
    public LayerMask AttackLayerMask;

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
        var player = FindObjectOfType<PlayerMovement>()?.gameObject;
        if(player == null)
        {
            player = FindObjectOfType<PlayerMovement1>().gameObject;
        }
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

        if (Vector3.Distance(Vec3dTo2d(transform.position), Vec3dTo2d(player.transform.position)) < 7)
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
            yield return new WaitForSeconds(2);
        }
    }


    public IEnumerator Attack(Transform player)
    {
        while (true)
        {
            agent.SetDestination(player.position);
            yield return null;
        }
    }

    public static Vector3 Vec3dTo2d(Vector3 vec)
    {
        return new Vector3(vec.x, 0, vec.z);
    }

    public void DamageHandle()
    {
        Instantiate(Parikel, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void AttackDingsi()
    {
        Debug.Log("Ekelhaft Attack");
        RaycastHit hit;
        if(Physics.Raycast(transform.position-transform.forward,transform.forward,out hit, AttackDistanceHit, AttackLayerMask))
        {
            hit.collider.GetComponent<PlayerMovement>()?.HandleDamage();
        }

        if (Physics.CheckSphere(transform.position + transform.forward, 2.2f, AttackLayerMask)){
            FindObjectOfType<PlayerMovement>()?.HandleDamage();
        }
    }
}
