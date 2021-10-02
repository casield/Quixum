using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct HelperVector3
{
    public float X;
    public float Y
    ;
    public float Z;
}

[Serializable]
public class HelperMessage
{
    public List<HelperVector3> positions;
}
public class Helper : MonoBehaviour, IConnectedObject
{
    List<HelperVector3> trailPositions = new List<HelperVector3>() { new HelperVector3() };
    public void onMessage(ObjectMessage m)
    {

        var pos = JsonUtility.FromJson<HelperMessage>(m.message);
        QuixConsole.Log("Helper", pos.positions.Count);
        trailPositions = pos.positions;
        //  trailPositions = pos;


    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < trailPositions.Count; i++)
        {
            var item = trailPositions[i];

            if(i > 0){
               Gizmos.color = Color.red;  
            }else{
                Gizmos.color = Color.blue;
            }
           
            Gizmos.DrawSphere(new Vector3(item.X, item.Y, item.Z), 50);
        }
    }

    public void sendMessageToRoom(string m)
    {

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
