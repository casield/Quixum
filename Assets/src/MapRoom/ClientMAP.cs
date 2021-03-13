using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colyseus;
using Colyseus.Schema;

public class ClientMAP : MonoBehaviour
{
    private Colyseus.Client client;
    private Colyseus.Room<WorldState> room;


    public List<ObjectState> objects = new List<ObjectState>();
    public List<V3> startPositionsArray = new List<V3>();

    public bool localhost;

    // Start is called before the first frame update
    void Start()
    {
        connect();

    }
    public void connect()
    {
        if (localhost)
        {
            client = new Colyseus.Client("ws://localhost:6017");

        }
        else
        {
            client = new Colyseus.Client("ws://" + Client.serverIP + ":6017");
        }

        Debug.Log("Connected");
        join();


    }

    public void nextMap(MapDesigner map)
    {
        addToArrays(map.gameObject);
        if (objects.Count > 0)
        {
            sendObjects();
            sendStartPositions();
        }

    }

    public void addToArrays(GameObject mapGO)
    {
        objects.Clear();
        startPositionsArray.Clear();
        ObjectDesigner[] od = mapGO.GetComponentsInChildren<ObjectDesigner>();
        for (int i = 0; i < od.Length; i++)
        {

            ObjectDesigner element = od[i];
            if (element.isActiveAndEnabled && element.gameObject.activeSelf)
            {
                //element.gameObject.transform.localScale.Scale(new Vector3(2, 2, 2));
                objects.Add(element.toJson());

            }


        }
        StartPositionDesigner startPositions = mapGO.GetComponentInChildren<StartPositionDesigner>();
        Debug.Log(startPositions);
        foreach (Transform child in startPositions.transform)
        {
            V3 v3 = new V3();
            v3.x = child.position.x;
            v3.y = child.position.y;
            v3.z = child.position.z;
            startPositionsArray.Add(v3);
        }

    }

    async void join()
    {
        this.room = await client.Join<WorldState>("MapsRoom");
        StartCoroutine(startSending());

    }

    async void changeName(string name)
    {
        await this.room.Send("name", name);
    }

    IEnumerator startSending()
    {
        MapDesigner[] components = GameObject.FindObjectsOfType<MapDesigner>();

        foreach (var item in components)
        {
            Debug.Log("Sending map " + item.Name);
            changeName(item.Name);
            yield return new WaitForSeconds(1);
            nextMap(item);

            finish();
            yield return new WaitForSeconds(1);
        }



    }
    async void finish()
    {
        await this.room.Send("finish");
    }

    async void sendObjects()
    {
        await this.room.Send("objs", objects);
        Debug.Log(objects.Count + " objects sended");
    }
    async void sendStartPositions()
    {
        await this.room.Send("startPositions", startPositionsArray);
        Debug.Log(startPositionsArray.Count + " positions sended");
    }

}
