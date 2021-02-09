using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class RotationController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
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
    }

    private void drag(Vector2 pos)
    {
        Vector2 pointerPosition = pos;
        fingerSprite.transform.position = pointerPosition;
        Vector2 result = startPosition - pointerPosition;
        Vector2 normal = result.normalized;
        sendData(result / (Screen.width / 4));
        //Debug.Log(result);
    }
    private void dragStart(Vector2 pos)
    {
        startPosition = pos;
        isDragging = true;
        startSprite.transform.position = startPosition;
        changeSpritesVisibility(true);
    }
    private void dragEnd()
    {
        isDragging = false;
        sendData(Vector2.zero);
        changeSpritesVisibility(false);
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
    
    private void onFingerUp(Finger obj)
    {
        if (activeFinger == obj)
        {
            dragEnd();
            activeFinger = null;
        }

    }

    private void onFingerMove(Finger obj)
    {
        if (isDragging && activeFinger == obj)
        {
            drag(obj.screenPosition);
        }
    }

    private void onFingerDown(Finger obj)
    {
        if (activeFinger == null)
        {
            activeFinger = obj;
            drag(obj.screenPosition);
        }

    }    public void OnEndDrag(PointerEventData eventData)
    {
        dragEnd();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragStart(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        drag(eventData.position);
    }
}
