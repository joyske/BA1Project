using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject PlayerRef;
    Vector3 BaseOffset;
    Vector3 Offset;
    float BlendValueX;
    float BlendValueZ;
    Vector3 DesiredPosition;
    public float CameraPositionBlendTime;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        //Initialize default camera offset and default DesiredPosition
        BaseOffset = transform.position;
        DesiredPosition = transform.position;
    }

    void Update()
    {
        //Move Camera rotation and position position with player character
        transform.eulerAngles = new Vector3( transform.eulerAngles.x, PlayerRef.transform.eulerAngles.y - 90, transform.eulerAngles.z);
        CalculatePositionWithRotation();
        DesiredPosition = PlayerRef.transform.position + Offset;
        transform.position = Vector3.SmoothDamp(transform.position, DesiredPosition, ref velocity, CameraPositionBlendTime);
    }

    private void CalculatePositionWithRotation()
    {
        //Calculate camera position around player char based on rotation
        BlendValueX = Mathf.Cos(-(PlayerRef.transform.eulerAngles.y * Mathf.Deg2Rad));
        BlendValueZ = Mathf.Sin(-(PlayerRef.transform.eulerAngles.y * Mathf.Deg2Rad));
        Offset.Set((BaseOffset.x * BlendValueX), (BaseOffset.y), (BaseOffset.x * BlendValueZ));
    }
}