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

    //private float currentSpeed;
    //public float forwardSpeed 1100;
    //public float backwardSpeed -550;
    
    public float turningSpeed;

    public float maxForwardSpeed;
    public float maxBackwardSpeed;
    public float desiredSpeedChangeMultiplier;
    public float currentDesiredSpeed;

    //public Vector2 movementInput;

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

    private GameObject TurningWheel;
    public float WheelTurnSpeed;

    private PlayerInputManager PlayerInputManager;

    private void OnCollisionEnter(Collision collision)
    {
        CollidingElements++;
    }

    private void OnCollisionExit(Collision collision)
    {
        CollidingElements--;
    }


    private void Start()
    {
        TurningWheel = GameObject.FindWithTag("TurningWheel");
        PlayerInputManager = GetComponent<PlayerInputManager>();

        HideMouseCursor();
    }

    private void FixedUpdate()
    {
        if (PlayerInputManager.movementInput.y != 0)
        {
            currentDesiredSpeed += (desiredSpeedChangeMultiplier * PlayerInputManager.movementInput.y) * Time.fixedDeltaTime;

            if (currentDesiredSpeed > maxForwardSpeed) { currentDesiredSpeed = maxForwardSpeed; }
            if (currentDesiredSpeed < maxBackwardSpeed) { currentDesiredSpeed = maxBackwardSpeed; }
        }

        if (PlayerInputManager.movementInput.x != 0) 
        { DesiredRotation = Vector3.SmoothDamp(DesiredRotation, new Vector3(0, (PlayerInputManager.movementInput.x) * turningSpeed, 0), ref velocity, turnDelay * Time.fixedDeltaTime); }
        else 
        { DesiredRotation = Vector3.SmoothDamp(DesiredRotation, new Vector3(0, 0, 0), ref velocity, turnDelay * Time.fixedDeltaTime); }



        //if ((Input.GetKey(KeyCode.W)) && !(Input.GetKey(KeyCode.S)))
        //{
        //    if (currentDesiredSpeed <= 0) { currentDesiredSpeed += ((desiredSpeedChangeMultiplier * 1.5f) * Time.fixedDeltaTime); }
        //    else { currentDesiredSpeed += (desiredSpeedChangeMultiplier * Time.fixedDeltaTime); }
            
        //    if (currentDesiredSpeed > maxForwardSpeed) { currentDesiredSpeed = maxForwardSpeed; }

            //currentSpeed = Mathf.Lerp(currentSpeed, forwardSpeed, accelerateDelay * Time.fixedDeltaTime);
        //}

        //if ((Input.GetKey(KeyCode.S)) && !(Input.GetKey(KeyCode.W)))
        //{
        //    if (currentDesiredSpeed >= 0) { currentDesiredSpeed -= ((desiredSpeedChangeMultiplier * 1.5f) * Time.fixedDeltaTime); }
        //    else { currentDesiredSpeed -= (desiredSpeedChangeMultiplier * Time.fixedDeltaTime); }

            
        //    if (currentDesiredSpeed < maxBackwardSpeed) { currentDesiredSpeed = maxBackwardSpeed; }

            //currentSpeed = Mathf.Lerp(currentSpeed, backwardSpeed, accelerateDelay * Time.fixedDeltaTime);
        //}

        //if ((!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)))
        //{
            
            
            //currentSpeed = Mathf.Lerp(currentSpeed, 0f, accelerateDelay * Time.fixedDeltaTime);
        //}

        //Debug.Log("CurrentDesiredSpeed: " + currentDesiredSpeed);



        //if ((Input.GetKey(KeyCode.A)) && !(Input.GetKey(KeyCode.D)))
        //{
        //    DesiredRotation = Vector3.SmoothDamp(DesiredRotation, new Vector3 (0, -turningSpeed, 0), ref velocity, turnDelay * Time.fixedDeltaTime);
        //}

        //if ((Input.GetKey(KeyCode.D)) && !(Input.GetKey(KeyCode.A)))
        //{
        //    DesiredRotation = Vector3.SmoothDamp(DesiredRotation, new Vector3(0, turningSpeed, 0), ref velocity, turnDelay * Time.fixedDeltaTime);
        //}

        //if ((!(Input.GetKey(KeyCode.A)) && !(Input.GetKey(KeyCode.D))) || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
        //{
        //    DesiredRotation = Vector3.SmoothDamp(DesiredRotation, new Vector3(0, 0, 0), ref velocity, turnDelay * Time.fixedDeltaTime);
        //}





        if (CollidingElements > 0) { CurrentCollidedMultiplier = CollidedMultiplier; }
        else { CurrentCollidedMultiplier = .5f; }


        //Add force/torque based on user inputs
        rigidBodyRef.AddTorque(DesiredRotation * CollidedMultiplier * Time.fixedDeltaTime, ForceMode.Acceleration);

        foreach (var Floater in Floaters)
        {

            if(Floater.isSubmerged || CollidingElements > 0)
            {
                rigidBodyRef.AddForce((-transform.right * currentDesiredSpeed * CurrentCollidedMultiplier * Time.fixedDeltaTime) / Floaters.Length, ForceMode.Acceleration);

                //rigidBodyRef.AddForceAtPosition((-transform.right * currentSpeed * Time.fixedDeltaTime) / Floaters.Length, Floater.transform.position, ForceMode.Acceleration);
            }
        }

        TurningWheel.transform.Rotate(-Vector3.up * Time.deltaTime * (currentDesiredSpeed * WheelTurnSpeed), Space.Self);

        if (Input.GetMouseButtonDown(0) != false | Input.GetMouseButtonDown(1))
        {
            HideMouseCursor();
        }
    }

    private void HideMouseCursor()
    {
        Cursor.visible = false;
    }
}

