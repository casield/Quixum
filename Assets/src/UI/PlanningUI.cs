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
    }

    private void onTurnChange(List<DataChange> changes)
    {
        foreach (var change in changes)
        {

            if (change.Field == "phase")
            {
                Debug.Log("change field " + change.Field);
                this.gameObject.SetActive(true);
            }
        }
    }

    public void setObject(TurnBox turnBox)
    {
        Debug.Log("object setted");
        this.turnBox = turnBox;
        this.turnBox.sendMessageToRoom("probando");
    }

    internal void open()
    {
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
        Debug.Log("Setting " + g);
        draggingObject = g;
        savedParentTransform = g.transform.parent.transform;
        draggingObject.transform.parent = this.transform;
        isDraggin = true;
    }
    public void dropObject()
    {
        Debug.Log("Dropping " + draggingObject);
        planningBoard.onDropObject(draggingObject);
        isDraggin = false;
    }

}
