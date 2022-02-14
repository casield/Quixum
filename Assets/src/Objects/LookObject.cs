using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LookObject : ConnectedObject
{
    private SObject sObject;
    private Player2 player;
    public Vector2 OnLookingPosition = new Vector2(150, 47);

    public bool isOnPlayer = true;

    private void Start()
    {
        sObject = GetComponent<SObject>();
        sObject.refreshTime = .9f;

    }

    public override void onMessage(ObjectMessage m)
    {
        Debug.Log("Message to lookObject");
        if (m.message == "player")
        {
           // sObject.refreshTime = 1;
            CameraController.Instance.SetInitialCameraPosition();
            isOnPlayer = true;
        }
        if(m.message == "target")
        {
            isOnPlayer=false;
        }

    }


    public override void setState(ObjectState state)
    {
        base.setState(state);
        player = Client.Instance.users[state.owner].player;
        player.lookObject = this;

        if(state.owner == Client.Instance.user.userState.sessionId){
           CameraController.Instance.SetLookObject(this); 
        }
        
    }
}