using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class OrgiePorgyController : MonoBehaviour
{
    private const string OrgiNestParrent = "OrgiNestParrent";

    [SerializeField]
    private bool RenderGizmos = false;
    [Header("SearchForNest")]
    [SerializeField]
    private float NestRange = 20;
    [SerializeField]
    private LayerMask MapMask;
    [SerializeField]
    private LayerMask HindernissMask;
    [SerializeField]
    private float NormalMax;
    [SerializeField]
    private float ImWegRange = 2;

    [Header("NestPrefab")]
    [SerializeField]
    private GameObject NestPrefab;
    private OrgiNest NestRef;

    [Header("Agent")]
    [SerializeField]
    private NavMeshAgent Agent;
    [SerializeField]
    private CharacterController CController;
    [SerializeField]
    private float WalkSpeed = 1;



    private void Start()
    {
        PlaceNest(SearchForNestPos());
    }

    private void Update()
    {
        
    }
    
    public Vector3 SearchForNestPos()
    {
        Vector2 hitPosition = Vector2.zero;
        bool positionValid = true;
        int i = 0;
        do
        {
            Vector2 position = transform.position.FlatPosition();
            Vector2 added = new Vector3().RandVec();
            Vector2 Positon2D = position + added;

            var (valid, hit) = CheckPositionForNest(Positon2D);
            positionValid = valid;

            hitPosition = hit.point;
            i++;
        } while (!positionValid && i < 100);

        return hitPosition;
    }

    public bool PlaceNest(Vector3 position)
    {
        if (!CheckPositionForNest(position).Item1)
        {
            return false;
        }

        var Parrent = GameObject.Find(OrgiNestParrent) ??new GameObject(OrgiNestParrent);
        var NestRefObject = Instantiate(NestPrefab, position, Quaternion.identity, Parrent.transform);
        NestRef = NestRefObject.GetComponent<OrgiNest>();
        return true;
    }

    public Tuple<bool, RaycastHit> CheckPositionForNest  (Vector2 Position)
    {
        RaycastHit hit;
        var valid = (Physics.Raycast(Position.Flat2Real() + Vector3.up * 1000000, Vector3.down, out hit,Mathf.Infinity,MapMask) && hit.normal.y > NormalMax && Physics.CheckSphere(hit.point, ImWegRange, HindernissMask));
        return new Tuple<bool, RaycastHit>(valid,hit);
    }
    public void WalkToPoint()
    {
        if (NestRef == null)
        {
            return;
        }
        var NavMeshAgent = Agent;
        NavMeshPath path = new NavMeshPath();
        NavMeshAgent.CalculatePath(Vector3.zero,path);
    }

    private IEnumerator WalkToSubRoutine(NavMeshPath path)
    {
        var queue = new Queue<Vector3>(path.corners);
        while (queue.Count != 0)
        {
            var next = queue.Dequeue();
            while (Vector3.Distance(transform.position.FlatPosition(), next.FlatPosition()) > 0.1f)
            {
                Move((next.FlatPosition() - transform.position.FlatPosition()).normalized);
                yield return null;
            }
        }
    }

    public void Move(Vector2 dir)
    {
        CController.Move(dir * WalkSpeed * Time.deltaTime);
        StopCoroutine("RotSubroutine");
        StartCoroutine("RotSubroutine", dir);
    }

    IEnumerator RotSubroutine(Vector2 dir)
    {
        var rot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.y), Vector3.up);
        while (Quaternion.Angle(transform.rotation, rot) > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, 0.1f);
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        if (RenderGizmos)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, NestRange);
        }
    }
}
public static class Vector3Extension
{
    public static Vector2 FlatPosition(this Vector3 vec)=>new Vector2(vec.x, vec.z);
    public static Vector3 RandVec(this Vector3 vec) {
        System.Random rand = new System.Random();
        return new Vector3((float)rand.NextDouble(),(float)rand.NextDouble(), (float)rand.NextDouble()).normalized;
    }
    public static Vector3 Flat2Real(this Vector2 vec) => new Vector3(vec.x, 0,vec.y);
}