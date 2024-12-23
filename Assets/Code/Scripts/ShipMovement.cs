using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour
{
    //ShipControls shipControls;
    public Rigidbody rigidBodyRef;

    public float forwardSpeed = 30f;
    public float backwardSpeed = -10f;
    public float turningSpeed = 20f;

    public Vector2 movementInput;

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
            rigidBodyRef.AddForce(-transform.right * Time.fixedDeltaTime * forwardSpeed, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rigidBodyRef.AddForce(-transform.right * Time.fixedDeltaTime * backwardSpeed, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -turningSpeed * Time.fixedDeltaTime, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, turningSpeed * Time.fixedDeltaTime, 0);
        }
    }
}
