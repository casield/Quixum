using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colyseus;
using Colyseus.Schema;
using GameDevWare.Serialization;
using TMPro;
using System;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ClientDebug : MonoBehaviour
{
    public bool localhost;
    private Colyseus.Client client;
    private Room<GameState> room;
    public IndexedDictionary<string, SObject> objects = new IndexedDictionary<string, SObject>();

    public GameObject ServerObjects;

    float lastTime = 0;
    public Material material;

    void Start()
    {
        initServer();

    }
    private void initServer()
    {



        if (localhost)
        {
            client = new Colyseus.Client("ws://localhost:6017");
        }
        else
        {
            Debug.Log("Conectando a drokt.com");
            client = new Colyseus.Client("ws://"+Client.serverIP+":6017");
        }

        //createRoom("bb");
        JoinOrCreateRoom();


    }

    private async void JoinOrCreateRoom()
    {
        this.room = await client.JoinOrCreate<GameState>("GameRoom",new Dictionary<string, object>() { });

        //room.State["world"].onChange += OnChangeObjects;
        room.State.world.objects.OnChange += OnChangeObjects;
        room.State.world.objects.OnAdd += AddObject;

        this.room.OnMessage<float>("time", (val) =>
        {
           // Debug.Log("Delta time" + (val - lastTime));
           GUIConsole.Instance.deltaTime = val;
        });
    }

    private void AddObject(ObjectState ob, string i)
    {
        MapSchema<ObjectState> array = new MapSchema<ObjectState>();
        if (ob.instantiate)
        {
            array.Add("" + i, ob);
            createObjects(array);
        }
        //ob.insta
    }

    private void OnChangeObjects(ObjectState value, string i)
    {
        if (objects.ContainsKey(i))
        {
            SObject itt = objects[i];
            itt.gameObject.transform.position = new Vector3(itt.state.position.x, itt.state.position.y, itt.state.position.z);
            itt.gameObject.transform.rotation = new Quaternion(itt.state.quaternion.x, itt.state.quaternion.y, itt.state.quaternion.z, itt.state.quaternion.w);

        }
        else
        {
            //Debug.Log("Couldn't find key on ChangeObjects " + body.uID);
        }

    }

    void createObjects(MapSchema<ObjectState> objects)
    {

        
        objects.ForEach((string s, ObjectState ob) =>
        {
            Debug.Log("Creating "+ob.type + " / ["+((BoxObject)ob).halfSize.x+"]");
            GameObject gameOb = null;

            SObject serverObject = null;

            if (ob.instantiate)
            {
                if (ob.GetType().Equals(typeof(SphereObject)))
                {
                    SphereObject sphereState = (SphereObject)ob;
                    if (ob.mesh.Length > 0)
                    {

                        //Debug.Log("Instantiating a mesh");
                        UnityEngine.Object prefab = Resources.Load(ob.mesh); // Assets/Resources/Prefabs/prefab1.FBX
                        gameOb = (GameObject)Instantiate(prefab);
                    }
                    else
                    {
                        gameOb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        gameOb.GetComponent<Renderer>().material = material;
                    }

                    gameOb.name = "Esfera (" + ob.uID + ")";
                    float size = (sphereState.radius) * 2;
                    gameOb.transform.localScale = new Vector3(size, size, size);
                    serverObject = gameOb.AddComponent<SObject>();
                    serverObject.setState(sphereState);

                    this.objects.Add(s, serverObject);
                }
                if (ob.GetType().Equals(typeof(BoxObject)))
                {

                    BoxObject boxState = (BoxObject)ob;
                    if (ob.mesh.Length > 0)
                    {

                        //Debug.Log("Instantiating a mesh");
                        UnityEngine.Object prefab = Resources.Load(ob.mesh); // Assets/Resources/Prefabs/prefab1.FBX
                        gameOb = (GameObject)Instantiate(prefab);
                    }
                    else
                    {
                        gameOb = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        gameOb.GetComponent<Renderer>().material = material;
                    }

                    gameOb.name = ob.type+"(" + ob.uID + ")";
                    Vector3 size = new Vector3(boxState.halfSize.x, boxState.halfSize.y, boxState.halfSize.z);
                    size.Scale(new Vector3(2, 2, 2));
                    gameOb.transform.localScale = size;

                    serverObject = gameOb.AddComponent<SObject>();
                    serverObject.setState(boxState);
                    this.objects.Add(s, serverObject);

                }

                gameOb.transform.parent = ServerObjects.transform;

            gameOb.transform.position = new Vector3(ob.position.x, ob.position.y, ob.position.z);
                gameOb.transform.rotation = new Quaternion(ob.quaternion.x, ob.quaternion.y, ob.quaternion.z, ob.quaternion.w);

            }

            if (gameOb != null)
            {
                /*gameOb.name = ob.type + " [" + ob.uID + "]";
                if (ob.owner.sessionId != "")
                {
                    gameOb.name += "=> " + ob.owner.sessionId;
                }
                if (ob.type == "golfball")
                {
                    Debug.Log("Creating GolfBall");
                    GolfBall objComp = gameOb.AddComponent<GolfBall>();
                    gameOb.layer = 8;
                    objComp.setState(ob);
                   // mMaterial = BallMaterial;
                    //this.golfballs.Add(ob.owner.sessionId, serverObject);
                }
                if (ob.type == "player")
                {
                    Debug.Log("Creating player " + ob.owner.sessionId);
                    Player objComp = gameOb.AddComponent<Player>();
                    gameOb.layer = 8;
                    objComp.setState(ob);
                    //mMaterial = BallMaterial;
                    // this.golfballs.Add(ob.owner.sessionId, serverObject);
                }
                if (ob.type == "trownobj")
                {
                   // mMaterial = WallMaterial;
                }
                if (ob.type == "characer")
                {

                }
                if (ob.mesh.Length == 0)
                {
                   // gameOb.GetComponent<Renderer>().material = mMaterial;
                }

                gameOb.transform.position = new Vector3(ob.position.x, ob.position.y, ob.position.z);
                gameOb.transform.rotation = new Quaternion(ob.quaternion.x, ob.quaternion.y, ob.quaternion.z, ob.quaternion.w);
*/

            }




        });

    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>

    async void moveR()
    {
        //await room.Send("moveR");
    }

    int tick = 0;
    int timetosend = 40;
    void Update()
    {
        if (tick == timetosend)
        {
            tick = 0;
            moveR();
        }

        tick++;
    }

    async void OnApplicationQuit()
    {
        await room.Leave(false);
        await room.Connection.Close();
        Debug.Log("Application ending after " + Time.time + " seconds");
    }
}