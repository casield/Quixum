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
    public float mass;

    private Vector3 newPosition;
    private Quaternion newQuaternion;
    public bool updateToFPS = false;
    public float refreshTime = .8f;
    private float lastTime = 0;
    public float imaginaryRT = 0;

    private bool isBox(){
        return state.GetType().Equals(typeof(BoxObject));
    }
    public void setState(ObjectState state)
    {
        this.state = state;
        state.position.OnChange += onPositionChange;
        state.quaternion.OnChange += onQuaternionChange;
        if(isBox()){
            ((BoxObject) state).halfSize.OnChange+=onBoxSizeChange;
        }
        updateInGui();
    }

    private void onBoxSizeChange(List<DataChange> changes)
    {
        BoxObject bb = (BoxObject) state;
        this.transform.localScale = new Vector3(bb.halfSize.x*2,bb.halfSize.y*2,bb.halfSize.z*2);
        print(state.type+ " / "+changes.Count);
        updateTime();
    }

    private void updateInGui()
    {
        type = state.type;
        uID = state.uID;
        mesh = state.mesh;
        material = state.material;
        mass = state.mass;
    }
    private void onQuaternionChange(List<DataChange> changes)
    {
        newQuaternion = new Quaternion(state.quaternion.x, state.quaternion.y, state.quaternion.z, state.quaternion.w);
        //updateTime();
    }

    private void onPositionChange(List<DataChange> changes)
    {
        newPosition = new Vector3(state.position.x, state.position.y, state.position.z);
        updateTime();
    }

    void updateTime(){
        
        imaginaryRT = Time.fixedTime-lastTime ;
        lastTime = Time.fixedTime;
        if(imaginaryRT != 0 && updateToFPS){
            refreshTime = imaginaryRT;
        }
        
    }

    private void FixedUpdate() {

        

        if(newPosition != null){
            if(newPosition != this.transform.position){
                this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, newPosition, refreshTime);
            }
        }

          if(newQuaternion != null){
            if(newQuaternion != this.transform.rotation){
                this.gameObject.transform.rotation = Quaternion.Lerp(this.gameObject.transform.rotation, newQuaternion,refreshTime);
            }
        }
    }

    public void onMessage(ObjectMessage message)
    {
        gameObject.GetComponent<ConnectedObject>().onMessage(message);
    }
}