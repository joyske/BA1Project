using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour
{
    //ShipControls shipControls;
    public Rigidbody rigidBodyRef;

    public float forwardSpeed;
    public float backwardSpeed;
    public float turningSpeed;

    public Vector2 movementInput;

    private Vector3 DesiredRotation;
    private Vector3 ActualRotation;

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
        if(Input.GetKey(KeyCode.W))
        {
            rigidBodyRef.AddForce(-transform.right * Time.fixedDeltaTime * forwardSpeed, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rigidBodyRef.AddForce(-transform.right * Time.fixedDeltaTime * backwardSpeed, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.A))
        {
            DesiredRotation = new Vector3(0, -turningSpeed * Time.fixedDeltaTime, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            DesiredRotation = new Vector3(0, turningSpeed * Time.fixedDeltaTime, 0);
        }

        if (!(Input.GetKey(KeyCode.A)) && !(Input.GetKey(KeyCode.D)))
        {
            DesiredRotation = new Vector3(0, 0, 0);
        }

        //Interpolate desired and actual rotation vector
        ActualRotation = Vector3.SmoothDamp(ActualRotation, DesiredRotation, ref velocity, turnDelay * Time.fixedDeltaTime);
        transform.Rotate(ActualRotation);


    }
}
