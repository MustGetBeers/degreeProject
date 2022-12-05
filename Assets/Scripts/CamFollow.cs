using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;

    void FixedUpdate()
    {
        // Rotate the camera every frame so it keeps looking at the target
        transform.LookAt(target);

    }
}
