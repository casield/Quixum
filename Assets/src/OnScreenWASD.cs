using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
namespace UnityEngine.InputSystem.OnScreen
{
    public class OnScreenWASD : OnScreenControl
    {
        [AddComponentMenu("Input/On-Screen Stick")]

        KeyControl kW;
        KeyControl kA;
        KeyControl kS;
        KeyControl kD;

        Vector2 velocity = new Vector2();

        float initVelocity = 1.5f;

        // Start is called before the first frame update
        void Awake()
        {

        }

        // Update is called once per frame
        void Update()
        {
            checkWASD();
        }

        void checkWASD()
        {
            kW = Keyboard.current[Key.W];
            kA = Keyboard.current[Key.A];
            kS = Keyboard.current[Key.S];
            kD = Keyboard.current[Key.D];

            if (kW.wasPressedThisFrame)
            {
                velocity.y = initVelocity;
                SendValueToControl(velocity);
            }
            if (kS.wasPressedThisFrame)
            {
                velocity.y = -initVelocity;
                SendValueToControl(velocity);
            }


            if (kD.wasPressedThisFrame)
            {
                velocity.x = initVelocity;
                SendValueToControl(velocity);
            }
            if (kA.wasPressedThisFrame)
            {
                velocity.x = -initVelocity;
                SendValueToControl(velocity);
            }

            if(kW.wasReleasedThisFrame || kS.wasReleasedThisFrame){
                velocity.y = 0;
                SendValueToControl(velocity);
            }
            if(kA.wasReleasedThisFrame || kD.wasReleasedThisFrame){
                velocity.x = 0;
                SendValueToControl(velocity);
            }
        }

        [InputControl(layout = "Vector2")]
        [SerializeField]
        private string m_ControlPath;

        protected override string controlPathInternal
        {
            get => m_ControlPath;
            set => m_ControlPath = value;
        }
    }
}