using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLock : MonoBehaviour
{
    private CinemachineCamera _camera;

    private PlayerInputManager Input;
    void Start()
    {
        Input = GameObject.FindWithTag("Boat").GetComponent<PlayerInputManager>();
        _camera = GetComponent<CinemachineCamera>();
        _camera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.rotateView)
        {
            _camera.enabled = true;
            return;
        }
        _camera.enabled = false;    

    }
}
