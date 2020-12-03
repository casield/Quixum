// GENERATED AUTOMATICALLY FROM 'Assets/src/InputControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputControl : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControl"",
    ""maps"": [
        {
            ""name"": ""Normal"",
            ""id"": ""301452e3-a722-4c2d-a655-6c624013405a"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""cc52a894-1f5c-4e07-80b9-554189f2be11"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Drag"",
                    ""type"": ""Value"",
                    ""id"": ""54c977c2-c6d7-4aea-8cc5-754eff88c589"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Stop Ball"",
                    ""type"": ""Button"",
                    ""id"": ""d546d233-ca01-4941-90e3-5432b02e4375"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SendMessage"",
                    ""type"": ""Button"",
                    ""id"": ""381d6a11-24c6-4d3b-bb90-ddf833caa51f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Position"",
                    ""type"": ""Value"",
                    ""id"": ""95ddf023-1e6f-4fcb-9069-ebf104e79e2e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DoubleTap"",
                    ""type"": ""Button"",
                    ""id"": ""6401b954-b5ef-4827-a19f-02bc4219e5be"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""8f49f871-0739-44bc-943d-4166ddd91dfb"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""0f6340d7-d491-4b15-bf54-21d97c91d2dc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""a3fecbef-23cb-4985-b7fc-45f1533eb714"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""3fe92fe0-84bf-434c-8502-1098720de2ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Use_Power1"",
                    ""type"": ""Button"",
                    ""id"": ""0e82ea01-b4c8-4ff7-b786-ec5d8471a0a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""87ebde3a-d74b-42e2-b00d-6a126e6f618a"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""647ca47e-888b-4faa-a2cc-e92ce89a43ca"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f31fdea3-4489-4759-be37-75b4611b0f93"",
                    ""path"": ""<Touchscreen>/touch0/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b7099ad-b470-4f85-a6bf-d9cd39ea663f"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""419bfa49-aae1-4fc7-b822-bd0a51e3aae4"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Stop Ball"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c023329b-9a91-4067-9246-2e3b3e818605"",
                    ""path"": ""<Touchscreen>/primaryTouch/tap"",
                    ""interactions"": ""MultiTap(tapTime=0.2,tapDelay=0.4,pressPoint=0.5)"",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Stop Ball"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84d4b0d0-3d64-47b0-ae51-0b744e977086"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""SendMessage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08c21269-57a4-41d3-8d03-3b4291504b50"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""SendMessage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5dea8f56-717c-47e1-99d0-ba2ce98840ec"",
                    ""path"": ""<Pointer>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""344e8372-9c44-4c66-8437-228e3a8b3a26"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de3e40db-c9b0-49b0-8be4-83925fec48c8"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""MultiTap"",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""DoubleTap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fdb34863-5f1c-4375-9f27-98896a34bfa2"",
                    ""path"": ""<Touchscreen>/primaryTouch/tap"",
                    ""interactions"": ""MultiTap"",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""DoubleTap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb0e7829-009d-45da-bb44-89232b1da8a6"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7a4f865-bce9-4b42-adc4-013739cd60db"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90346d0d-1b4c-4b58-8b71-6ccb457e73b9"",
                    ""path"": ""<Touchscreen>/touch1/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8e9dc66-f8e8-4c83-958f-a862e009fa64"",
                    ""path"": ""<Touchscreen>/touch1/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3edd7155-c5a4-4fa0-a9ba-a080d3ff050b"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b5ef546-a666-442f-9e76-2d15e3b55a04"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC;Touch;Joystick"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fbae7d84-e348-491f-b888-8d0fb008707e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8b36265-6ec3-4218-9eaa-5100e77c522b"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Use_Power1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PC"",
            ""bindingGroup"": ""PC"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Joystick"",
            ""bindingGroup"": ""Joystick"",
            ""devices"": [
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Normal
        m_Normal = asset.FindActionMap("Normal", throwIfNotFound: true);
        m_Normal_Click = m_Normal.FindAction("Click", throwIfNotFound: true);
        m_Normal_Drag = m_Normal.FindAction("Drag", throwIfNotFound: true);
        m_Normal_StopBall = m_Normal.FindAction("Stop Ball", throwIfNotFound: true);
        m_Normal_SendMessage = m_Normal.FindAction("SendMessage", throwIfNotFound: true);
        m_Normal_Position = m_Normal.FindAction("Position", throwIfNotFound: true);
        m_Normal_DoubleTap = m_Normal.FindAction("DoubleTap", throwIfNotFound: true);
        m_Normal_Zoom = m_Normal.FindAction("Zoom", throwIfNotFound: true);
        m_Normal_Shoot = m_Normal.FindAction("Shoot", throwIfNotFound: true);
        m_Normal_Move = m_Normal.FindAction("Move", throwIfNotFound: true);
        m_Normal_Jump = m_Normal.FindAction("Jump", throwIfNotFound: true);
        m_Normal_Use_Power1 = m_Normal.FindAction("Use_Power1", throwIfNotFound: true);
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

    // Normal
    private readonly InputActionMap m_Normal;
    private INormalActions m_NormalActionsCallbackInterface;
    private readonly InputAction m_Normal_Click;
    private readonly InputAction m_Normal_Drag;
    private readonly InputAction m_Normal_StopBall;
    private readonly InputAction m_Normal_SendMessage;
    private readonly InputAction m_Normal_Position;
    private readonly InputAction m_Normal_DoubleTap;
    private readonly InputAction m_Normal_Zoom;
    private readonly InputAction m_Normal_Shoot;
    private readonly InputAction m_Normal_Move;
    private readonly InputAction m_Normal_Jump;
    private readonly InputAction m_Normal_Use_Power1;
    public struct NormalActions
    {
        private @InputControl m_Wrapper;
        public NormalActions(@InputControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Click => m_Wrapper.m_Normal_Click;
        public InputAction @Drag => m_Wrapper.m_Normal_Drag;
        public InputAction @StopBall => m_Wrapper.m_Normal_StopBall;
        public InputAction @SendMessage => m_Wrapper.m_Normal_SendMessage;
        public InputAction @Position => m_Wrapper.m_Normal_Position;
        public InputAction @DoubleTap => m_Wrapper.m_Normal_DoubleTap;
        public InputAction @Zoom => m_Wrapper.m_Normal_Zoom;
        public InputAction @Shoot => m_Wrapper.m_Normal_Shoot;
        public InputAction @Move => m_Wrapper.m_Normal_Move;
        public InputAction @Jump => m_Wrapper.m_Normal_Jump;
        public InputAction @Use_Power1 => m_Wrapper.m_Normal_Use_Power1;
        public InputActionMap Get() { return m_Wrapper.m_Normal; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(NormalActions set) { return set.Get(); }
        public void SetCallbacks(INormalActions instance)
        {
            if (m_Wrapper.m_NormalActionsCallbackInterface != null)
            {
                @Click.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnClick;
                @Drag.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnDrag;
                @Drag.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnDrag;
                @Drag.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnDrag;
                @StopBall.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnStopBall;
                @StopBall.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnStopBall;
                @StopBall.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnStopBall;
                @SendMessage.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnSendMessage;
                @SendMessage.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnSendMessage;
                @SendMessage.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnSendMessage;
                @Position.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnPosition;
                @Position.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnPosition;
                @Position.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnPosition;
                @DoubleTap.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnDoubleTap;
                @DoubleTap.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnDoubleTap;
                @DoubleTap.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnDoubleTap;
                @Zoom.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnZoom;
                @Shoot.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnShoot;
                @Move.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnJump;
                @Use_Power1.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnUse_Power1;
                @Use_Power1.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnUse_Power1;
                @Use_Power1.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnUse_Power1;
            }
            m_Wrapper.m_NormalActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @Drag.started += instance.OnDrag;
                @Drag.performed += instance.OnDrag;
                @Drag.canceled += instance.OnDrag;
                @StopBall.started += instance.OnStopBall;
                @StopBall.performed += instance.OnStopBall;
                @StopBall.canceled += instance.OnStopBall;
                @SendMessage.started += instance.OnSendMessage;
                @SendMessage.performed += instance.OnSendMessage;
                @SendMessage.canceled += instance.OnSendMessage;
                @Position.started += instance.OnPosition;
                @Position.performed += instance.OnPosition;
                @Position.canceled += instance.OnPosition;
                @DoubleTap.started += instance.OnDoubleTap;
                @DoubleTap.performed += instance.OnDoubleTap;
                @DoubleTap.canceled += instance.OnDoubleTap;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Use_Power1.started += instance.OnUse_Power1;
                @Use_Power1.performed += instance.OnUse_Power1;
                @Use_Power1.canceled += instance.OnUse_Power1;
            }
        }
    }
    public NormalActions @Normal => new NormalActions(this);
    private int m_PCSchemeIndex = -1;
    public InputControlScheme PCScheme
    {
        get
        {
            if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex("PC");
            return asset.controlSchemes[m_PCSchemeIndex];
        }
    }
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    private int m_JoystickSchemeIndex = -1;
    public InputControlScheme JoystickScheme
    {
        get
        {
            if (m_JoystickSchemeIndex == -1) m_JoystickSchemeIndex = asset.FindControlSchemeIndex("Joystick");
            return asset.controlSchemes[m_JoystickSchemeIndex];
        }
    }
    public interface INormalActions
    {
        void OnClick(InputAction.CallbackContext context);
        void OnDrag(InputAction.CallbackContext context);
        void OnStopBall(InputAction.CallbackContext context);
        void OnSendMessage(InputAction.CallbackContext context);
        void OnPosition(InputAction.CallbackContext context);
        void OnDoubleTap(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnUse_Power1(InputAction.CallbackContext context);
    }
}
