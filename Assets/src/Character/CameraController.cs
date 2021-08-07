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



    public float YPositionFollowObject = 30;


    public LookObject lookObject;
    private Vector2 newPosition = new Vector3();
    public Vector2 lastPosition = new Vector3();
    public bool hasChanged = false;





    void Awake()
    {

        Instance = this;
        dCamera = GetComponent<Camera>();
        transform.rotation = Quaternion.identity;
        //followObject = new GameObject("Camera Follow Object");
    }


    public void SetLookObject(LookObject lookObject)
    {
        this.lookObject = lookObject;
        followObject = lookObject.gameObject;
    }


    public void SetCameraPosition(float X, float Y)
    {
        newPosition = new Vector2(X, Y);
        hasChanged = true;
    }

    private void ActualSetCameraPosition()
    {
       
        Vector3 desiredPosition = player.transform.position - ((player.transform.right * -1) * newPosition.x);
        Vector3 lastDesiredPosition = player.transform.position - ((player.transform.right * -1) * lastPosition.x);
        desiredPosition.y += newPosition.y;
        lastDesiredPosition.y+=lastPosition.y;
        transform.position = desiredPosition;//Vector3.Slerp(desiredPosition,lastDesiredPosition,.1f);
      
       var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        lastPosition = new Vector2(newPosition.x,newPosition.y);


        //transform.LookAt(Vector3.Lerp(followObject.transform.position,player.transform.position,.5f),);

        hasChanged = false;
        // 
    }

    public void SetInitialCameraPosition()
    {


        SetCameraPosition(130, 80);
          transform.parent = player.transform;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null && !initPlayer)
        {

            SetInitialCameraPosition();

            initPlayer = true;

        }

        if (player != null && RotationController.Instance != null)
        {
            var targetRotation = Quaternion.LookRotation(followObject.transform.position - transform.position);
            //transform.LookAt(Vector3.Lerp(followObject.transform.position,player.transform.position,.5f),);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
        }
        if (hasChanged)
        {
            ActualSetCameraPosition();
        }

    }
}
