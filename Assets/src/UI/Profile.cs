using System;
using System.Collections;
using System.Collections.Generic;
using Colyseus.Schema;
using TMPro;
using UnityEngine;

public class Profile : MonoBehaviour
{
    public Client client;

    void Start()
    {
        Client.Instance.addReadyListener(Init);

    }

    private void Init()
    {
        client = Client.Instance;
        //client.room.State.turnState.OnChange += onTurnChange;
    }

    public void OnClose()
    {
        this.gameObject.SetActive(false);
    }

    public async void ChangeGauntlet(string type){
        if(client!= null){
           await client.room.Send("changeGauntlet",new ChangeGauntletMessage(){type=type});
        }
    }
}
