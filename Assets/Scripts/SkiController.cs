using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkiController : MonoBehaviour
{
    public GameObject gameManager;

    private Vector2 movementDirection;
    private Vector2 lookDirection;
    private float rightFootTrigger;
    private float leftFootTrigger;

    public Rigidbody rb;
    public Rigidbody rbFootL;
    public Rigidbody rbFootR;
    public ConfigurableJoint hipJoint;
    //public Transform basePlayerTransform;
    public Transform leftFootLookAtPoint;
    public Transform rightFootLookAtPoint;


    public float speed;
    public float addForceValue;
    public Vector3 help;


    private void Start()
    {
        //UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Move();
        //Commented this out for now whilst I deal with moving a bit
        Look();

        RightFoot();
        LeftFoot();

       // WTF();
        LookATME();

    }


    public void OnJump()
    {
        rb.AddForce(Vector3.up * addForceValue, ForceMode.Force);

    }

    public void OnMove(InputValue value)
    {
        // Sets the movement direction from the value of the input value WASD, or left thumbstick
        movementDirection = value.Get<Vector2>();

    }

    public void OnLook(InputValue value)
    {
        lookDirection = value.Get<Vector2>();
    }

    public void OnRightFoot(InputValue value)
    {
        rightFootTrigger = value.Get<float>();
    }

    public void OnLeftFoot(InputValue value)
    {
        leftFootTrigger = value.Get<float>();

    }

    private void OnRestart()
    {
        gameManager.GetComponent<GameManager>().Restarting();

    }


    public void Move()
    {
        //Moves the player, This moves the player, but is not really want we want.
        //transform.Translate(movementDirection.y * speed * Time.deltaTime * Vector3.forward);
        //transform.Translate(movementDirection.x * speed * Time.deltaTime * Vector3.right);

        //rbFootL.AddForce(movementDirection.y * addForceValue * Time.fixedDeltaTime * Vector3.forward);
        //rbFootL.AddForce(movementDirection.x * addForceValue * Time.fixedDeltaTime * Vector3.right);

        // Getting the x component of the movementDirection vector2, which is from the left thumbstick input for moving. Rotating the left foot based on that??
        rbFootL.transform.Rotate(0, -movementDirection.x * (1 + leftFootTrigger), 0);
        rbFootL.transform.Rotate(-movementDirection.y * (1 + leftFootTrigger), 0, 0);
        //delay
        //yield return new WaitForSeconds(1.0f);

        //rbFootR.AddForce(movementDirection.y * addForceValue * Time.fixedDeltaTime * Vector3.forward);
        //rbFootR.AddForce(movementDirection.x * addForceValue * Time.fixedDeltaTime * Vector3.right);


        //I think this stuff is about direction ?
        //Vector3 direction = new Vector3(movementDirection.x, movementDirection.x, movementDirection.y).normalized;

        //if (direction.magnitude >= 0.1f)
        //{
        //    //float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

        //    //Not really sure what all this doing, I should get it explained... the targetAngle is deffo wrong here.
        //    //this.hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

        //    this.rb.AddTorque(speed * Time.fixedDeltaTime * direction);

        //}

    }

    public void Look()
    {

        //Doing the same thing as the Move() method, but for right foot and right thumbstick instead of left.
        rbFootR.transform.Rotate(0, -lookDirection.x * (1 + rightFootTrigger), 0);
        rbFootR.transform.Rotate(-lookDirection.y * (1 + rightFootTrigger), 0, 0);

    }

    public void RightFoot()
    {

        rbFootR.AddRelativeForce(rightFootTrigger * addForceValue * Time.fixedDeltaTime * -Vector3.forward);

    }

    public void LeftFoot()
    {

        rbFootL.AddRelativeForce(leftFootTrigger * addForceValue * Time.fixedDeltaTime * -Vector3.forward);

    }

    public void WTF()
    {
        Vector3 direction = new Vector3(movementDirection.x, movementDirection.x, movementDirection.y).normalized;

        if (direction.magnitude >= 0.1f)
        {

            //basePlayerTransform.Rotate(0f, direction.y, 0f);

            //basePlayerTransform.Rotate(0f, rbFootL.rotation.y, 0f);

            Quaternion deltaRotation = Quaternion.Euler(0f, rbFootL.rotation.y, 0f);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }

    public void LookATME()
    {
        Vector3 direction = (leftFootLookAtPoint.position - rightFootLookAtPoint.position).normalized;

        float distance = Vector3.Distance(leftFootLookAtPoint.position, rightFootLookAtPoint.position);

        Vector3 lookAtPoint = (rightFootLookAtPoint.position + (direction * (distance / 2)));

        Vector3 lookAtDirection = (lookAtPoint - rb.position).normalized;
        lookAtDirection.y = 0;
        lookAtDirection.Normalize();

        rb.rotation = Quaternion.LookRotation(lookAtDirection, Vector3.up);

    }


}
