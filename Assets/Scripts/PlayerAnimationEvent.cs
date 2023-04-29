using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public PlayerMovement pl;

    public void DoDamage()
    {
        pl.DoDamage();
    }
}
