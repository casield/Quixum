using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDesigner : MonoBehaviour
{
    public int tileToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public ObjectState toJson()
    {
        ObjectState ob = new ObjectState();

        ob.quaternion.x = gameObject.transform.rotation.x;
        ob.quaternion.y = gameObject.transform.rotation.y;
        ob.quaternion.z = gameObject.transform.rotation.z;
        ob.quaternion.w = gameObject.transform.rotation.w;

        ob.position.x = gameObject.transform.position.x;
        ob.position.y = gameObject.transform.position.y;
        ob.position.z = gameObject.transform.position.z;

        ob.type = ""+tileToLoad;

        return ob;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
