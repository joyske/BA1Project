using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject PlayerRef;
    Vector3 BaseOffset;
    Vector3 Offset;
    float BlendValueX;
    float BlendValueZ;
    float BlendValueY;
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
    public float MinAngleY;
    public float MaxAngleY;
    public float CameraRotationRangeY;
    public float CameraYRotationPositionInfluence;

    private void Start()
    {
        //Initialize default camera offset and default DesiredPosition
        BaseOffset = transform.position;
        DesiredPosition = transform.position;

        //Initialize camera position
        RotationEulerAngles = new Vector3(transform.eulerAngles.x, PlayerRef.transform.eulerAngles.y - 90 + CameraMoveRotationOffsetX, transform.eulerAngles.z);

        //Disable cursor
        HideMouseCursor();
    }

    void Update()
    {
        if (Input.GetAxis("Mouse X") != 0)
        {
            DesiredCameraMoveRotationOffsetX = DesiredCameraMoveRotationOffsetX + Time.deltaTime * MouseSensitivity * (Input.GetAxis("Mouse X"));
        }
        if (Input.GetAxis("Mouse Y") != 0)
        {
            DesiredCameraMoveRotationOffsetY = Mathf.Clamp(DesiredCameraMoveRotationOffsetY + Time.deltaTime * MouseSensitivity * 0.6f * -(Input.GetAxis("Mouse Y")), MinAngleY, MaxAngleY);
        }


        //Disable cursor
        if (Input.GetMouseButtonDown(0) != false)
        {
            HideMouseCursor();
        }
    }

    private void CalculatePositionWithRotation()
    {
        //Calculate camera position around player char based on rotation
        BlendValueX = Mathf.Cos(-((RotationEulerAngles.y + 90) * Mathf.Deg2Rad));
        BlendValueZ = Mathf.Sin(-((RotationEulerAngles.y + 90) * Mathf.Deg2Rad));

        BlendValueY = (Mathf.Clamp(1 - ((Mathf.Clamp((transform.eulerAngles.x + MinAngleY), MinAngleY, MaxAngleY)) / MaxAngleY), 0, 1));

        Offset.Set( (BaseOffset.x * (BlendValueX * ((1 - CameraYRotationPositionInfluence) + (BlendValueY * CameraYRotationPositionInfluence)))), (BaseOffset.y + (CameraRotationRangeY * ((1 - BlendValueY) - 0.5f))), (BaseOffset.x * (BlendValueZ * (1 - CameraYRotationPositionInfluence + (BlendValueY * CameraYRotationPositionInfluence)))));
    }


    private void FixedUpdate()
    {
        CameraMoveRotationOffsetX = DesiredCameraMoveRotationOffsetX;
        CameraMoveRotationOffsetY = DesiredCameraMoveRotationOffsetY;


        UnityEngine.Debug.Log(Mathf.Clamp(PlayerRef.transform.eulerAngles.x, 0, 360) + CameraMoveRotationOffsetY);
        RotationEulerAngles = new Vector3(Mathf.Clamp(PlayerRef.transform.eulerAngles.x, 0, 360) + CameraMoveRotationOffsetY, PlayerRef.transform.eulerAngles.y - 90 + CameraMoveRotationOffsetX, 0);

        transform.eulerAngles = RotationEulerAngles;
        
        if (transform.eulerAngles.x < 0)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        CalculatePositionWithRotation();
        DesiredPosition = PlayerRef.transform.position + Offset;
        transform.position = Vector3.SmoothDamp(transform.position, DesiredPosition, ref velocity, 0);
        // transform.position = Mathf.MoveTowards(transform.position, DesiredPosition, CameraBlendTime * Time.deltaTime);
        
    }

    private void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}