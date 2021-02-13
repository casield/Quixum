using System;
using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;
public class SObject : MonoBehaviour
{
    public ObjectState state;
    
    public string uID;
    public string type;
    public string mesh;
    public string material;
    


    public void setState(ObjectState state)
    {
        this.state = state;
        state.position.OnChange += onPositionChange;
        state.quaternion.OnChange += onQuaternionChange;
        updateInGui();
    }

    private void updateInGui()
    {
        type = state.type;
        uID = state.uID;
        mesh = state.mesh;
        material = state.material;
    }
    private void onQuaternionChange(List<DataChange> changes)
    {
        this.gameObject.transform.rotation = new Quaternion(state.quaternion.x, state.quaternion.y, state.quaternion.z, state.quaternion.w);
    }

    private void onPositionChange(List<DataChange> changes)
    {
        this.gameObject.transform.position = new Vector3(state.position.x, state.position.y, state.position.z);
    }

    public void onMessage(ObjectMessage message)
    {
        gameObject.GetComponent<ConnectedObject>().onMessage(message);
    }
}