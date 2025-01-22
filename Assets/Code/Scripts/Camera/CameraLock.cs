using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLock : MonoBehaviour
{
    private CinemachineCamera _camera;

    private PlayerInputManager Input;

    private Vector3 lastPosition;
    private InputAxis lastPanAxis;
    private InputAxis lastTiltAxis;
    private bool didJustSwitch;

    void Start()
    {
        Input = GameObject.FindWithTag("Boat").GetComponent<PlayerInputManager>();
        _camera = GetComponent<CinemachineCamera>();
        _camera.enabled = false;

        lastPanAxis = _camera.GetComponent<CinemachinePanTilt>().PanAxis;
        lastTiltAxis = _camera.GetComponent<CinemachinePanTilt>().TiltAxis;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.rotateView)
        {
            if (didJustSwitch == false ) 
            {
                _camera.GetComponent<CinemachinePanTilt>().PanAxis = lastPanAxis;
                _camera.GetComponent<CinemachinePanTilt>().TiltAxis = lastTiltAxis;
            }

            _camera.enabled = true;
            didJustSwitch = true;
            return;
        }

        if (didJustSwitch) 
        {
            lastPanAxis = _camera.GetComponent<CinemachinePanTilt>().PanAxis;
            lastTiltAxis = _camera.GetComponent<CinemachinePanTilt>().TiltAxis;
        }
        
        _camera.enabled = false;
        didJustSwitch = false;

    }
}
