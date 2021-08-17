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




    public LookObject lookObject;
    private Vector2 newPosition = new Vector3();
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
        desiredPosition.y += newPosition.y;
        transform.position = Vector3.Lerp(player.transform.position, desiredPosition,.9f);

       // var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);


        hasChanged = false;
        // 
    }

    public void SetInitialCameraPosition()
    {
        SetCameraPosition(130, 80);
       // transform.parent = player.transform;
    }



    // Update is called once per frame
    void Update()
    {

        if (player != null && !initPlayer)
        {

            SetInitialCameraPosition();

            initPlayer = true;

        }
       // if (hasChanged)
       // {
           
        //}

        if (player != null && RotationController.Instance != null)
        {
             ActualSetCameraPosition();
            var targetRotation = Quaternion.LookRotation(followObject.transform.position - transform.position);

            transform.rotation = targetRotation;//Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
        }



    }
}
