using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class RotationController : MonoBehaviour
{
    Client client;
    Vector2 startPosition = new Vector2();
    private bool isDragging = false;
    private InputControl control;

    public Image startSprite, fingerSprite;
    Finger activeFinger;

    private void Awake()
    {
        Client.Instance.addReadyListener(init);
        startSprite.gameObject.SetActive(false);
        fingerSprite.gameObject.SetActive(false);
        EnhancedTouchSupport.Enable();

        Touch.onFingerDown += onFingerDown;
        Touch.onFingerMove += onFingerMove;
        Touch.onFingerUp += onFingerUp;
    }

    private void onFingerUp(Finger obj)
    {
        if (activeFinger == obj)
        {
            isDragging = false;
            sendData(Vector2.zero);
            changeSpritesVisibility(false);
            activeFinger = null;
        }

    }

    private void onFingerMove(Finger obj)
    {
        if (isDragging && activeFinger == obj)
        {
            Vector2 pointerPosition = obj.screenPosition;
            fingerSprite.transform.position = pointerPosition;
            Vector2 result = startPosition - pointerPosition;
            Vector2 normal = result.normalized;
            sendData(result / (Screen.width / 4));
        }
    }

    private void onFingerDown(Finger obj)
    {
        if (activeFinger == null)
        {
            activeFinger = obj;
            startPosition = obj.screenPosition;
            isDragging = true;
            startSprite.transform.position = startPosition;
            changeSpritesVisibility(true);
        }

    }

    private void init()
    {
        client = Client.Instance;
    }
    void changeSpritesVisibility(bool active)
    {
        startSprite.gameObject.SetActive(active);
        fingerSprite.gameObject.SetActive(active);
    }
    /* public void onClick(CallbackContext context)
     {
         if (context.phase == UnityEngine.InputSystem.InputActionPhase.Started)
         {
             startPosition = Character.Instance.inputControl.Normal.Position.ReadValue<Vector2>();
             isDragging = true;
             startSprite.transform.position = startPosition;
             changeSpritesVisibility(true);
         }

         if (context.phase == UnityEngine.InputSystem.InputActionPhase.Canceled)
         {

             isDragging = false;
             sendData(Vector2.zero);
             changeSpritesVisibility(true);
         }
     }

     public void onMouseMove(CallbackContext context)
     {
         // Debug.Log(context.ReadValue<Vector2>());
         if (isDragging)
         {
             Vector2 pointerPosition = context.ReadValue<Vector2>();
             fingerSprite.transform.position = pointerPosition;
             Vector2 result = startPosition - pointerPosition;
             Vector2 normal = result.normalized;
             sendData(result / (Screen.width / 4));
         }

     }*/

    public async void sendData(Vector2 eventData)
    {

        if (client != null)
        {
            Vector2 delta = eventData;
            V3 v3 = new V3();
            v3.x = (delta.x);
            v3.y = 0;
            await client.room.Send("rotatePlayer", v3);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
