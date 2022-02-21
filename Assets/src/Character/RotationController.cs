﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
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
    private void Start()
    {
        Instance = this;
        Client.Instance.addReadyListener(init);
        startSprite.gameObject.SetActive(false);
        fingerSprite.gameObject.SetActive(false);
        QuixConsole.Log("Hola!!");
    }

    private void drag(Vector2 pos)
    {
        Vector2 pointerPosition = pos;
        fingerSprite.transform.position = pointerPosition;
        Vector2 result = startPosition - pointerPosition;

        sendData(result.normalized);
        //Debug.Log(result);
    }
    private void dragStart(Vector2 pos)
    {
        startPosition = pos;//new Vector2(Screen.width / 2, Screen.height / 2);
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
    }

    private void init()
    {
        client = Client.Instance;
        dragStart(Vector2.zero);
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
        if (rotateMessage.x != 0 || rotateMessage.y != 0 && Character.Instance.inputControl.Normal.Click.ReadValue<float>() >0)
        {

            actualSendData();
        }
    }
}
