//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Code/Input/ShipControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @ShipControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ShipControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ShipControls"",
    ""maps"": [
        {
            ""name"": ""Ship"",
            ""id"": ""97aa2fca-193c-425b-ae5c-489784b86790"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""376a5cf7-a534-4f3f-85ba-00984ab284bd"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""95317378-1fc9-49e9-8086-0db4e9046d73"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""b903cfea-93df-48eb-80c4-091c96571760"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": ""Invert,Clamp(min=-0.01,max=0.01)"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""d50bce08-b31d-4a62-9776-05036514ca23"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""87e78459-01dd-4913-91f0-16377d65edeb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""4f2180ae-03f5-488c-98bc-868d143cc27c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d1a644c3-8b1e-4cb9-bb37-1020fdabc788"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""b18972f8-c6c5-456f-af7f-f66eabceb81b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5f982817-82df-406f-9567-52ebf71d4c26"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7fe9e68e-1e62-47d0-8722-b12025614ebd"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""da248ce8-1299-40ea-82aa-027d9696abad"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ad61a001-0b1b-49a5-822b-a909d8501c41"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""LeftJoystick"",
                    ""id"": ""b2bdd766-bbb2-4240-bca4-fdb6d30f603e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3bc5d60a-0574-4f8c-9a67-a6fe6a2aae4e"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""14637684-2b1a-4925-8f2c-3edf201c7c2c"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2ad44dae-5b3b-4c8e-adc5-0fe7e3892537"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2feacf51-2768-4840-8de3-420fe3b2f6d9"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""338799b3-ef83-4c03-918f-fd34b2132a38"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""085c05c9-275d-4527-9c8b-a7815681cbdb"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ebbcad7a-d8a7-4d63-9646-8840b6e7e963"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5646f000-6065-40b6-b282-5f6e427b839c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a488c49-f267-441b-8507-980de8ae0783"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Ship
        m_Ship = asset.FindActionMap("Ship", throwIfNotFound: true);
        m_Ship_Move = m_Ship.FindAction("Move", throwIfNotFound: true);
        m_Ship_Look = m_Ship.FindAction("Look", throwIfNotFound: true);
        m_Ship_Zoom = m_Ship.FindAction("Zoom", throwIfNotFound: true);
        m_Ship_RightClick = m_Ship.FindAction("RightClick", throwIfNotFound: true);
        m_Ship_LeftClick = m_Ship.FindAction("LeftClick", throwIfNotFound: true);
        m_Ship_Pause = m_Ship.FindAction("Pause", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Ship
    private readonly InputActionMap m_Ship;
    private List<IShipActions> m_ShipActionsCallbackInterfaces = new List<IShipActions>();
    private readonly InputAction m_Ship_Move;
    private readonly InputAction m_Ship_Look;
    private readonly InputAction m_Ship_Zoom;
    private readonly InputAction m_Ship_RightClick;
    private readonly InputAction m_Ship_LeftClick;
    private readonly InputAction m_Ship_Pause;
    public struct ShipActions
    {
        private @ShipControls m_Wrapper;
        public ShipActions(@ShipControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Ship_Move;
        public InputAction @Look => m_Wrapper.m_Ship_Look;
        public InputAction @Zoom => m_Wrapper.m_Ship_Zoom;
        public InputAction @RightClick => m_Wrapper.m_Ship_RightClick;
        public InputAction @LeftClick => m_Wrapper.m_Ship_LeftClick;
        public InputAction @Pause => m_Wrapper.m_Ship_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Ship; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ShipActions set) { return set.Get(); }
        public void AddCallbacks(IShipActions instance)
        {
            if (instance == null || m_Wrapper.m_ShipActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ShipActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
            @Zoom.started += instance.OnZoom;
            @Zoom.performed += instance.OnZoom;
            @Zoom.canceled += instance.OnZoom;
            @RightClick.started += instance.OnRightClick;
            @RightClick.performed += instance.OnRightClick;
            @RightClick.canceled += instance.OnRightClick;
            @LeftClick.started += instance.OnLeftClick;
            @LeftClick.performed += instance.OnLeftClick;
            @LeftClick.canceled += instance.OnLeftClick;
            @Pause.started += instance.OnPause;
            @Pause.performed += instance.OnPause;
            @Pause.canceled += instance.OnPause;
        }

        private void UnregisterCallbacks(IShipActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
            @Zoom.started -= instance.OnZoom;
            @Zoom.performed -= instance.OnZoom;
            @Zoom.canceled -= instance.OnZoom;
            @RightClick.started -= instance.OnRightClick;
            @RightClick.performed -= instance.OnRightClick;
            @RightClick.canceled -= instance.OnRightClick;
            @LeftClick.started -= instance.OnLeftClick;
            @LeftClick.performed -= instance.OnLeftClick;
            @LeftClick.canceled -= instance.OnLeftClick;
            @Pause.started -= instance.OnPause;
            @Pause.performed -= instance.OnPause;
            @Pause.canceled -= instance.OnPause;
        }

        public void RemoveCallbacks(IShipActions instance)
        {
            if (m_Wrapper.m_ShipActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IShipActions instance)
        {
            foreach (var item in m_Wrapper.m_ShipActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ShipActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ShipActions @Ship => new ShipActions(this);
    public interface IShipActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
        void OnLeftClick(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
