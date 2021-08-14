using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

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
    public bool isMesh = false;

    public BoundingBox boundingBox;
    void Start()
    {

    }
    public static string uniqueId()
    {
        // desired length of Id
        // always start with a letter -- base 36 makes for a nice shortcut
        Random rand = new Random();
        var idStr = Convert.ToString((System.Math.Floor(((float)(UnityEngine.Random.Range(0, 1000000)) * 25)) + 10), null);
        // add a timestamp in milliseconds (base 36 again) as the base
        idStr += "_" + Convert.ToString(DateTime.Now.Millisecond, null);
        // similar to above, complete the Id using random, alphanumeric characters

        return (idStr);
        //return "";
    }
    public ObjectState toJson()
    {
        uID = this.uID != "" ? this.uID : uniqueId();
        ObjectState p = null;
        if (shape == "box")
        {
            p = new BoxObject();
            BoxObject bo = (BoxObject)p;
            bo.halfSize = new V3();
            if (boundingBox != null)
            {
                bo.halfSize.x = boundingBox.mCollider.bounds.extents.x*2;
                bo.halfSize.y = boundingBox.mCollider.bounds.extents.y*2;
                bo.halfSize.z = boundingBox.mCollider.bounds.extents.z*2;
            }
            else
            {
                if (isMesh)
                {
                   bo.halfSize.x = gameObject.transform.localScale.x / 2;
                    bo.halfSize.y = gameObject.transform.localScale.y / 2;
                    bo.halfSize.z = gameObject.transform.localScale.z / 2;

                  /*  bo.halfSize.x = gameObject.transform.localScale.x;
                    bo.halfSize.y = gameObject.transform.localScale.y;
                    bo.halfSize.z = gameObject.transform.localScale.z;*/
                }
                else
                {
                    bo.halfSize.x = gameObject.transform.localScale.x;
                    bo.halfSize.y = gameObject.transform.localScale.y;
                    bo.halfSize.z = gameObject.transform.localScale.z;
                }
            }





        }
        if (shape == "sphere")
        {

            p = new SphereObject();
            SphereObject bo = (SphereObject)p;
            bo.radius = gameObject.transform.localScale.x / 2;


        }
        p.quaternion.x = gameObject.transform.rotation.x;
        p.quaternion.y = gameObject.transform.rotation.y;
        p.quaternion.z = gameObject.transform.rotation.z;
        p.quaternion.w = gameObject.transform.rotation.w;

        //if (boundingBox == null)
        //{
            p.position.x = gameObject.transform.position.x;
            p.position.y = gameObject.transform.position.y;
            p.position.z = gameObject.transform.position.z;
       /* }else{
            p.position.x =  boundingBox.mCollider.bounds.center.x-boundingBox.mCollider.bounds.extents.x;
            p.position.y =  boundingBox.mCollider.bounds.center.y-boundingBox.mCollider.bounds.extents.x;
            p.position.z =  boundingBox.mCollider.bounds.center.z-boundingBox.mCollider.bounds.extents.x;

            
        }*/


        p.type = type;
        p.uID = uID;
        p.material = material;
        p.instantiate = instantiate;
        p.mass = mass;
        p.mesh = mesh;
        p.isMesh = isMesh;

        return p;

    }

    // Update is called once per frame
    void Update()
    {

    }
}

