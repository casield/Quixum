

#if UNITY_EDITOR
using UnityEngine;

using UnityEditor;
using UnityEngine.SceneManagement;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;


public class Model_Map
{
    public ObjectId _id { set; get; }

    public Model_Object[] objects { set; get; }
    public static string name { set; get; }
    public Object ballspawn { set; get; }
}

public class Model_Object
{
    public Vector3 position { set; get; }
    public Quaternion quat { set; get; }

    public string uID { set; get; }
    public Vector3 halfSize { set; get; }
    public float radius { set; get; }
    public Vector3[] verts { set; get; }
    public int[] faces { set; get; }
    public string material { set; get; }


}

public class LevelDesignerWindow : EditorWindow
{
    Rect materialesRect = new Rect();

    public string MapName = "";
    Material material = null;





    [MenuItem("drokt.com/Level Designer")]
    static void OpenWindow()
    {

        LevelDesignerWindow window = (LevelDesignerWindow)GetWindow(typeof(LevelDesignerWindow));
        window.Show();
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
            botones();
            materiales();
        }


    }
    void botones()
    {
        Rect rect = EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        drawCreateSpiral();
        EditorGUILayout.EndVertical();
        //This box will cover all controls between the former BeginVertical() & EndVertical()
    }

    Transform pivotTransform;
    int planes = 6;
    int planeWidth = 5;
    float planePadding = 5;
    float planeAncho = 1;
    int radius = 3;
    float separation = 1;

    float paredAngle = 35;
    float paredAltura = 5;



    bool reDraw = false;
    GameObject spiralContainer;
    List<SpiralLabel> spiralsLabels = new List<SpiralLabel>();
    class SpiralLabel
    {
        public GameObject piso;
        public GameObject pared1;
        public SpiralLabel(GameObject piso, GameObject pared1)
        {
            this.piso = piso;
            this.pared1 = pared1;
        }
    }
    private void drawCreateSpiral()
    {
        GUILayout.Label("Spiral designer");
        EditorGUI.BeginChangeCheck();
        planes = EditorGUILayout.IntField("Numero de planos", planes);

        planeWidth = EditorGUILayout.IntField("Plane Width", planeWidth);
        planePadding = EditorGUILayout.FloatField("Plane padding", planePadding);
        planeAncho = EditorGUILayout.FloatField("Anchura plano", planeAncho);


        GUILayout.Space(5);

        paredAngle = EditorGUILayout.FloatField("Angulo de las paredes", paredAngle);
        paredAltura = EditorGUILayout.FloatField("Alto de las paredes", paredAltura);
        
        GUILayout.Space(10);

        radius = EditorGUILayout.IntField("Radius", radius);
        separation = EditorGUILayout.FloatField("Separacion", separation);
        if (EditorGUI.EndChangeCheck())
        {
            reDraw = true;
        }
        GUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Spiral container");
        spiralContainer = (GameObject)EditorGUILayout.ObjectField(spiralContainer, typeof(GameObject), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Pivot");
        pivotTransform = (Transform)EditorGUILayout.ObjectField(pivotTransform, typeof(Transform), true);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Crear espiral") || reDraw)
        {
            reDraw = false;
            DestroyImmediate(spiralContainer);

            if (spiralContainer == null)
            {
                spiralContainer = new GameObject("Spiral container");
            }

            float grados = 360 / planes;
            for (int a = 0; a <= planes; a++)
            {
                // spiralsLabels.

                GameObject piso;

                piso = GameObject.CreatePrimitive(PrimitiveType.Cube);

                piso.transform.localScale = new Vector3(planeAncho, radius, planeWidth + planePadding);
                piso.transform.parent = spiralContainer.transform;
                Vector3 rot = piso.transform.rotation.eulerAngles;

                float angleInDegrees = (grados * a) - 90;
                float pi = Mathf.PI / 180;
                float x = radius * Mathf.Cos(angleInDegrees * pi);
                float y = radius * Mathf.Sin(angleInDegrees * pi);

                float angleInDegrees2 = ((grados * (a + 1)) - 90);

                rot.z += angleInDegrees;
                piso.transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
                // piso.SetActive(false);


                piso.transform.position = new Vector3(x, y, (separation * a));
                piso.transform.Translate(new Vector3(pivotTransform.position.x + radius / 2, (pivotTransform.position.y + radius), pivotTransform.position.z), Space.World);

                GameObject pared;

                pared = GameObject.CreatePrimitive(PrimitiveType.Cube);

                pared.name = "pared";
                pared.transform.localScale = new Vector3(paredAltura, radius, planeAncho);
                pared.transform.parent = spiralContainer.transform;
                float newR = radius - 5f;
                float xx = newR * Mathf.Cos(angleInDegrees * pi);
                float yy = newR * Mathf.Sin(angleInDegrees * pi);
                float paddingZ = (planeWidth / 2);
                pared.transform.position = new Vector3(xx, yy, ((separation * a) - (paddingZ)));
                pared.transform.Rotate(rot.x, rot.y, rot.z);
                pared.transform.Rotate(paredAngle, 0, 0, Space.Self);

                pared.transform.Translate(new Vector3(pivotTransform.position.x + radius / 2, (pivotTransform.position.y + radius), pivotTransform.position.z), Space.World);

                GameObject pared2;

                pared2 = GameObject.CreatePrimitive(PrimitiveType.Cube);

                pared2.name = "pared 2";
                pared2.transform.localScale = new Vector3(paredAltura, radius, planeAncho);
                pared2.transform.parent = spiralContainer.transform;
                pared2.transform.position = new Vector3(xx, yy, ((separation * a) + (paddingZ)));

                pared2.transform.Rotate(rot.x, rot.y, rot.z);
                pared2.transform.Rotate(paredAngle, 0, 0, Space.Self);
                pared2.transform.Translate(new Vector3(pivotTransform.position.x + radius / 2, (pivotTransform.position.y + radius), pivotTransform.position.z), Space.World);
            }
        }
    }

    void materiales()
    {
        GUILayout.BeginArea(materialesRect);

        Event evt = Event.current;

        GUILayout.Label("Material to set");
        material = (Material)EditorGUILayout.ObjectField(material, typeof(Material), true);

        GUILayout.EndArea();


    }


}
#endif
