using System;
using System.Collections;
using System.Collections.Generic;
using Colyseus.Schema;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGame : MonoBehaviour
{

    public TextMeshProUGUI unreadMessagesText;
    public TextMeshProUGUI turnNumberText;
    private Client client;
    public Button profileButton;
    public Profile profileWindow;


    private int unreadMessages = 0;
    void Start()
    {
        client = Client.Instance;
        client.addReadyListener(Init);
    }
    void Init()
    {
        setListeners();
        profileButton.onClick.AddListener(onClickProfile);

    }

    void onClickProfile()
    {
        profileWindow.gameObject.SetActive(true);
       // gameObject.SetActive(false);
    }
    bool onNewMessage()
    {
        unreadMessages += 1;

        unreadMessagesText.text = "" + unreadMessages;
        return true;
    }

    ArrayList shotsArray = new ArrayList();


    public void setListeners()
    {
        client.room.State.winner.OnChange += setWinner;
        //Add on change tunr
        client.room.State.turnState.OnChange += onTurnStateChange;



    }

    private void setWinner(List<DataChange> changes)
    {
        throw new NotImplementedException();
    }


    void onTurnStateChange(List<Colyseus.Schema.DataChange> changes)
    {
        changes.ForEach(val =>
        {
            if (val.Field == "turn")
            {
               turnNumberText.text = "" + val.Value;
            }
        });


    }
}
