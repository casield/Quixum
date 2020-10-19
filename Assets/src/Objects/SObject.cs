using UnityEngine;
public class SObject{
    public GameObject gameObject;
    public ObjectState state;

    public SObject(GameObject o,ObjectState s){
        gameObject = o;
        state = s;
    }
}