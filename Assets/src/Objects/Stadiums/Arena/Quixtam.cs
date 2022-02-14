using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Quixtam : ConnectedObject
{
    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        Character.Instance.inputControl.Normal.DoubleClick.performed += onClick;
        transform.gameObject.AddComponent<MeshCollider>();
    }

    private void onClick(InputAction.CallbackContext obj)
    {
        Ray ray = Camera.main.ScreenPointToRay(Character.Instance.inputControl.Normal.Position.ReadValue<Vector2>());
        RaycastHit hit;
        QuixConsole.Log("Hit quixtam");
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == this.transform)
            {
                sendMessageToRoom("hit");
                QuixConsole.Log("Hit quixtam");
            }
        }
    }

    public override void onMessage(ObjectMessage m)
    {
        base.onMessage(m);
        Debug.Log("Object message en LookTarget " + m.message);
    }


}
