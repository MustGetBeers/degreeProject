using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkiController : MonoBehaviour
{
    public Rigidbody rb;
    public float addForceValue;

    // Update is called once per frame
    void Update()
    {


    }


    public void OnFire()
    {
        rb.AddForce(Vector3.forward * addForceValue);
    }
}
