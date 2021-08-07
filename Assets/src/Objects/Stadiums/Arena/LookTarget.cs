using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class LookTarget : ConnectedObject
{

    private void Start()
    {
        Init();
    }
    public void Init(){
        Character.Instance.inputControl.Normal.DoubleClick.performed +=onClick;
    }

    private void onClick(InputAction.CallbackContext obj)
    {
        Ray ray = Camera.main.ScreenPointToRay(Character.Instance.inputControl.Normal.Position.ReadValue<Vector2>());  
      RaycastHit hit;  

      if (Physics.Raycast(ray, out hit)) {
          if(hit.transform == this.transform){
              sendMessageToRoom("hit");
                   Debug.Log("Click");
          }
      }
    }

    public override void onMessage(ObjectMessage m)
    {
        base.onMessage(m);
        Debug.Log("Object message en LookTarget " + m.message);
    }




}