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
        client.room.State.turnState.OnChange += onTurnChange;
        closeButton.onClose(onClose);
    }

    private void onBagChange(List<DataChange> changes)
    {
        foreach (var item in changes)
        {
            if (item.Field == "shop")
            {

                ArraySchema<PowerState> mapS = (ArraySchema<PowerState>)item.Value;
                foreach (Transform child in ShopLayOut.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                mapS.ForEach((powerState) =>
                {
                    PowerLabel p = Instantiate(powerLabel_Prefab, ShopLayOut.transform).GetComponent<PowerLabel>();
                    p.load(powerState);

                    p.onClick(() =>
                    {
                        p.buy();
                    });
                });
            }
        }
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
