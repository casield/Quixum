using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Player2 player;



    public static CameraController Instance { get; private set; }


    private bool initPlayer = false;

    public Camera dCamera;

    private GameObject followObject;

    private float moveYVelocity = 4;
    private float maxYPosition = 30;

    private Vector3 newPosition = new Vector3();

    public float YPositionFollowObject = 30;


    public LookObject lookObject;





    void Awake()
    {

        Instance = this;
        dCamera = GetComponent<Camera>();
        //followObject = new GameObject("Camera Follow Object");
    }

    public void SetFollowObjectToPlayer()
    {
     /*   if (player != null)
        {
            newPosition = player.transform.position;
            newPosition.y+=50;
        }*/

        //followObject.transform.position = Vector3.zero;
    }

    public void SetLookObject(LookObject lookObject){
        this.lookObject = lookObject;
        followObject = lookObject.gameObject;
    }

    public void MoveCameraY(float Y)
    {
        /*if (Math.Abs(Y) > 0.5)
        {
            if(Math.Abs(followObject.transform.localPosition.y) <= maxYPosition){
                newPosition = followObject.transform.position + new Vector3(0, Y * moveYVelocity, 0);
            }else{
                followObject.transform.localPosition = new Vector3(0,maxYPosition-.01f,0);
            }
            
        }*/
        //Debug.Log(Y);


    }



    // Update is called once per frame
    void Update()
    {
        if (player != null && !initPlayer)
        {

            Vector3 desiredPosition = player.transform.position - ((player.transform.right * -1) * 200);
            desiredPosition.y += 100;
            transform.position = desiredPosition;
            transform.parent = player.transform;
            transform.LookAt(player.transform);
            //followObject.transform.parent = player.gameObject.transform;
            //SetFollowObjectToPlayer();
            initPlayer = true;

        }
        if (player != null && RotationController.Instance!=null)
        {
            

            newPosition.x = player.transform.position.x;
            newPosition.z = player.transform.position.z;
            
            //if(RotationController.Instance.rotateMessage.y == 0){
            //    newPosition.y = player.transform.position.y;
           // }
            newPosition.y = player.transform.position.y+YPositionFollowObject;
           // followObject.transform.position = Vector3.Lerp(followObject.transform.position, newPosition, player.sObject.refreshTime);

            transform.LookAt(followObject.transform);
        }

    }
}
