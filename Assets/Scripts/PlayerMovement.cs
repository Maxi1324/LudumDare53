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

    public float jumpForce = -30;
    public float grav = 3;

    public Transform GroundPos;
    public LayerMask mask;

    private float v;
    private bool stopV = false;

    public CharacterController character;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        var onGround = Physics.CheckSphere(GroundPos.position, 0.5f, mask);

       

        var speedMod = speed;
        var hori = Input.GetAxis("Horizontal");
        var verti = Input.GetAxis("Vertical");

        var mousex = Input.GetAxis("Mouse X");

        var Jump = Input.GetButtonDown("Jump");
        var Sprint = Input.GetButton("LeftShift");


        if (Sprint)
        {
            speedMod *= SprintMult;
        }
        if (onGround)
        {
            v = -0;
        }
        else
        {
            v += grav * Time.deltaTime ;
        }
        if (Jump)
        {
            v = jumpForce;
        }

        
        character.Move(Vector3.down * v *Time.deltaTime);

        if (Mathf.Abs(hori) > deadZone)
        {
            float lSpeed = speedMod * 0.65f;
            character.Move(transform.right * hori * lSpeed * Time.deltaTime);
        }
        if (Mathf.Abs(verti) > deadZone)
        {
            float lSpeed = speedMod*((verti > 0) ? 1 : 0.5f);
            character.Move(transform.forward * verti * lSpeed * Time.deltaTime);
        }

        if(Mathf.Abs(mousex) > DeadZoneMouse)
        {
            transform.Rotate(0, MouseSpeedX * mousex*Time.deltaTime,0);
        }

    }
}
