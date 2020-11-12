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
    public Dictionary<string, IObstacle> obstacles = new Dictionary<string, IObstacle>();

    ArrayList funcArray = new ArrayList();

    // Start is called before the first frame update

    public Dictionary<string, SObject> objects = new Dictionary<string, SObject>();
    public Dictionary<string, SObject> golfballs = new Dictionary<string, SObject>();
    public bool drawPhysics;

    public PlayerInput playerInput;

    public static Client Instance { get; private set; }

    private string mapName = "";

    public bool localhost;
    
    public float time = 0;

    public static string serverIP = "3.131.152.148"; 

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
            client = new Colyseus.Client("ws://"+serverIP+":6017");
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
        setListeners();
        readMessages();

        this.gameObject.name +=" ["+this.room.SessionId+"]";

    }

    private void readMessages()
    {
        room.OnMessage<string>("changeMap", onChangeMap);

        room.OnMessage<string>("error", onErrorMessage);

        room.OnMessage<BoxObject>("trowMode", onTrowMode);
        room.OnMessage<bool>("exitTrowMode", exitTrowMode);
        room.OnMessage<ObstacleState>("LongNeck", onLongNeck);
        room.OnMessage<ObjectMessage>("objectM", onObjectMessage);
        room.OnMessage<float>("time", (val)=>{
            time = val;
        });


    }

    private void onObjectMessage(ObjectMessage obj)
    {
        objects[obj.uID].onMessage(obj);
    }

    private void onLongNeck(ObstacleState state)
    {
        obstacles[state.uID].activate();
    }
    private void exitTrowMode(bool exit)
    {
        Character.Instance.showCharacter();
        Character.Instance.enabled = true;
        TrowMode.Instance.enabled = false;
        // TrowMode.Instance.objectState = obj;
    }

    private void onTrowMode(BoxObject obj)
    {
        Character.Instance.hideCharacter();
        Character.Instance.enabled = false;
        TrowMode.Instance.enabled = true;
        TrowMode.Instance.objectState = obj;
    }

    private void onErrorMessage(string obj)
    {
        ServerMessagesController.Instance.showError(obj);
    }

    public void setListeners()
    {


        room.OnError += (code, message) => Debug.LogError("ERROR, code =>" + code + ", message => " + message);
        room.State.world.objects.OnChange += OnChangeObjects;
        room.State.world.objects.OnAdd += AddObject;
        room.State.world.objects.OnRemove += RemoveObject;
        room.State.world.tiles.OnAdd += OnAddTiles;
        room.State.world.obstacles.OnAdd += OnAddObstacles;
        room.OnLeave += onLeave;


        //Call all onready

        Instance = this;
        Character.Instance.setListeners();
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

        room.State.world.tiles.OnAdd -= OnAddTiles;
        room.State.world.obstacles.OnAdd -= OnAddObstacles;
        room.OnLeave -= onLeave;

        Character.Instance.removeListeners();
    }

    private void onChangeMap(object obj)
    {
        Debug.Log("Change Map");


        foreach (KeyValuePair<string, IObstacle> child in obstacles)
        {
            GameObject.Destroy(child.Value.gObject);
        }
        foreach (GameObject child in tiles)
        {
            GameObject.Destroy(child);
        }
        //this.golfballs.Clear();
        // this.objects.Clear();
        //this.obstacles.Clear();
        //removeListeners();
        //setListeners();
    }



    private void OnAddTiles(ObjectState value, int key)
    {

        UnityEngine.Object prefab = Resources.Load("Tiles/" + value.type); // Assets/Resources/Prefabs/prefab1.FBX
        GameObject t = (GameObject)Instantiate(prefab, new Vector3(value.position.x, value.position.y, value.position.z), Quaternion.identity);
        t.transform.parent = ServerObjects.transform;
        t.transform.localScale = new Vector3(1, 1, 1);
        t.transform.rotation = new Quaternion(value.quaternion.x, value.quaternion.y, value.quaternion.z, value.quaternion.w);

        tiles.Add(t);
    }
    private void OnAddObstacles(ObstacleState value, int key)
    {
        Debug.Log("Create obstacles in " + this.mapName);
        UnityEngine.Object prefab = Resources.Load("Objects/" + value.objectname); // Assets/Resources/Prefabs/prefab1.FBX
        GameObject t = (GameObject)Instantiate(prefab, new Vector3(value.position.x, value.position.y, value.position.z), Quaternion.identity);
        t.transform.parent = ServerObjects.transform;
        t.transform.localScale = new Vector3(1, 1, 1);
        t.transform.rotation = new Quaternion(value.quaternion.x, value.quaternion.y, value.quaternion.z, value.quaternion.w);

        t.name += " ["+value.uID+"]";
        IObstacle obstacle = t.GetComponent<IObstacle>();
        obstacles.Add(value.uID, obstacle);

    }


    void OnChangeObjects(ObjectState body, string i)
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
    void RemoveObject(ObjectState body, string i)
    {
        if (body.instantiate)
        {
            Debug.Log("Eliminando el objeto #" + body.type);
            if (objects.ContainsKey(i))
            {
                SObject obj = objects[i];
                DestroyImmediate(obj.gameObject);
                objects.Remove(i);
            }
            else
            {
                Debug.LogError("Couldn't destroy object " + body.type + " / " + i);
            }
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
        //ob.instantiate = true;


    }

    void createObjects(MapSchema<ObjectState> objects)
    {


        objects.ForEach((string s, ObjectState ob) =>
        {
            GameObject gameOb = null;

            SObject serverObject = null;
            var mMaterial = material;

            if (ob.instantiate)
            {
                if (ob.GetType().Equals(typeof(SphereObject)))
                {
                    SphereObject sphereState = (SphereObject)ob;
                    if (ob.mesh.Length > 0)
                    {

                        //Debug.Log("Instantiating a mesh");
                        UnityEngine.Object prefab = Resources.Load( ob.mesh); // Assets/Resources/Prefabs/prefab1.FBX
                        gameOb = (GameObject)Instantiate(prefab);
                    }
                    else
                    {
                        gameOb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    }

                    gameOb.name = "Esfera (" + ob.uID + ")";
                    float size = (sphereState.radius) * 2;
                    gameOb.transform.localScale = new Vector3(size, size, size);
                    serverObject = new SObject(gameOb, sphereState);

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
                    }

                    gameOb.name = "Box (" + ob.uID + ")";
                    Vector3 size = new Vector3(boxState.halfSize.x, boxState.halfSize.y, boxState.halfSize.z);
                    size.Scale(new Vector3(2, 2, 2));
                    gameOb.transform.localScale = size;

                    serverObject = new SObject(gameOb, boxState);
                    this.objects.Add(s, serverObject);

                }

                gameOb.transform.parent = ServerObjects.transform;



            }

            if (gameOb != null)
            {
                gameOb.name = ob.type + " ["+ob.uID+"]";
                if(ob.owner.sessionId != ""){
                    gameOb.name+= "=> "+ob.owner.sessionId;
                }
                if (ob.type == "golfball")
                {
                    Debug.Log("Creating GolfBall");
                    GolfBall objComp = gameOb.AddComponent<GolfBall>();
                    gameOb.layer = 8;
                    objComp.setState(ob);
                    mMaterial = BallMaterial;
                    this.golfballs.Add(ob.owner.sessionId, serverObject);
                }
                if (ob.type == "player")
                {
                    Debug.Log("Creating player "+ob.owner.sessionId);
                    Player objComp = gameOb.AddComponent<Player>();
                    gameOb.layer = 8;
                    objComp.setState(ob);
                }
                if (ob.type == "trownobj")
                {
                    mMaterial = WallMaterial;
                }
                if(ob.type =="characer"){
                    
                }
                if (ob.mesh.Length == 0)
                {
                    gameOb.GetComponent<Renderer>().material = mMaterial;
                }

                gameOb.transform.position = new Vector3(ob.position.x, ob.position.y, ob.position.z);
                gameOb.transform.rotation = new Quaternion(ob.quaternion.x, ob.quaternion.y, ob.quaternion.z, ob.quaternion.w);


            }

            


        });

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
