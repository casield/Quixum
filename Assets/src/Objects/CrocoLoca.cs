using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocoLoca : MonoBehaviour, IConnectedObject
{
    public void onMessage(ObjectMessage m)
    {
        Debug.Log("Message to crocodile");
        if(m.message == "text_Eat"){
            BubblesController.Instance.createBubble("I'm gonna eat You!\nAre you ready?",gameObject,100);
        }
    }

    public void sendMessageToRoom(string m)
    {
       // throw new System.NotImplementedException();
    }

    public void setState(ObjectState state)
    {
      
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
