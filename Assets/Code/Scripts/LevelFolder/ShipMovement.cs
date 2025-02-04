using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    //References
    private Rigidbody rigidBodyRef;
    private GameObject TurningWheel;
    private PlayerInputManager PlayerInputManager;

    //Adjustable movement parameters
    public float turningSpeed;
    public float maxForwardSpeed;
    public float maxBackwardSpeed;
    public float desiredSpeedChangeMultiplier;
    public float turnDelay;
    public float CollidedMultiplier = 0.7f;
    public float WheelTurnSpeed;

    //List of Floater points
    public Floater[] Floaters;

    //Parameters used for movement calculation
    private Vector3 desiredRotation;
    private Vector3 velocity = Vector3.zero;
    private int collidingElements = 0;
    private float currentCollidedMultiplier = 1;
    [HideInInspector]
    public float currentDesiredSpeed;


    private void Start()
    {
        //Get references, hide cursor
        rigidBodyRef = GetComponent<Rigidbody>();
        TurningWheel = GameObject.FindWithTag("TurningWheel");
        PlayerInputManager = GetComponent<PlayerInputManager>();
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        //Determine desired forward/backward speed
        if (PlayerInputManager.movementInput.y != 0)
        {
            currentDesiredSpeed += (desiredSpeedChangeMultiplier * PlayerInputManager.movementInput.y) * Time.fixedDeltaTime;

            //Clamp to min/max values
            if (currentDesiredSpeed > maxForwardSpeed) { currentDesiredSpeed = maxForwardSpeed; }
            if (currentDesiredSpeed < maxBackwardSpeed) { currentDesiredSpeed = maxBackwardSpeed; }
        }

        //Determine desired rotation force
        if (PlayerInputManager.movementInput.x != 0) 
        { desiredRotation = Vector3.SmoothDamp(desiredRotation, new Vector3(0, (PlayerInputManager.movementInput.x) * turningSpeed, 0), ref velocity, turnDelay * Time.fixedDeltaTime); }
        else 
        { 
            //If no sideway input, reduce desiredRotation and round towards 0 if below threshhold
            desiredRotation = Vector3.SmoothDamp(desiredRotation, new Vector3(0, 0, 0), ref velocity, turnDelay * Time.fixedDeltaTime);
            if(Vector3.Magnitude(desiredRotation) <= 2)
            { desiredRotation = new Vector3(0, 0, 0); }
        }


        //Lower user input force if currently colliding
        if (collidingElements > 0) { currentCollidedMultiplier = CollidedMultiplier; }
        else { currentCollidedMultiplier = 1; }

        //Add torque based on user inputs
        rigidBodyRef.AddTorque(desiredRotation * CollidedMultiplier * Time.fixedDeltaTime, ForceMode.Acceleration);

        //Apply proportional force for each submerged floater based on user inputs
        foreach (var Floater in Floaters)
        {
            if(Floater.isSubmerged || collidingElements > 0)
            {
                rigidBodyRef.AddForce((-transform.right * currentDesiredSpeed * currentCollidedMultiplier * Time.fixedDeltaTime) / Floaters.Length, ForceMode.Acceleration);
            }
        }

        //Turn wheels proportional to current desired velocity
        TurningWheel.transform.Rotate(-Vector3.up * Time.deltaTime * (currentDesiredSpeed * WheelTurnSpeed), Space.Self);

        //Rehide cursor on enter window after alt + tab
        if (Input.GetMouseButtonDown(0) | Input.GetMouseButtonDown(1))
        {
            Cursor.visible = false;
        }
    }


    //Colliding elements reduce desired speed
    private void OnCollisionEnter(Collision collision)
    {
        if (!((collision.gameObject.tag == ("Ocean")) | (collision.gameObject.tag == ("Player"))))
        { collidingElements++; }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!((collision.gameObject.tag == ("Ocean")) | (collision.gameObject.tag == ("Player"))))
        { currentDesiredSpeed = currentDesiredSpeed * (1 - (((1 - 0.2f) / collidingElements)) * Time.deltaTime); }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!((collision.gameObject.tag == ("Ocean")) | (collision.gameObject.tag == ("Player"))))
        { collidingElements--; }
    }
}

