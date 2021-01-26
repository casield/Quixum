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
    public Vector3 padding = new Vector3(0, 20, 0);

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
        control.Normal.Drag.performed += onDrag;
    }

    private void onDrag(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (control.Normal.Click.phase == UnityEngine.InputSystem.InputActionPhase.Started)
        {
            Vector2 v = (obj.ReadValue<Vector2>());
            isDragging = true;
            rotateView(v);

        }
        else
        {
            isDragging = false;
        }

    }
    

    async private void rotateView(Vector2 v)
    {
        float rotationVelocity = .05f;
        float rotation = rotationVelocity * v.x;
        float rotationY = (rotationVelocity/6) * v.y;
         cameraHelper.transform.Rotate(new Vector3(0, rotation,0),Space.World); 
        //transform.Rotate(new Vector3(0, rotation,0));
        
        if (Client.Instance != null && !uiblocker.BlockedByUI)
        {
            Quaternion qu = Quaternion.Euler(cameraHelper.transform.rotation.eulerAngles.x, cameraHelper.transform.rotation.eulerAngles.y + 90, cameraHelper.transform.rotation.eulerAngles.z);
            Quat quat = new Quat();
            quat.x = qu.x;
            quat.y = qu.y;
            quat.z = qu.z;
            quat.w = qu.w;
            V3 euler = new V3();
             euler.x = qu.eulerAngles.x;
            euler.y = qu.eulerAngles.y;
            euler.z = qu.eulerAngles.z;

            EulerQuat euq = new EulerQuat();
            euq.euler = euler;
            euq.quat = quat;
            await Client.Instance.room.Send("rotate", euq);
        }

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
        cameraHelper = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cameraHelper.name = "Camera helper";
        // Client.Instance.addReadyListener(init);
    }

    private void init()
    {
        Debug.Log("Init");
        rotateView(new Vector2(1,1));
        // Debug.Log(o);
    }


    // Update is called once per frame
    void LateUpdate()
    {
        if (player != null && !initPlayer)
        {
            
            Vector3 desiredPosition = player.transform.position + padding;
            Vector3 smoothPosition = Vector3.Slerp(transform.position, desiredPosition, smoothSpeed);

            transform.position = desiredPosition;
            // Vector3 v= transform.localPosition+padding;
             transform.LookAt(player.transform);
             this.transform.parent = this.player.transform;
            initPlayer = true;
        }
        if (player != null)
        {
            cameraHelper.transform.position = player.transform.position;
        }
    }
}
