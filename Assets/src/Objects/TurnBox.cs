using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBox : MonoBehaviour, IConnectedObject
{

    // Start is called before the first frame update
    ObjectState objectState;
    void Start()
    {
        Client.Instance.addReadyListener(init);
    }

    private void init()
    {
        PlanningUI.Instance.setObject(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public async void sendMessageToRoom(string mess)
    {
        ObjectMessage m = new ObjectMessage();
        m.uID = this.objectState.uID;
        m.room = Client.Instance.room.SessionId;
        m.message = mess;
        await Client.Instance.room.Send("objectMessage",m);
    }
    public void onMessage(ObjectMessage m)
    {
        
    }

    public void setState(ObjectState state)
    {
        this.objectState = state;
    }
}
