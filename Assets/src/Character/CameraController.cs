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
        Vector3 desiredPosition = player.transform.position - ((player.transform.right * -1) * X);
        desiredPosition.y += Y;
        transform.position = desiredPosition;
        transform.parent = player.transform;
        transform.LookAt(player.transform);
    }

    public void SetInitialCameraPosition(){
        SetCameraPosition(130,80);
    }



    // Update is called once per frame
    void Update()
    {
        if (player != null && !initPlayer)
        {

            SetInitialCameraPosition();

            initPlayer = true;

        }
        if (player != null && RotationController.Instance != null)
        {
            transform.LookAt(Vector3.Lerp(followObject.transform.position,player.transform.position,.5f));
        }

    }
}
