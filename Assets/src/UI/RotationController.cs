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
    public static RotationController Instance;
    Finger activeFinger;

    public MoveMessage rotateMessage = new MoveMessage();
    private void Awake()
    {
        Instance = this;
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
        actualSendData();
        changeSpritesVisibility(false);
        CameraController.Instance.SetFollowObjectToPlayer();
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

    public void sendData(Vector2 eventData)
    {

        if (client != null && Character.Instance.player != null)
        {
            Vector2 delta = eventData;
            rotateMessage.uID = Character.Instance.player.state.uID;
            rotateMessage.x = delta.x;
            rotateMessage.y = delta.y;


        }
    }

    private async void actualSendData()
    {
        if (client != null)
        {
            await client.room.Send("rotate", rotateMessage);
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

    }
    public void OnEndDrag(PointerEventData eventData)
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

    private void FixedUpdate()
    {
        if (rotateMessage.x != 0 || rotateMessage.y != 0)
        {
            CameraController.Instance.MoveCameraY(rotateMessage.y);
            actualSendData();
        }
    }
}
