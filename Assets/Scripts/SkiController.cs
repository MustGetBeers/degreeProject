using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkiController : MonoBehaviour
{

    private Vector2 movementDirection;
    private Vector2 lookDirection;

    public Rigidbody rb;
    public ConfigurableJoint hipJoint;
    public Transform head;

    public float speed;
    public float addForceValue;

    // Update is called once per frame
    void Update()
    {
        Move();

        //Commented this out for now whilst I deal with moving a bit
        //Look();
    }


    public void OnFire()
    {
        rb.AddForce(Vector3.up * addForceValue, ForceMode.Force);
    
    }

    public void OnMove(InputValue value)
    {
        // Sets the movement direction from the value of the input value WASD
        movementDirection = value.Get<Vector2>();

    }

    public void OnLook(InputValue value)
    {
        lookDirection = value.Get<Vector2>();
    }

    public void Move()
    {
        // Moves the player
        //transform.Translate(movementDirection.y * speed * Time.deltaTime * Vector3.forward);  This moves the player, but is not really want we want.
        //transform.Translate(movementDirection.x * speed * Time.deltaTime * Vector3.right);

        rb.AddForce(movementDirection.y * addForceValue * Time.deltaTime * Vector3.forward);
        rb.AddForce(movementDirection.x * addForceValue * Time.deltaTime * Vector3.right);

        Vector3 direction = new Vector3(movementDirection.x, 0f, movementDirection.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

            //Not really sure what all this doing, I should get it explained... the targetAngle is deffo wrong here.
            this.hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            this.rb.AddForce(direction * speed * Time.fixedDeltaTime);

        }
    }

    public void Look()
    {

        Vector3 direction = new Vector3(lookDirection.x, 0f, lookDirection.y).normalized;

        float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

        head.rotation = Quaternion.Euler(0f, targetAngle, 0f);
    }
}
