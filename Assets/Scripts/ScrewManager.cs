using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewManager : MonoBehaviour
{
    public static ScrewManager instance;
    public List<GameObject> Screws;
    public List<GameObject> Screwiiiii;

    private int ScrewState;

    public ScrewManager()
    {
        instance = this;
    }

    private void Start()
    {
        Screws.ForEach(s => s.SetActive(false));
    }

    public void inc()
    {
        Screws[ScrewState].SetActive(true);
        Screwiiiii[ScrewState].SetActive(true);

        ScrewState += 1;
        if(ScrewState == Screws.Count)
        {
            FindObjectOfType<Joey>().ScrewsCollected();
        }
    }
}
