using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;



    private List<GameObject> enm = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(dingsi());
    }

    public IEnumerator dingsi()
    {
        while (true)
        {
            enm = enm.Where(e => e != null).ToList();
            if(enm.Count <= 3)
            {
                yield return new WaitForSeconds(3);
                enm.Add(Instantiate(enemy, transform.position, Quaternion.identity));
            }
            yield return null;
        }
    }

}
