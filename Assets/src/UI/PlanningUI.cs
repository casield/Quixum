using System;
using System.Collections;
using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlanningUI : MonoBehaviour
{
    // Start is called before the first frame update

    Client client;
    public static PlanningUI Instance;
    private TurnBox turnBox;
    private ArenaShopItem draggingObject;
    private Transform savedParentTransform;

    public PlanningBoard planningBoard;
    private bool isDraggin = false;

    private void Awake()
    {
        Instance = this;
        planningBoard = GetComponentInChildren<PlanningBoard>();
        Debug.Log("Planning Board - " + planningBoard);
    }

    private void init()
    {
        client = Client.Instance;
        client.room.State.turnState.OnChange += onTurnChange;
        client.userState.board.OnRemove+=onItemRemove;
    }

    private void onItemRemove(ArenaItemState value, string key)
    {
        Debug.Log("Destroying "+key);
        planningBoard.droppedItems[key].destroy();
    }

    private void onTurnChange(List<DataChange> changes)
    {
        foreach (var change in changes)
        {

            if (change.Field == "phase")
            {
                 Debug.Log("XD "+change.Value);
                if (change.Value.ToString() == "1")
                {
                    Open();
                }
               
                if (change.Value.ToString() == "2")
                {
                    Close();
                }

            }
        }
    }

    public void setObject(TurnBox turnBox)
    {
        this.turnBox = turnBox;
        this.turnBox.sendMessageToRoom("probando");
    }

    public async void sendBoard()
    {
        if (Client.Instance.userState != null)
        {
            await Client.Instance.room.Send("readyPlanning","true");
        }
    }

    public void Open()
    {
        this.gameObject.SetActive(true);
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    void Start()
    {
        Client.Instance.addReadyListener(init);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDraggin)
        {
            draggingObject.transform.parent = this.transform;
            draggingObject.transform.position = Character.Instance.inputControl.Normal.Position.ReadValue<Vector2>();
        }
    }
    public void setDraggingObject(ArenaShopItem g)
    {
        draggingObject = g;
        savedParentTransform = g.transform.parent.transform;
        draggingObject.transform.parent = this.transform;
        isDraggin = true;
    }
    public void dropObject()
    {
        isDraggin = false;
    }

}
