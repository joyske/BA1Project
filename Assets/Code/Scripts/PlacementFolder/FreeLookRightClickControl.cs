using UnityEngine;
using Cinemachine;

public class FreeLookRightClickControl : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;

    void Update()
    {
        if (Input.GetMouseButton(1)) // Right-click
        {
            freeLookCamera.m_XAxis.m_InputAxisName = "Mouse X";
            freeLookCamera.m_YAxis.m_InputAxisName = "Mouse Y";
        }
        else
        {
            freeLookCamera.m_XAxis.m_InputAxisName = "";
            freeLookCamera.m_YAxis.m_InputAxisName = "";
        }
    }
}
