using UnityEngine;

public class GUIConsole : MonoBehaviour
{
    string myLog = "*begin log";
    string filename = "";
    bool doShow = false;
    int kChars = 700;

    private float lastTime = 0;
    public float deltaTime = 0;
    public static GUIConsole Instance { get; private set; }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>

    void OnEnable() { Application.logMessageReceived += Log; }
    void OnDisable() { Application.logMessageReceived -= Log; }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) { doShow = !doShow; }
       
    }
    public void Log(string logString, string stackTrace, LogType type)
    {
        // for onscreen...
        if (logString != "ROOM_STATE_PATCH")
        {
            myLog = myLog + "\n" + logString;
            if (myLog.Length > kChars) { myLog = myLog.Substring(myLog.Length - kChars); }
        }
    }

    void OnGUI()
    {
        if (!doShow) { return; }
        /* GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity,
            new Vector3(Screen.width / 1200.0f, Screen.height / 800.0f, 1.0f));*/
        GUI.TextArea(new Rect(10, 10, 540, 370), myLog);
        GUI.TextArea(new Rect(560, 10, 200, 50), "Server Objects: " + Client.Instance.ServerObjects.transform.childCount);
        GUI.TextArea(new Rect(560, 60, 200, 50), "ping: " + (deltaTime) + "ms");
        // lastTime = Client.Instance.time;
    }
}