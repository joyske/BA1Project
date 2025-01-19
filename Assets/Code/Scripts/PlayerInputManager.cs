using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{   
    private ShipControls shipControls;
    public Vector2 movementInput;
    public bool rotateView = false;
    private void OnEnable()
    {
        if(shipControls == null)
        {
            shipControls = new ShipControls();
            shipControls.Enable();
            shipControls.Ship.Move.performed += context => movementInput = context.ReadValue<Vector2>();
            shipControls.Ship.Lock.started += context => rotateView = true;
            shipControls.Ship.Lock.canceled += context => rotateView = false;
        }
    }

    private void OnDisable()
    {
        shipControls.Disable();
    }
}
