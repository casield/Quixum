using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;

    [NonSerialized]
    public GameObject cameraHelper;
    public Vector3 padding = new Vector3(0, 50, 0);

    [Range(0.0f, 1f)]
    public float smoothSpeed = 0f;

    public static CameraController Instance { get; private set; }
    public InputControl control;
    private bool isDragging = false;
    private bool initPlayer = false;

    public Camera dCamera;



    void Awake()
    {
        control = new InputControl();
        Instance = this;
        dCamera = GetComponent<Camera>();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        control.Enable();
        // control.Normal.Drag.performed += onDrag;
    }
    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        control.Disable();
    }

    void Start()
    {
        // Client.Instance.addReadyListener(init);
    }

    private void init()
    {
        Debug.Log("Init");
    }


    // Update is called once per frame
    void LateUpdate()
    {
        if (player != null && !initPlayer)
        {

             Vector3 desiredPosition = player.transform.position + padding;
            transform.position = desiredPosition;
            transform.LookAt(player.transform);
            transform.parent = player.transform;
            initPlayer = true;
            
        }
    }
}
