using System;
using System.Collections;
using System.Collections.Generic;
using Colyseus.Schema;
using TMPro;
using UnityEngine;

public class Profile : MonoBehaviour
{
    Client client;
    public TextMeshProUGUI turnNumberText;
    public CloseButton closeButton;
    public GameObject InGameUI;

    public GameObject powerLabel_Prefab;
    public GameObject ShopLayOut;
    private bool addedBagChanges = false;

    void Start()
    {
        Client.Instance.addReadyListener(init);

    }

    private void init()
    {
        client = Client.Instance;
        //client.room.State.turnState.OnChange += onTurnChange;
        closeButton.onClose(onClose);
    }


    void onClose()
    {
        InGameUI.SetActive(true);

    }

    private void onTurnChange(List<DataChange> changes)
    {
        changes.ForEach(val =>
        {

            if (val.Field == "turn")
            {
                turnNumberText.text = "Turn " + val.Value;
            }
        });
        if (!addedBagChanges)
        {
           /* client.room.State.turnState.players[client.room.SessionId].bag.OnChange += onBagChange;*/
            addedBagChanges = true;
        }

    }



    // Update is called once per frame
    void Update()
    {

    }
}
