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
    public Material HoleWallMaterial;
    public Colyseus.Client client;
    public Colyseus.Room<GameState> room;
    public GUIController GUIController;

    bool destroyAllBool = false;

    public Prefabs prefabs;

    public GameObject ServerObjects;
    public GameObject PhysicsObjects;
    ArrayList tiles = new ArrayList();

    ArrayList funcArray = new ArrayList();

    // Start is called before the first frame update

    public Dictionary<string, SObject> objects = new Dictionary<string, SObject>();
    public Dictionary<string, SObject> updateObjects = new Dictionary<string, SObject>();
    public Dictionary<string, SObject> golfballs = new Dictionary<string, SObject>();
    public bool drawPhysics;

    public PlayerInput playerInput;

    public static Client Instance { get; private set; }

    private string mapName = "";

    public bool localhost;

    public float time = 0;

    public static string serverIP = "3.13.63.39";
    public UserState userState;

    void Awake()
    {
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

        //createRoom("bb");
        JoinOrCreateRoom();


    }

    public void addReadyListener(UnityAction func)
    {
        if (this.room != null)
        {
            func();
        }
        else
        {
            funcArray.Add(func);
        }

        //func();
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
        Dictionary<string, object> options = new Dictionary<string, object>();


        this.room = await client.Create<GameState>("GameRoom", options);
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
            time = val;
            GUIConsole.Instance.deltaTime = time;
        });
        room.OnMessage<string>("error", onErrorMessage);
        room.OnMessage<string>("info", onInfoMessage);
        room.OnMessage<ObjectMessage>("objectM", onObjectMessage);


    }

    private void onObjectMessage(ObjectMessage obj)
    {
        objects[obj.uID].onMessage(obj);
    }
    private void onErrorMessage(string obj)
    {
        ServerMessagesController.Instance.showError(obj);
    }
    private void onInfoMessage(string obj)
    {
        GameMessages.Instance.showMessage(obj, 2);
    }


    public void setListeners()
    {
        room.OnError += (code, message) => Debug.LogError("ERROR, code =>" + code + ", message => " + message);
        room.State.world.objects.OnChange += OnChangeObjects;
        room.State.world.objects.OnAdd += AddObject;
        room.State.world.objects.OnRemove += RemoveObject;
        room.OnLeave += onLeave;
        room.State.users.OnAdd += onUserAdded;
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

    public void removeListeners()
    {
        room.State.world.objects.OnChange -= OnChangeObjects;
        room.State.world.objects.OnAdd -= AddObject;
        room.State.world.objects.OnRemove -= RemoveObject;

        room.OnLeave -= onLeave;

        Character.Instance.removeListeners();
    }
    void OnChangeObjects(ObjectState body, string i)
    {

        if (objects.ContainsKey(i))
        {
            SObject itt = objects[i];
            Vector3 desPos = new Vector3(itt.state.position.x, itt.state.position.y, itt.state.position.z);
            if (!updateObjects.ContainsKey(body.uID))
            {
                updateObjects.Add(itt.state.uID, itt);
            }
        }
        else
        {
            Debug.Log("Couldn't find key on ChangeObjects " + body.uID);
        }

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

        MapSchema<ObjectState> array = new MapSchema<ObjectState>();
        if (ob.instantiate)
        {
            array.Add("" + i, ob);
            createObjects(array);
        }
    }

    void createObjects(MapSchema<ObjectState> objects)
    {
        objects.ForEach((string s, ObjectState ob) =>
        {
            GameObject gameOb = null;
            SObject serverObject = null;
            if (ob.instantiate)
            {
                gameOb = createObject(ob);
                serverObject = new SObject(gameOb, ob);
                this.objects.Add(ob.uID, serverObject);
            }

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
        if (ob.mesh.Length == 0)
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
        if (ob.mesh.Length > 0)
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


    void onLeave(NativeWebSocket.WebSocketCloseCode e)
    {
        Debug.Log("OnLeave method");
        destroyAllBool = true;
    }

    void destroyAll()
    {
        if (destroyAllBool)
        {
            foreach (var item in objects)
            {
                Debug.Log("Destroying " + item.Value.gameObject.name);
                try
                {
                    Destroy(item.Value.gameObject);
                }
                catch (KeyNotFoundException e)
                {
                    Debug.Log("Couldn't destroy " + item.Value.ToString());
                }

            }
            objects.Clear();
            destroyAllBool = false;
        }

    }


    // Update is called once per frame
    void Update()
    {
        destroyAll();

        foreach (var item in updateObjects)
        {

            SObject itt = item.Value;
            Vector3 desPos = new Vector3(itt.state.position.x, itt.state.position.y, itt.state.position.z);
            Quaternion desQuat = new Quaternion(itt.state.quaternion.x, itt.state.quaternion.y, itt.state.quaternion.z, itt.state.quaternion.w);
            if (itt.gameObject != null)
            {
                itt.gameObject.transform.position = Vector3.Lerp(itt.gameObject.transform.position, desPos, 1);
                itt.gameObject.transform.rotation = new Quaternion(itt.state.quaternion.x, itt.state.quaternion.y, itt.state.quaternion.z, itt.state.quaternion.w);

                if (typeof(BoxObject).IsInstanceOfType(itt.state))
                {
                    
                    BoxObject o = (BoxObject)itt.state;
                    Debug.Log(Json.SerializeToString(o.halfSize));
                    itt.gameObject.transform.localScale = new Vector3(o.halfSize.x,o.halfSize.y,o.halfSize.z);
                }

                 if (typeof(SphereObject).IsInstanceOfType(itt.state))
                {
                    SphereObject o = (SphereObject)itt.state;
                    itt.gameObject.transform.localScale = new Vector3(o.radius,o.radius,o.radius);
                }


                // itt.gameObject.transform.rotation = Quaternion.Lerp(itt.gameObject.transform.rotation, desQuat, 1);
            }
            else
            {
                Debug.Log("itt.GameObject is null");
            }

        }
        updateObjects.Clear();
        // Debug.Log(uiblocker.BlockedByUI);
    }

    /// <summary>
    /// Callback sent to all game objects before the application is quit.
    /// </summary>
    async void OnApplicationQuit()
    {
        await room.Leave(false);
        await room.Connection.Close();

    }
}
