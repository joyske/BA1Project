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

    private void Start()
    {
        BaseOffset = transform.position;
    }

    void Update()
    {
        transform.eulerAngles = new Vector3( transform.eulerAngles.x, PlayerRef.transform.eulerAngles.y - 90, transform.eulerAngles.z);

        CalculatePositionWithRotation();
        transform.position = PlayerRef.transform.position + Offset;
    }

    private void CalculatePositionWithRotation()
    {
        

        BlendValueX = ((Mathf.Abs((-(PlayerRef.transform.eulerAngles.y - 180))/180)-0.5f)*2);
        BlendValueZ = Mathf.Sin(-(PlayerRef.transform.eulerAngles.y * Mathf.Deg2Rad));


        Debug.Log("X" + BlendValueX + "Z" + BlendValueZ);
        Offset.Set((BaseOffset.x * BlendValueX), (BaseOffset.y), (BaseOffset.x * BlendValueZ));
    }
}