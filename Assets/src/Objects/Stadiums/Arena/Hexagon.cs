using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hexagon : ConnectedObject
{
    // Start is called before the first frame update
    TextMeshPro text;
    GameObject textObject;

    GameObject GridObject;
    static string _GRIDOBJECT_NAME = "Hexgrid";
    void Start()
    {
        var hexobject = GameObject.Find(_GRIDOBJECT_NAME);
        if (hexobject != null)
        {
            transform.parent = hexobject.transform;
            GridObject = hexobject;
        }
        else
        {
            GridObject = new GameObject(_GRIDOBJECT_NAME);
            transform.parent = GridObject.transform;
        }
    }

    void AddMessages()
    {
        textObject = new GameObject("Hexagon Text");
        text = textObject.AddComponent<TextMeshPro>();
        textObject.transform.parent = transform;
        RectTransform textTransform = (RectTransform)textObject.transform;
        text.fontSize = 1500;
        text.color = new Color(0, 0, 0, 1);
        text.alignment = TextAlignmentOptions.Center;
        textTransform.sizeDelta = new Vector2(750, 550);

        textTransform.position += new Vector3(0, 700, 0);

        textObject.transform.rotation = Quaternion.Euler(90, 0, 0);

        text.text = "Hola";
    }

    public override void onMessage(ObjectMessage m)
    {
        if (m.message.Contains("c:"))
        {
            QuixConsole.Log(m.message);
            var func = m.message.Split(':');
            ColorUtility.TryParseHtmlString(func[1], out Color color);
            gameObject.GetComponent<Renderer>().sharedMaterial.SetColor("Color",color);

        }
        base.onMessage(m);
        // text.text = m.message;

    }
}
