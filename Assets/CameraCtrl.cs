using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public void RotateViewClockwise()
    {
        transform.Rotate(Vector3.up ,- 90f,Space.World);
    }
    public void RotateViewAnticlockwise()
    {
        transform.Rotate(Vector3.up, 90f, Space.World);
    }
}
