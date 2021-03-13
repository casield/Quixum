using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuixBox : MonoBehaviour, ConnectedObject
{
    public void onMessage(ObjectMessage m)
    {
        //throw new System.NotImplementedException();
    }

    public void sendMessageToRoom(string m)
    {
        //throw new System.NotImplementedException();
    }

    public void setState(ObjectState state)
    {
        //throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        var rend = this.GetComponent<MeshRenderer>();
        if (rend != null)
        {
            this.GetComponent<MeshRenderer>().material = Character.Instance.defaultMaterial;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
