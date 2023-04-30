using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
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

    public LayerMask Watermask;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    public FootAttack f1;
    public FootAttack f2;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pos = transform.position;
        rot = transform.rotation;
    }

    public void Checkpoint()
    {
        pos = transform.position;
        rot = transform.rotation;
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
        Vector3 dir = new Vector3(hori, 0, verti);

        var Sprint = Input.GetButton("LeftShift");
        if (Sprint)
        {
            Anim.SetBool("Run", true);
        }
        else
        {
            Anim.SetBool("Run", false);
        }

        if(dir.magnitude > 0.1f)
        {
            Anim.SetBool("Walk", true);
        }
        else
        {
            Anim.SetBool("Walk", false);
        }

        var Fire1 = Input.GetButton("Fire1");
        Fire1 = false;

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
        if (Jump && Physics.CheckSphere(GroundPos.position, 2.3f, mask))
        {
            character.Move(new Vector3(0, 2, 0));
            v = jumpForce;
            Anim.SetBool("Jump", true);

            f1.gross = true;
            f2.gross = true;
        }
        else
        {
            Anim.SetBool("Jump", false);
        }

        if (Fire1)
        {
            Anim.SetBool("Attack", true);
        }
        else
        {
            Anim.SetBool("Attack", false);
        }


        if(dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + DamageStart.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            float lSpeed = speedMod;

            character.Move(moveDir.normalized * lSpeed * Time.deltaTime);
        }

        character.Move(Vector3.down * v *Time.deltaTime);


        var water = Physics.CheckSphere(GroundPos.position, .4f, Watermask);
        if (water)
        {
            Debug.Log("jer");
                transform.position = pos;
                transform.rotation = rot;
                TextManager.instance.ShowText("The Water is poisonous", 1);
                TextManager.instance.ShowText("Are you okay?", 1);
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

    public Vector3 pos;
    public Quaternion rot;


    public void HandleDamage()
    {
        transform.position = pos;
        transform.rotation = rot;
        TextManager.instance.ShowText("Ýou've got hit", 1);
        TextManager.instance.ShowText("Are you okay?", 1);
        Debug.Log("hit");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            transform.position = pos;
            transform.rotation = rot;
            TextManager.instance.ShowText("The Water is poisonous", 1);
            TextManager.instance.ShowText("Are you okay?", 1);
        }
        Debug.Log("jer12");

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("jer");
        if (other.gameObject.tag == "Water")
        {
            transform.position = pos;
            transform.rotation = rot;
            TextManager.instance.ShowText("The Water is poisonous", 1);
            TextManager.instance.ShowText("Are you okay?", 1);
        }
    }
}
