using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDesigner : MonoBehaviour
{
    // Start is called before the first frame update
    public string type = "QuixBox";
    public string uID;
    public string material = "normalMaterial";
    public bool instantiate = false;
    public string shape = "box";
    public float mass = 0;
    public string mesh;
    void Start()
    {

    }
    public static string uniqueId()
    {
        // desired length of Id
        // always start with a letter -- base 36 makes for a nice shortcut
        UnityEngine.Random rand = new UnityEngine.Random();
        var idStr = Convert.ToString((System.Math.Floor(((float)(UnityEngine.Random.Range(0, 1000000)) * 25)) + 10), null);
        // add a timestamp in milliseconds (base 36 again) as the base
        idStr += "_" + Convert.ToString(DateTime.Now.Millisecond, null);
        // similar to above, complete the Id using random, alphanumeric characters

        return (idStr);
    }
    public ObjectState toJson()
    {
        uID = this.uID!=""?this.uID:uniqueId();
        ObjectState p = new ObjectState();
        if (shape == "box")
        {

            p = new BoxObject();
            BoxObject bo = (BoxObject)p;
            bo.halfSize = new V3();
            bo.halfSize.x = gameObject.transform.localScale.x ;
            bo.halfSize.y = gameObject.transform.localScale.y ;
            bo.halfSize.z = gameObject.transform.localScale.z ;


        }
         if (shape == "sphere")
        {

            p = new SphereObject();
            SphereObject bo = (SphereObject)p;
            bo.radius = gameObject.transform.localScale.x/2;


        }
        if (type == "ballspawn")
        {
            p = new SphereObject();
            SphereObject bo = (SphereObject)p;
            bo.radius = 5;
        }

        p.quaternion.x = gameObject.transform.rotation.x;
        p.quaternion.y = gameObject.transform.rotation.y;
        p.quaternion.z = gameObject.transform.rotation.z;
        p.quaternion.w = gameObject.transform.rotation.w;

        p.position.x = gameObject.transform.position.x;
        p.position.y = gameObject.transform.position.y;
        p.position.z = gameObject.transform.position.z;
        p.type = type;
        p.uID = uID;
        p.material = material;
        p.instantiate = instantiate;
        p.mass = mass;
        p.mesh = mesh;

        return p;

    }

    // Update is called once per frame
    void Update()
    {

    }
}

