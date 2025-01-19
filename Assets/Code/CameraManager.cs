using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    private Transform player;
    private Transform target;

    void Start()
    {
        player = GameObject.FindWithTag("Boat").transform;
        target = GetComponent<CinemachineCamera>().Target.TrackingTarget;
        target.parent = player.transform;


        m_ControllerManager.Controllers.Array.data[4].InputValue
            m_ControllerManager.Controllers.Array.data[4].Enabled
            m_ControllerManager.Controllers.Array.data[0].Enabled

        float zoomValue = GetComponent<CinemachineInputAxisController>().Manager.Controllers.Array.data[4].InputValue
            m_LocalPosition.x
            m_ControllerManager.Controllers.Array.data[4].Driver.AccelTime
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
