using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LookObject : ConnectedObject
{
    private SObject sObject;
    private Player2 player;

    public Vector2 OnLookingPosition = new Vector2(150, 47);
    public float maxDistance = 1300;

    public bool isOnPlayer = true;

    private void Start()
    {
        sObject = GetComponent<SObject>();
        sObject.refreshTime = .7f;


        CameraController.Instance.SetLookObject(this);

    }

    public override void onMessage(ObjectMessage m)
    {
        Debug.Log("Message to lookObject");
        if (m.message == "player")
        {
           // sObject.refreshTime = 1;
            CameraController.Instance.SetInitialCameraPosition();
            isOnPlayer = true;
        }
        if(m.message == "target")
        {
           // sObject.refreshTime = .7f;
            //CameraController.Instance.SetInitialCameraPosition();
            isOnPlayer=false;
            //SetCamera();
        }

    }
    private void FixedUpdate()
    {
        if (!isOnPlayer)
            SetCamera();
    }

    public void SetCamera()
    {   
       // CameraController.Instance.SetCameraPosition(porcenage*OnLookingPosition.x, (OnLookingPosition.y/distanceWithObject)*100);
        CameraController.Instance.SetCameraPosition(OnLookingPosition.x, OnLookingPosition.y);
    }

    public override void setState(ObjectState state)
    {
        base.setState(state);
        player = Client.Instance.objects[state.owner].GetComponent<Player2>();
    }
}

[CustomEditor(typeof(LookObject))]
public class LevelScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LookObject myTarget = (LookObject)target;
        if (GUILayout.Button("Build Camera"))
        {
            myTarget.SetCamera();
        }


    }
}