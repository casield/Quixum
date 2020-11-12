using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRot_Debug : MonoBehaviour
{
    private float xRot = 0;
    private float yRot = 0;
    private float rotationSpeed = .1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void rotateView(float axis, float axisY, bool rotateY, GameObject lookAt)
    {

        xRot += rotationSpeed * axis * 2;
        yRot += rotationSpeed * axisY * 2;
        // GameObject ball = client.golfballs[client.room.SessionId].gameObject;
        if (rotateY)
        {
            transform.rotation = Quaternion.Euler(-yRot, xRot, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, xRot, 0);
        }

        Quaternion qu = Quaternion.Euler(transform.rotation.eulerAngles.x, +transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z);
        Quat quat = new Quat();
        quat.x = qu.x;
        quat.y = qu.y;
        quat.z = qu.z;
        quat.w = qu.w;

        //await client.room.Send("rotate", quat);
        transform.LookAt(gameObject.transform);


        //transform.LookAt(gameObject.transform);


    }

    // Update is called once per frame
    void Update()
    {
        rotateView(-1,0,false,null);
    }
}
