using UnityEngine;
using UnityEngine.InputSystem;

public class ClickObject : ConnectedObject
{
    private void Start() {
      //  Character.Instance.inputControl.Normal.DoubleClick.performed +=onClick;
        transform.gameObject.AddComponent<MeshCollider>();
    }
}