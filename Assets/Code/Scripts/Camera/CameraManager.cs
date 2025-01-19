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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
