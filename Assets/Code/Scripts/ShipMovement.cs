using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public Rigidbody rigidBodyRef;

    public float turningSpeed;
    public float maxForwardSpeed;
    public float maxBackwardSpeed;
    public float desiredSpeedChangeMultiplier;
    
    public float turnDelay;
    public float CollidedMultiplier = 0.7f;
    public float WheelTurnSpeed;

    public Floater[] Floaters;

    private Vector3 velocity = Vector3.zero;
    private int CollidingElements = 0;
    private float CurrentCollidedMultiplier = 1;

    [HideInInspector]
    public float currentDesiredSpeed;

    private GameObject TurningWheel;
    private PlayerInputManager PlayerInputManager;

    private Vector3 DesiredRotation;
    private Vector3 targetVelocity = Vector3.zero;
    private Vector3 deltaVelocity;
    


    //Colliding elements reduce desired speed
    private void OnCollisionEnter(Collision collision)
    {
        if (!((collision.gameObject.tag == ("Ocean")) | (collision.gameObject.tag == ("Player"))))
        { CollidingElements++; }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!((collision.gameObject.tag == ("Ocean")) | (collision.gameObject.tag == ("Player"))))
        { currentDesiredSpeed = currentDesiredSpeed * (1 - (((1 - 0.2f) / CollidingElements)) * Time.deltaTime); }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!((collision.gameObject.tag == ("Ocean")) | (collision.gameObject.tag == ("Player"))))
        { CollidingElements--; }
    }


    private void Start()
    {
        //Get references, hide cursor
        TurningWheel = GameObject.FindWithTag("TurningWheel");
        PlayerInputManager = GetComponent<PlayerInputManager>();

        HideMouseCursor();
    }

    private void FixedUpdate()
    {
        //Determine desired forward/backward speed
        if (PlayerInputManager.movementInput.y != 0)
        {
            currentDesiredSpeed += (desiredSpeedChangeMultiplier * PlayerInputManager.movementInput.y) * Time.fixedDeltaTime;

            if (currentDesiredSpeed > maxForwardSpeed) { currentDesiredSpeed = maxForwardSpeed; }
            if (currentDesiredSpeed < maxBackwardSpeed) { currentDesiredSpeed = maxBackwardSpeed; }
        }

        //Determine desired rotation force
        if (PlayerInputManager.movementInput.x != 0) 
        { DesiredRotation = Vector3.SmoothDamp(DesiredRotation, new Vector3(0, (PlayerInputManager.movementInput.x) * turningSpeed, 0), ref velocity, turnDelay * Time.fixedDeltaTime); }
        else 
        { 
            DesiredRotation = Vector3.SmoothDamp(DesiredRotation, new Vector3(0, 0, 0), ref velocity, turnDelay * Time.fixedDeltaTime);
            if(Vector3.Magnitude(DesiredRotation) <= 2)
            { DesiredRotation = new Vector3(0, 0, 0); }
        }


        //Lower user input force if currently colliding
        if (CollidingElements > 0) { CurrentCollidedMultiplier = CollidedMultiplier; }
        else { CurrentCollidedMultiplier = 1; }

        //Add torque based on user inputs
        rigidBodyRef.AddTorque(DesiredRotation * CollidedMultiplier * Time.fixedDeltaTime, ForceMode.Acceleration);



        targetVelocity = -transform.right * currentDesiredSpeed * CurrentCollidedMultiplier;
        deltaVelocity = targetVelocity - rigidBodyRef.velocity;

        //Apply proportional force for each submerged floater based on user inputs
        foreach (var Floater in Floaters)
        {
            if(Floater.isSubmerged || CollidingElements > 0)
            {
                rigidBodyRef.AddForce((-transform.right * currentDesiredSpeed * CurrentCollidedMultiplier * Time.fixedDeltaTime) / Floaters.Length, ForceMode.Acceleration);
            }
        }



        //Turn wheels proportional to current desired velocity
        TurningWheel.transform.Rotate(-Vector3.up * Time.deltaTime * (currentDesiredSpeed * WheelTurnSpeed), Space.Self);

        //Rehide cursor on enter window after alt + tab
        if (Input.GetMouseButtonDown(0) | Input.GetMouseButtonDown(1))
        {
            HideMouseCursor();
        }
    }

    private void HideMouseCursor()
    {
        Cursor.visible = false;
    }
}

