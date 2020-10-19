using System;
using System.Collections;
using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;

public class Bag_UI : MonoBehaviour
{
    // Start is called before the first frame update
    public Client client;
    private bool addedBagChanges = false;
    public List<GameObject> slotsArray = new List<GameObject>();
    public GameObject powerLabel_Prefab;
    public bool isInGame = false;

    void Start()
    {
        Client.Instance.addReadyListener(Init);
    }

    public void Init()
    {
        client = Client.Instance;
        client.room.State.turnState.OnChange += onTurnChange;

    }

    private void onSlotAddOrChange(PowerState value, int key)
    {
        Debug.Log("OnSlotAdd " + value.type);
        if (value.type != "empty")
        {
            PowerLabel pl = slotsArray[key].GetComponentInChildren<PowerLabel>();
            if (pl != null)
            {
                Destroy(slotsArray[key].GetComponentInChildren<PowerLabel>().gameObject);
            }

            PowerLabel p = Instantiate(powerLabel_Prefab).GetComponent<PowerLabel>();
            p.displayCircle = isInGame;
            p.transform.parent = slotsArray[key].transform;
            

            p.transform.localPosition = Vector3.zero;
            if (!p.displayCircle)
            {
                float multiply = 1 / p.transform.parent.localScale.x;
                float multiplyY = 1 / p.transform.parent.localScale.y;
                p.transform.localScale = new Vector3(multiply, multiplyY, multiply);
            }
            else
            {
                p.transform.localScale = new Vector3(1, 1, 1);
            }
            p.load(value);

            //((RectTransform)p.transform).sizeDelta = ((RectTransform)p.transform.parent.transform).sizeDelta;
            p.onClick(() => { p.activate(); });
        }
        else
        {
            PowerLabel pl = slotsArray[key].GetComponentInChildren<PowerLabel>();
            if (pl != null)
            {
                Destroy(slotsArray[key].GetComponentInChildren<PowerLabel>().gameObject);
            }
        }
    }


    private void onTurnChange(List<DataChange> changes)
    {
        if (!addedBagChanges)
        {
            {
                client.room.State.turnState.players[client.room.SessionId].bag.slots.OnAdd += onSlotAddOrChange;
                client.room.State.turnState.players[client.room.SessionId].bag.slots.OnChange += onSlotAddOrChange;
                //client.room.State.turnState.players[client.room.SessionId].bag.slots.OnChange += onSlotAdd;

                addedBagChanges = true;
            }



        }
    }


}
