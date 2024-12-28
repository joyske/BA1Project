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
    public float CameraBlendTime;
    private Vector3 velocity = Vector3.zero;
    private float DesiredCameraMoveRotationOffsetX;
    private float CameraMoveRotationOffsetX;
    private float DesiredCameraMoveRotationOffsetY;
    private float CameraMoveRotationOffsetY;
    public float MouseSensitivity = 1f;
    public bool CameraTurnsWithShip;
    private Vector3 RotationEulerAngles;

    private void Start()
    {
        //Initialize default camera offset and default DesiredPosition
        BaseOffset = transform.position;
        DesiredPosition = transform.position;

        //Initialize camera position
        RotationEulerAngles = new Vector3(transform.eulerAngles.x, PlayerRef.transform.eulerAngles.y - 90 + CameraMoveRotationOffsetX, transform.eulerAngles.z);

        //Disable cursor
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetAxis("Mouse X") != 0)
        {
            DesiredCameraMoveRotationOffsetX = DesiredCameraMoveRotationOffsetX + Time.deltaTime * MouseSensitivity * (Input.GetAxis("Mouse X"));
        }
    }

    private void CalculatePositionWithRotation()
    {
        //Calculate camera position around player char based on rotation
        BlendValueX = Mathf.Cos(-((RotationEulerAngles.y + 90) * Mathf.Deg2Rad));
        BlendValueZ = Mathf.Sin(-((RotationEulerAngles.y + 90) * Mathf.Deg2Rad));
        Offset.Set((BaseOffset.x * BlendValueX), (BaseOffset.y), (BaseOffset.x * BlendValueZ));
    }


    private void FixedUpdate()
    {
        CameraMoveRotationOffsetX = DesiredCameraMoveRotationOffsetX; //Mathf.MoveTowardsAngle(CameraMoveRotationOffsetX, DesiredCameraMoveRotationOffsetX, Time.deltaTime);


        //If desired ove Camera rotation and position position with player character + turn with mouse
        if (CameraTurnsWithShip)
        {
            RotationEulerAngles = new Vector3(transform.eulerAngles.x, PlayerRef.transform.eulerAngles.y - 90 + CameraMoveRotationOffsetX, transform.eulerAngles.z);
        }
        else
        {
            RotationEulerAngles = new Vector3(transform.eulerAngles.x, CameraMoveRotationOffsetX - 90, transform.eulerAngles.z);
        }
        

        transform.eulerAngles = RotationEulerAngles;
        CalculatePositionWithRotation();
        DesiredPosition = PlayerRef.transform.position + Offset;
        transform.position = Vector3.SmoothDamp(transform.position, DesiredPosition, ref velocity, 0);
        // transform.position = Mathf.MoveTowards(transform.position, DesiredPosition, CameraBlendTime * Time.deltaTime);
        
    }
}