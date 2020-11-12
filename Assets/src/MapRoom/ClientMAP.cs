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
    public List<ObjectState> tiles = new List<ObjectState>();
    public List<ObstacleState> obstacles = new List<ObstacleState>();

    public bool localhost;

    // Start is called before the first frame update
    void Start()
    {
        connect();

    }
    public void connect()
    {
        if(localhost){
            client = new Colyseus.Client("ws://localhost:6017");
            
        }else{
            client = new Colyseus.Client("ws://"+Client.serverIP+":6017");
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
        }
        if (tiles.Count > 0)
        {
            sendTiles();

        }
        if (obstacles.Count > 0)
        {
            sendObstacles(map.gameObject);
        }

    }

    public void addToArrays(GameObject mapGO)
    {
        objects.Clear();
        tiles.Clear();
        obstacles.Clear();
        ObjectDesigner[] od = mapGO.GetComponentsInChildren<ObjectDesigner>();
        for (int i = 0; i < od.Length; i++)
        {
            ObjectDesigner element = od[i];
            objects.Add(element.toJson());
        }

        TileDesigner[] td = mapGO.GetComponentsInChildren<TileDesigner>();
        for (int i = 0; i < td.Length; i++)
        {
            TileDesigner element = td[i];
            tiles.Add(element.toJson());
        }

        ObstacleDesigner[] bsd = mapGO.GetComponentsInChildren<ObstacleDesigner>();
        for (int i = 0; i < bsd.Length; i++)
        {
            ObstacleDesigner element = bsd[i];
            obstacles.Add(element.toJson());

        }
    }

    async void sendExtraPoints(NamedPoint[] points)
    {
        await this.room.Send("extraPoints", points);
        new WaitForSeconds(1);
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

    async void sendTiles()
    {
        await this.room.Send("tiles", tiles);
        Debug.Log(tiles.Count + " tiles sended");
    }
    async void sendObstacles(GameObject mapGO)
    {
        await this.room.Send("obstacles", obstacles);
        Debug.Log(obstacles.Count + " obstacles sended");
        /*ObstacleDesigner[] bsd = mapGO.GetComponentsInChildren<ObstacleDesigner>();
        for (int i = 0; i < bsd.Length; i++)
        {
            ObstacleDesigner element = bsd[i];
            sendExtraPoints(element.giveExtraPoints());

        }*/
    }

}
