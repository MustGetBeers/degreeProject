using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkiController : MonoBehaviour
{

    private Vector2 movementDirection;
    public float speed;
    public Rigidbody rb;
    public float addForceValue;

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    public void OnFire()
    {
        rb.AddForce(Vector3.forward * addForceValue, ForceMode.Force);
    }

    public void OnMove(InputValue value)
    {
        // Sets the movement direction from the value of the input value WASD
        movementDirection = value.Get<Vector2>();

    }

    public void Move()
    {
        // Moves the player
        //transform.Translate(movementDirection.y * speed * Time.deltaTime * Vector3.forward);  This moves the player, but is not really want we want.
        //transform.Translate(movementDirection.x * speed * Time.deltaTime * Vector3.right);

        rb.AddForce(movementDirection.y * addForceValue * Time.deltaTime * Vector3.forward);
        rb.AddForce(movementDirection.x * addForceValue * Time.deltaTime * Vector3.right);

    }
}
