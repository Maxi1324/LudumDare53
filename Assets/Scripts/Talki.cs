using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talki : MonoBehaviour
{
    void Start()
    {
        TextManager.instance.ShowText("Just go ahead", 1);
        TextManager.instance.ShowText("We need to delevier the Engery Thingy", 1);
    }
}
