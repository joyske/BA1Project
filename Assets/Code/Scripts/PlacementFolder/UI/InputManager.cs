using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{

    [SerializeField]
    Camera sceneCamera;

    [SerializeField]
    LayerMask placementLayermask;

    private Vector3 lastPos;

    public event Action OnClicked, OnExit;

    /// <summary>
    /// Checks for Input
    /// </summary>
    public void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
            OnClicked?.Invoke();
        if (Input.GetKeyDown(KeyCode.Escape))
            OnExit?.Invoke();
    }

    public bool IsPointOverUI()
        => EventSystem.current.IsPointerOverGameObject();


    /// <summary>
    ///  Gets mouseposition and returns it 
    /// </summary>
    /// <returns>Vector3</returns>
    public Vector3 GetSelectedMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, placementLayermask))
        {
            lastPos = hit.point;
        }
        return lastPos;
    }

}
