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

    public Animator Anim;

    public float dis;
    public LayerMask DamageMask;
    public Transform DamageStart;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DoDamage()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, DamageStart.forward, out hit,dis, DamageMask))
        {
            hit.collider.gameObject.GetComponent<DamageHandler>().handle();
        }
        Debug.DrawLine(transform.position, DamageStart.forward * dis);
    }

    void Update()
    {
        var onGround = Physics.CheckSphere(GroundPos.position, 0.5f, mask);

        var speedMod = speed;
        var hori = Input.GetAxis("Horizontal");
        var verti = Input.GetAxis("Vertical");

        var Jump = Input.GetButtonDown("Jump");
        var Sprint = Input.GetButton("LeftShift");

        var Fire1 = Input.GetButton("Fire1");

        if (Sprint)
        {
            speedMod *= SprintMult;
        }
        if (onGround)
        {
            v = 0f;
        }
        else
        {
            v += grav * Time.deltaTime ;
        }
        if (Jump && Physics.CheckSphere(GroundPos.position, 1.3f, mask))
        {
            v = jumpForce;
        }

        if (Fire1)
        {
            Anim.SetBool("Attack", true);
        }
        else
        {
            Anim.SetBool("Attack", false);
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
    }

    private void LateUpdate()
    {
        var mousex = Input.GetAxis("Mouse X");
        if (Mathf.Abs(mousex) > DeadZoneMouse)
        {
            transform.Rotate(0, MouseSpeedX * mousex * Time.deltaTime, 0);
        }
    }
}
