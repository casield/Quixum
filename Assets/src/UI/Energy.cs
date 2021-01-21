using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Colyseus.Schema;

public class Energy : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textPercent;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Client.Instance.addReadyListener(init);
    }

    private void init()
    {
            Client.Instance.room.State.users.OnAdd+=onAddUser;
            Client.Instance.room.State.users.OnChange+=onAddUser;
        

        // Client.Instance.room.State.users.OnChange+=OnUserChange;
    }

    private void onAddUser(UserState value, string key)
    {
        if(value.sessionId == Client.Instance.room.SessionId){
            textPercent.text = "" + value.energy;
        }
        
    }

    
}
