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



    void Awake()
    {

        Instance = this;
        dCamera = GetComponent<Camera>();
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null && !initPlayer)
        {

             Vector3 desiredPosition = player.transform.position - ((player.transform.right*-1) * 200);
             desiredPosition.y +=100;
            transform.position = desiredPosition;
            transform.parent = player.transform;
            transform.LookAt(player.transform);
            
            initPlayer = true;
            
        }
    }
}
