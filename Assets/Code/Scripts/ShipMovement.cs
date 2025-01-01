using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour
{
    //ShipControls shipControls;
    public Rigidbody rigidBodyRef;

    private float currentSpeed;
    public float forwardSpeed;
    public float backwardSpeed;
    public float turningSpeed;

    public Vector2 movementInput;

    private Vector3 DesiredRotation;
    private Vector3 ActualRotation;

    private Vector3 DesiredVelocity;
    private Vector3 ActualVelocity;

    public float accelerateDelay;
    public float turnDelay;

    private Vector3 velocity = Vector3.zero;

    /*private void OnEnable()
    {
        if(shipControls == null)
        {
            shipControls = new ShipControls();
            shipControls.Enable();
            shipControls.Ship.Move.performed += context => movementInput = context.ReadValue<Vector2>();
        }
    }

    private void OnDisable()
    {
        shipControls.Disable();
    }*/

    private void FixedUpdate()
    {
        if ((Input.GetKey(KeyCode.W)) && !(Input.GetKey(KeyCode.S)))
        {
            currentSpeed = Mathf.Lerp(currentSpeed, forwardSpeed, accelerateDelay * Time.fixedDeltaTime);
        }

        if ((Input.GetKey(KeyCode.S)) && !(Input.GetKey(KeyCode.W)))
        {
            currentSpeed = Mathf.Lerp(currentSpeed, backwardSpeed, accelerateDelay * Time.fixedDeltaTime);
        }

        if ((!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)))
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, accelerateDelay * Time.fixedDeltaTime);
        }




        if ((Input.GetKey(KeyCode.A)) && !(Input.GetKey(KeyCode.D)))
        {
            DesiredRotation = Vector3.SmoothDamp(DesiredRotation, new Vector3 (0, -turningSpeed, 0), ref velocity, turnDelay * Time.fixedDeltaTime);
        }

        if ((Input.GetKey(KeyCode.D)) && !(Input.GetKey(KeyCode.A)))
        {
            DesiredRotation = Vector3.SmoothDamp(DesiredRotation, new Vector3(0, turningSpeed, 0), ref velocity, turnDelay * Time.fixedDeltaTime);
        }

        if ((!(Input.GetKey(KeyCode.A)) && !(Input.GetKey(KeyCode.D))) || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
        {
            DesiredRotation = Vector3.SmoothDamp(DesiredRotation, new Vector3(0, 0, 0), ref velocity, turnDelay * Time.fixedDeltaTime);
        }



        //Add force/torque based on user inputs
        rigidBodyRef.AddTorque(DesiredRotation * Time.fixedDeltaTime, ForceMode.Acceleration);
        rigidBodyRef.AddForce(-transform.right * currentSpeed * Time.fixedDeltaTime, ForceMode.Acceleration);
    }
}
