using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 100;
    public float SprintMult = 1.3f;
    public float deadZone = 0.1f;
    public float DeadZoneMouse = 0.1f;
    public float MouseSpeedX = 100;

    void Start()
    {
        
    }

    void Update()
    {
        var speedMod = speed;
        var hori = Input.GetAxis("Horizontal");
        var verti = Input.GetAxis("Vertical");

        var mousex = Input.GetAxis("Mouse X");


        var Sprint = Input.GetButton("LeftShift");

        if (Sprint)
        {
            speedMod *= SprintMult;
        }

        if(Mathf.Abs(hori) > deadZone)
        {
            float lSpeed = speedMod * 0.65f;
            transform.position += transform.right * hori*lSpeed*Time.deltaTime;
        }
        if (Mathf.Abs(verti) > deadZone)
        {
            float lSpeed = speedMod*((verti > 0) ? 1 : 0.5f);
            transform.position += transform.forward * verti * lSpeed * Time.deltaTime;
        }

        if(Mathf.Abs(mousex) > DeadZoneMouse)
        {
            transform.Rotate(0, MouseSpeedX * mousex*Time.deltaTime,0);
        }
    }
}
