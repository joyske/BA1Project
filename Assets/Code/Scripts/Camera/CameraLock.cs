using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLock : MonoBehaviour
{
    private CinemachineCamera _camera;
    private CinemachinePanTilt cinemachinePanTilt;

    private PlayerInputManager Input;

    private Vector3 lastPosition;
    private InputAxis lastPanAxis;
    private InputAxis lastTiltAxis;
    private bool didJustSwitch;

    

    void Start()
    {
        Input = GameObject.FindWithTag("Boat").GetComponent<PlayerInputManager>();
        _camera = GetComponent<CinemachineCamera>();
        cinemachinePanTilt = GetComponent<CinemachinePanTilt>();
        cinemachinePanTilt.enabled = false;

        lastPanAxis = cinemachinePanTilt.PanAxis;
        lastTiltAxis = cinemachinePanTilt.TiltAxis;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.rotateView)
        {
            if (didJustSwitch == false ) 
            {
                cinemachinePanTilt.PanAxis = lastPanAxis;
                cinemachinePanTilt.TiltAxis = lastTiltAxis;
            }

            cinemachinePanTilt.enabled = true;
            didJustSwitch = true;
            return;
        }

        if (didJustSwitch) 
        {
            lastPanAxis = cinemachinePanTilt.PanAxis;
            lastTiltAxis = cinemachinePanTilt.TiltAxis;
        }

        cinemachinePanTilt.enabled = false;
        didJustSwitch = false;

    }
}
