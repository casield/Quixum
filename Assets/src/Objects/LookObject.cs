using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookObject : MonoBehaviour, ConnectedObject{
    private SObject sObject;

    private void Start() {
        sObject = GetComponent<SObject>();

        CameraController.Instance.SetLookObject(this);
    }

    public void onMessage(ObjectMessage m)
    {
        Debug.Log("Message to lookObject");
       
    }

    public void sendMessageToRoom(string m)
    {
        //throw new NotImplementedException();
    }

    public void setState(ObjectState state)
    {
        //throw new NotImplementedException();
    }
}
