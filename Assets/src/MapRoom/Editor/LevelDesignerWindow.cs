
/*
#if UNITY_EDITOR
using UnityEngine;

using UnityEditor;
using UnityEngine.SceneManagement;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

public class Model_Map {
        public ObjectId _id { set; get; }
        
        public Model_Object[] objects { set; get; }
        public static string name { set; get; }
        public Object ballspawn {set;get;}
}

public class Model_Object{
    public Vector3 position {set;get;}
    public Quaternion quat {set;get;}

    public string uID {set;get;}
    public Vector3 halfSize {set;get;}
    public float radius {set;get;}
    public Vector3[] verts {set;get;}
    public int[] faces {set;get;}
    public string material {set;get;}


}

public class LevelDesignerWindow : EditorWindow
{
    Rect botonesRect = new Rect();
    Rect materialesRect = new Rect();

    public GameObject MAP;
    public string MapName = "";
    public ClientMAP client;
    Material material = null;




    [MenuItem("drokt.com/Level Designer")]
    static void OpenWindow()
    {
    
    LevelDesignerWindow window = (LevelDesignerWindow)GetWindow(typeof(LevelDesignerWindow));
    window.Show();
    }
void setLayouts()
{
    botonesRect.width = Screen.width;
    botonesRect.height = 120;

    materialesRect.width = Screen.width;
    materialesRect.height = 120;
    materialesRect.y = 150;
}

/// <summary>
/// OnGUI is called for rendering and handling GUI events.
/// This function can be called multiple times per frame (one call per event).
/// </summary>
void OnGUI()
{
    Scene scene = SceneManager.GetActiveScene();

    if (scene.name == "MapScene")
    {
        setLayouts();

        botones();
        materiales();

    }


}
void botones()
{
    if (MAP == null && client == null)
    {
        MAP = GameObject.Find("MAP");

        client = GameObject.Find("ClientMaps").GetComponent<ClientMAP>();
    }



    GUILayout.BeginArea(botonesRect);

    //client.MapName = MapName;

    if (GUILayout.Button("Create box"))
    {
        GameObject o = GameObject.CreatePrimitive(PrimitiveType.Cube);
        DestroyImmediate(o.GetComponent<BoxCollider>());
        o.transform.localScale = new Vector3(5, 5, 5);
        o.transform.SetParent(MAP.transform);
        o.GetComponent<Renderer>().material = material;
        ObjectDesigner odesig = o.AddComponent<ObjectDesigner>();
        odesig.uID = ObjectDesigner.uniqueId();
        odesig.gameObject.name = odesig.uID;
        odesig.type = "box";
    }

    GUILayout.Space(5);

    if (GUILayout.Button("Create golf ball spawn"))
    {
        GameObject o = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        o.transform.localScale = new Vector3(10, 10, 10);
        DestroyImmediate(o.GetComponent<SphereCollider>());
        o.transform.SetParent(MAP.transform);
        o.name = "Ball position";
        o.GetComponent<Renderer>().material = material;
        ObjectDesigner odesig = o.AddComponent<ObjectDesigner>();
        odesig.type = "ballspawn";
    }
    GUILayout.Space(5);
    if (GUILayout.Button("Create hole"))
    {
        GameObject o = GameObject.CreatePrimitive(PrimitiveType.Cube);
        o.transform.localScale = new Vector3(13, 10, 13);
        DestroyImmediate(o.GetComponent<BoxCollider>());
        o.transform.SetParent(MAP.transform);
        o.GetComponent<Renderer>().material = material;
        ObjectDesigner odesig = o.AddComponent<ObjectDesigner>();
        odesig.uID = ObjectDesigner.uniqueId();
        odesig.gameObject.name = "Hole";
        odesig.type = "hole";
    }

    GUILayout.Space(5);
    if (GUILayout.Button("Create power"))
    {
        GameObject o = GameObject.CreatePrimitive(PrimitiveType.Cube);
        o.transform.localScale = new Vector3(13, 10, 13);
        DestroyImmediate(o.GetComponent<BoxCollider>());
        o.transform.SetParent(MAP.transform);
        o.GetComponent<Renderer>().material = material;
        ObjectDesigner odesig = o.AddComponent<ObjectDesigner>();
        odesig.uID = ObjectDesigner.uniqueId();
        odesig.gameObject.name = "Power " + odesig.uID;
        odesig.type = "addmass";
    }



    GUILayout.EndArea();
}

void materiales()
{
    GUILayout.BeginArea(materialesRect);

    Event evt = Event.current;

    GUILayout.Label("Material to set");
    material = (Material)EditorGUILayout.ObjectField(material, typeof(Material), true);

    GUILayout.EndArea();


}

[CustomEditor(typeof(ObjectDesigner))]
public class ObjecDisgnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ObjectDesigner ob = (ObjectDesigner)target;

        if (GUILayout.Button("Plane"))
        {
            ob.gameObject.transform.localScale = new Vector3(100, 2, 100);
        }

        if (GUILayout.Button("New uID"))
        {
            ob.uID = ObjectDesigner.uniqueId();
            ob.gameObject.name = ob.type+" - "+ ob.uID;
        }

        if (GUILayout.Button("Create walls"))
        {
            ob.createWalls();
        }
    }
}

}
#endif
*/