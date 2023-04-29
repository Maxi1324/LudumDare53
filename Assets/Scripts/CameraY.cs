using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraY : MonoBehaviour
{
    public float MouseSpeedY = 100;
    public float DeadZoneMouse = 0.1f;
    void LateUpdate()
    {
        var mousex = Input.GetAxis("Mouse Y");
        if (Mathf.Abs(mousex) > DeadZoneMouse)
        {
            transform.Rotate( MouseSpeedY * mousex * Time.deltaTime,0, 0);
        }
    }
}
