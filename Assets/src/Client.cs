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

public class Client : MonoBehaviour
{
    public Material material;
    public Material WallMaterial;
    public Material BallMaterial;
    public Colyseus.Client client;
    public Colyseus.Room<GameState> room;

    public GameObject ServerObjects;
    ArrayList funcArray = new ArrayList();
    public Dictionary<string, SObject> objects = new Dictionary<string, SObject>();
    public Dictionary<string, SObject> golfballs = new Dictionary<string, SObject>();
    public PlayerInput playerInput;
    public static Client Instance { get; private set; }
    public bool localhost;
    public static string serverIP = "3.13.63.39";
    public UserState userState;

    void Awake()
    {
        ServerObjects = new GameObject("Server Objects");
        
        Instance = this;
    }

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
            client = new Colyseus.Client("ws://" + serverIP + ":6017");
        }
        JoinOrCreateRoom();


    }

    public void addReadyListener(UnityAction func)
    {
        if (this.room != null && this.userState != null)
        {
            func();
        }
        else
        {
            funcArray.Add(func);
        }
    }

    public async void leaveLastRoom()
    {
        if (this.room != null)
        {
            await this.room.Leave();
        }
    }
    public async void joinRoom(string id)
    {

        if (this.room != null)
        {
            if (this.room.Id != id)
            {
                leaveLastRoom();
                this.room = await client.JoinById<GameState>(id);
                name = this.room.Name;

                Instance = this;
                setListeners();
            }
        }
        else
        {
            this.room = await client.JoinById<GameState>(id);
            Instance = this;
            setListeners();
        }

    }

    public async void createRoom(string name)
    {
        leaveLastRoom();
        this.room = await client.Create<GameState>("GameRoom");
        await this.room.Send("setName", name);
        setListeners();
    }

    public async void JoinOrCreateRoom()
    {
        leaveLastRoom();
        this.room = await client.JoinOrCreate<GameState>("GameRoom");
        readMessages();
        setListeners();


        this.gameObject.name += " [" + this.room.SessionId + "]";

    }

    private void readMessages()
    {
        Debug.Log("Read messages");
        room.OnMessage<float>("time", (val) =>
        {
            GUIConsole.Instance.deltaTime = val;
        });
        room.OnMessage<string>("error", onErrorMessage);
        room.OnMessage<string>("info", onInfoMessage);
        room.OnMessage<ObjectMessage>("objectM", onObjectMessage);


    }

    private void onObjectMessage(ObjectMessage obj)
    {
        if (objects.ContainsKey(obj.uID))
        {
            objects[obj.uID].onMessage(obj);
        }
        else
        {
            Debug.Log("Could'nt find " + obj.uID);
        }

    }
    private void onErrorMessage(string obj)
    {
        ServerMessagesController.Instance.showError(obj);
    }
    private void onInfoMessage(string obj)
    {
        GameMessages.Instance.showMessage(obj, 2);
    }


    private void onUserAdded(UserState value, string key)
    {
        if (value.sessionId == room.SessionId)
        {
            this.userState = value;
        }
        Instance = this;
        foreach (UnityAction func in funcArray)
        {
            func();
        }
    }
    public void setListeners()
    {
        room.OnError += (code, message) => Debug.LogError("ERROR, code =>" + code + ", message => " + message);
        room.State.world.objects.OnAdd += AddObject;
        room.State.world.objects.OnRemove += RemoveObject;
        room.State.users.OnAdd += onUserAdded;
    }

    public void removeListeners()
    {
        room.State.world.objects.OnAdd -= AddObject;
        room.State.world.objects.OnRemove -= RemoveObject;


        Character.Instance.removeListeners();
    }
    void RemoveObject(ObjectState body, string i)
    {
        if (objects.ContainsKey(i))
        {
            SObject obj = objects[i];
            Destroy(obj.gameObject);
            objects.Remove(i);
            Debug.Log("Eliminando el objeto " + obj.gameObject.name);
        }
        else
        {
            Debug.LogError("Couldn't destroy object " + body.type + " / " + body.uID);
        }
    }
    void AddObject(ObjectState ob, string i)
    {
        Debug.Log("Creating Object");
        MapSchema<ObjectState> array = new MapSchema<ObjectState>();
        array.Add("" + i, ob);
        createObjects(array);
    }
    void createObjects(MapSchema<ObjectState> objects)
    {
        objects.ForEach((string s, ObjectState ob) =>
        {
            GameObject gameOb = null;
            SObject serverObject = null;
            gameOb = createObject(ob);
            serverObject = gameOb.AddComponent<SObject>();
            serverObject.setState(ob);
            this.objects.Add(ob.uID, serverObject);

            if (gameOb != null)
            {
                setUpGameObject(ob, gameOb, serverObject);
            }
        });
    }

    private void setUpGameObject(ObjectState ob, GameObject gameOb, SObject serverObject)
    {
        Material mMaterial = material;
        gameOb.name = ob.type + " [" + ob.uID + "]";
        if (ob.owner.sessionId != "")
        {
            gameOb.name += "=> " + ob.owner.sessionId;
        }
        if (ob.type == "GolfBall2")
        {
            Debug.Log("Creating GolfBall");
            GolfBall objComp = gameOb.AddComponent<GolfBall>();
            gameOb.layer = 8;
            objComp.setState(ob);
            mMaterial = BallMaterial;
            this.golfballs.Add(ob.owner.sessionId, serverObject);
        }
        if (ob.type == "Player2")
        {
            Debug.Log("Creating player " + ob.owner.sessionId);
            Player objComp = gameOb.AddComponent<Player>();
            gameOb.layer = 8;
            objComp.setState(ob);
        }
        if (ob.type == "TurnBox")
        {
            Debug.Log("Creating TurnBox ");
            TurnBox objComp = gameOb.AddComponent<TurnBox>();
            gameOb.layer = 8;
            objComp.setState(ob);
        }
        if (ob.mesh == null)
        {
            gameOb.GetComponent<Renderer>().material = mMaterial;
        }

        gameOb.transform.position = new Vector3(ob.position.x, ob.position.y, ob.position.z);
        gameOb.transform.rotation = new Quaternion(ob.quaternion.x, ob.quaternion.y, ob.quaternion.z, ob.quaternion.w);
    }

    GameObject createObject(ObjectState ob)
    {
        GameObject gameOb;
        bool isBox = ob.GetType().Equals(typeof(BoxObject));
        PrimitiveType primitiveType = isBox ? PrimitiveType.Cube : PrimitiveType.Sphere;
        if (ob.mesh != null)
        {
            UnityEngine.Object prefab = Resources.Load(ob.mesh); // Assets/Resources/Prefabs/prefab1.FBX
            gameOb = (GameObject)Instantiate(prefab);
        }
        else
        {
            gameOb = GameObject.CreatePrimitive(primitiveType);
        }
        gameOb.name = "Object (" + ob.uID + ")";
        Vector3 size;
        if (isBox)
        {
            BoxObject boxState = (BoxObject)ob;
            size = new Vector3(boxState.halfSize.x, boxState.halfSize.y, boxState.halfSize.z);
        }
        else
        {
            SphereObject boxState = (SphereObject)ob;
            size = new Vector3(boxState.radius, boxState.radius, boxState.radius);
        }

        size.Scale(new Vector3(2, 2, 2));

        gameOb.transform.localScale = size;
        gameOb.transform.parent = ServerObjects.transform;
        return gameOb;
    }

    async void OnApplicationQuit()
    {
        await room.Leave(false);
        await room.Connection.Close();
        Destroy(ServerObjects);

    }
}
