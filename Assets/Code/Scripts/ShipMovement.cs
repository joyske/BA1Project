using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public Floater[] Floaters;

    private Vector3 velocity = Vector3.zero;
    private int CollidingElements = 0;
    private float CurrentCollidedMultiplier = 1;
    public float CollidedMultiplier = 0.7f;

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

    private void OnCollisionEnter(Collision collision)
    {
        CollidingElements++;
    }

    private void OnCollisionExit(Collision collision)
    {
        CollidingElements--;
    }

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





        if (CollidingElements > 0) { CurrentCollidedMultiplier = CollidedMultiplier; }
        else { CurrentCollidedMultiplier = .5f; }


        //Add force/torque based on user inputs
        rigidBodyRef.AddTorque(DesiredRotation * CollidedMultiplier * Time.fixedDeltaTime, ForceMode.Acceleration);

        foreach (var Floater in Floaters)
        {

            if(Floater.isSubmerged || CollidingElements > 0)
            {
                rigidBodyRef.AddForce((-transform.right * currentSpeed * CurrentCollidedMultiplier * Time.fixedDeltaTime) / Floaters.Length, ForceMode.Acceleration);


                Debug.Log((-transform.right * currentSpeed * CurrentCollidedMultiplier * Time.fixedDeltaTime) / Floaters.Length);
                //rigidBodyRef.AddForceAtPosition((-transform.right * currentSpeed * Time.fixedDeltaTime) / Floaters.Length, Floater.transform.position, ForceMode.Acceleration);
            }


            //rigidBodyRef.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMulti, 0f), transform.position, ForceMode.Acceleration);
        }

        //rigidBodyRef.AddForce(-transform.right * currentSpeed * Time.fixedDeltaTime, ForceMode.Acceleration);
    }
}

