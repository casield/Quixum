using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static UnityEngine.InputSystem.InputAction;

public class TrowMode : MonoBehaviour
{
    public static TrowMode Instance;
    public ObjectState objectState;
    private float xRot;
    private float yRot;
    private float rotationSpeed = 0.1f;
    public Client client;
    private Vector2 savedAxis = new Vector2();

    public Camera MainCamera;
    GameObject dragObject;
    BoxObject box;
    public Profile profileWindow;
    private int zoomVelocity = 2;

    void Awake()
    {
        Instance = this;
        client.addReadyListener(init);

        objectState = new BoxObject();
        box = (BoxObject)objectState;
        int size = 15;
        box.halfSize.x = size;
        box.halfSize.y = size;
        box.halfSize.z = size;
        box.material = "normalMaterial";
        box.type = "trownobj";

    }

    void OnEnable()
    {
        GameObject ball = Character.Instance.giveBall();
        this.transform.position = new Vector3(0, ball.transform.position.y + 150, 0);
        this.transform.rotation = Quaternion.Euler(80, 0, 0);
        if (dragObject != null)
        {
            dragObject.SetActive(true);
        }

        if (profileWindow.gameObject.activeSelf)
        {
            profileWindow.closeButton.clickButton();
        }

    }
    void OnDisable()
    {
        if (dragObject != null)
        {
            dragObject.SetActive(false);
        }

    }

    public async void onClick(CallbackContext ctx)
    {

        if (ctx.performed && this.enabled)
        {
            Debug.Log("Throw!");
            box.position.x = dragObject.transform.position.x;
            box.position.y = dragObject.transform.position.y;
            box.position.z = dragObject.transform.position.z;

            box.quaternion.x = dragObject.transform.rotation.x;
            box.quaternion.y = dragObject.transform.rotation.y;
            box.quaternion.z = dragObject.transform.rotation.z;
            box.quaternion.w = dragObject.transform.rotation.w;
            await client.room.Send("trowBox", box);
        }

    }

    public void onScroll(CallbackContext ctx)
    {
        if(this.enabled){
           if (ctx.ReadValue<float>() == 120)
        {
            zoomView(true);
        }
        if (ctx.ReadValue<float>() == -120)
        {
            zoomView(false);
        } 
        }
        
    }

    public void onPosition(InputAction.CallbackContext obj)
    {
        if (this.enabled)
        {
            
            Vector2 pos = obj.ReadValue<Vector2>();
            Debug.Log(pos);
            setDragObjectPosition(pos);
        }


    }

    void setDragObjectPosition(Vector2 pos)
    {
        if (dragObject != null)
        {

            dragObject.transform.position = MainCamera.ScreenToWorldPoint(new Vector3(pos.x, pos.y, MainCamera.nearClipPlane +60));
            dragObject.transform.LookAt(MainCamera.transform);


        }
    }

    void init()
    {
        dragObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        dragObject.transform.localScale = new Vector3(box.halfSize.x, box.halfSize.y, box.halfSize.z);
        dragObject.GetComponent<MeshRenderer>().material= Client.Instance.WallMaterial;
        // dragObject.transform.parent = transform;
        dragObject.name = "Drag Object";
    }

    public void onDrag(InputAction.CallbackContext obj)
    {
        if (this.enabled)
        {
            if (Character.Instance.inputControl.Normal.Click.ReadValue<float>() == 1)
            {
                Vector2 delta = obj.ReadValue<Vector2>();

                panView(delta.x, delta.y, true);
            }
        }


    }

    void panView(float axis, float axisY, bool rotateY)
    {

        xRot = rotationSpeed * axis;
        yRot = rotationSpeed * axisY;
        transform.position = new Vector3(transform.position.x - xRot, transform.position.y, transform.position.z - yRot);
    }

    void zoomView(bool add)
    {
        int addNum = add ? this.zoomVelocity : -this.zoomVelocity;
        transform.position = new Vector3(transform.position.x, transform.position.y + addNum, transform.position.z);
        dragObject.transform.position = new Vector3(dragObject.transform.position.x, dragObject.transform.position.y + addNum, dragObject.transform.position.z);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        
    }

    // Update is called once per fram
}
