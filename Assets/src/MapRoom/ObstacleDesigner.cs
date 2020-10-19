using System.Collections;
using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;

public class ObstacleDesigner : MonoBehaviour
{
    // Start is called before the first frame updatepublic int tileToLoad;
    // Start is called before the first frame update
    public string modelToLoad;

    public List<GameObject> extraPoints = new List<GameObject>();
    void Start()
    {
        
    }

    public Dictionary<int,V3> giveExtraPoints(){
        Dictionary<int,V3> points = new Dictionary<int,V3>();

        for(int a=0;a<extraPoints.Count;a++){
            points[a] = new V3();
            points[a].x = extraPoints[a].transform.position.x;
            points[a].y = extraPoints[a].transform.position.y;
            points[a].z = extraPoints[a].transform.position.z;
        }
        return points;
    }

    public ObstacleState toJson()
    {
        ObstacleState ob = new ObstacleState();

        ob.quaternion.x = gameObject.transform.rotation.x;
        ob.quaternion.y = gameObject.transform.rotation.y;
        ob.quaternion.z = gameObject.transform.rotation.z;
        ob.quaternion.w = gameObject.transform.rotation.w;

        ob.position.x = gameObject.transform.position.x;
        ob.position.y = gameObject.transform.position.y;
        ob.position.z = gameObject.transform.position.z;

        ob.objectname = ""+modelToLoad;
        ob.uID = ObjectDesigner.uniqueId();
        

        ob.extraPoints = new ArraySchema<V3>(giveExtraPoints());

        return ob;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
