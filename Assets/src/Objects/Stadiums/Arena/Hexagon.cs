using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hexagon : ConnectedObject
{
    // Start is called before the first frame update
    TextMeshPro text;
    GameObject textObject;
    void Start()
    {
        textObject = new GameObject("Hexagon Text");
       text= textObject.AddComponent<TextMeshPro>();
        textObject.transform.parent = transform;
        RectTransform textTransform = (RectTransform)textObject.transform;
        text.fontSize=1500;
        text.color=new Color(0,0,0,1);
        text.alignment = TextAlignmentOptions.Center;
        textTransform.sizeDelta = new Vector2(750,550);

        textTransform.position+=new Vector3(0,700,0);
        
        textObject.transform.rotation = Quaternion.Euler(90,0,0);
        
        text.text="Hola";
    }

    public override void onMessage(ObjectMessage m)
    {
        base.onMessage(m);
        text.text = m.message;

    }

    // Update is called once per frame
    void Update()
    {

       // QuixConsole.Log("Position",transform.position);
        //textObject.transform.position =textObject.transform.position + new Vector3(0,1500,0);
    }
}
