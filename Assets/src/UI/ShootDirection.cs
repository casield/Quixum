using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDirection : MonoBehaviour
{
    private float zCoord;
    private Vector3 offset;

    public GameObject sphere;
    public Camera mCamera;
    private Vector3 mPointOfContact;

    Vector3 vecToScreen = Vector3.zero;
    RaycastHit hitP;

    public static ShootDirection Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*Character.Instance.inputControl.Normal.Click.started += _ =>
        {
            Vector2 inputPosition = Character.Instance.inputControl.Normal.Position.ReadValue<Vector3>();
            vecToScreen = new Vector3(inputPosition.x, inputPosition.y, 0);
            var camRay = mCamera.ScreenPointToRay(vecToScreen);

            if (Physics.Raycast(camRay, out hitP))
            {
                if (hitP.collider.gameObject.layer == 10)
                {
                    Debug.Log("hit " + hitP.transform.name);
                    MouseDown(hitP.point);

                    mPointOfContact = sphere.transform.InverseTransformPoint(hitP.point);
                }
            }


        };*/
        Character.Instance.inputControl.Normal.Click.canceled += _ =>
        {
            MouseUp();
        };
    }
    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void MouseDown(Vector3 point)
    {
        Character.Instance.canRotate = false;
        transform.position = point;
        align();
    }

    void MouseUp()
    {
        Character.Instance.canRotate = true;
    }
    public void reset()
    {
        transform.localPosition = new Vector3(0, 0, -.6f);
    }

    void align()
    {
        var range = 0.15;
        //if (x >= 1 && x <= 100)
        if (transform.localPosition.x >= -range && transform.localPosition.x <= range)
        {
            // Character.Instance.pointOfContact.x = 0;
            transform.localPosition = new Vector3(0, mPointOfContact.y, mPointOfContact.z - .05f);
            Character.Instance.pointOfContact = transform.localPosition;
            Character.Instance.pointOfContact.x = 0;
        }
        else
        {
            Character.Instance.pointOfContact = transform.localPosition;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Client.Instance.room != null)
        {
            if (Character.Instance.isDragging)
            {

              /*  if (Character.Instance.giveBall() != null)
                {
                    sphere.transform.rotation = Character.Instance.giveBall().transform.rotation;
                }*/


            }
        }

    }
}
