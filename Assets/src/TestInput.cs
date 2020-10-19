using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class TestInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPosition(CallbackContext ctx){
       // Debug.Log(ctx.ReadValue<Vector2>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
