using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class RotationControllerUI : VisualElement
{
    // Start is called before the first frame update
    Client client;
    public MoveMessage rotateMessage = new MoveMessage();
    private Vector2 startPosition = new Vector2();
    private bool isDragging;

    public new class UxmlFactory : UxmlFactory<RotationControllerUI, UxmlTraits> { };
    public new class UxmlTraits : VisualElement.UxmlTraits { };


    public RotationControllerUI()
    {
        this.RegisterCallback<GeometryChangedEvent>(GeometryChange);
        this.style.position = Position.Absolute;
        this.style.height = Length.Percent(100);
        this.style.width = Length.Percent(100);
    }

    private void GeometryChange(GeometryChangedEvent evt)
    {
        Client.Instance.OnReadyListener += Init;
        this.UnregisterCallback<GeometryChangedEvent>(GeometryChange);
    }

    private void Init()
    {
        Client.Instance.OnReadyListener -= Init;
        client = Client.Instance;

        this.RegisterCallback<PointerDownEvent>((e) =>
        {
            QuixConsole.Log("PointerOver", e.position);
            dragStart(e.position);
        });
        this.RegisterCallback<PointerUpEvent>((e) =>
        {
            QuixConsole.Log("DragEnd", e.position);
            dragEnd();
        });
        this.RegisterCallback<PointerMoveEvent>((e) =>
        {
            if (isDragging)
            {
                QuixConsole.Log("PointerOver", e.position);
                drag(e.position);
                actualSendData();
            }

        });
    }

    private void drag(Vector2 pos)
    {
        Vector2 pointerPosition = pos;
        //fingerSprite.transform.position = pointerPosition;
        Vector2 result = startPosition - pointerPosition;

        sendData(result.normalized);
        //Debug.Log(result);
    }
    private void dragStart(Vector2 pos)
    {
        startPosition = pos;//new Vector2(Screen.width / 2, Screen.height / 2);
        isDragging = true;
        //startSprite.transform.position = startPosition;
        //changeSpritesVisibility(true);
    }
    private void dragEnd()
    {
        isDragging = false;
        sendData(Vector2.zero);
        actualSendData();
    }

    private async void actualSendData()
    {
        if (client != null)
        {
            await client.room.Send("rotate", rotateMessage);
        }
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

}
