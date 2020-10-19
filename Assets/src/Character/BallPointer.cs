using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class BallPointer : MonoBehaviour
{

    public MeshRenderer meshRenderer;

    private int frameCount = 8;
    private int frameIndex = 0;

    private float force = .08f;

    private float frame = 0;
    InputControl inputControl;
    private bool isHolding = false;

    // Start is called before the first frame update
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>

    void Start()
    {
        Debug.Log(Screen.height);

        meshRenderer = GetComponent<MeshRenderer>();
        inputControl = Character.Instance.inputControl;

    }

    bool isTouch()
    {
        return Client.Instance.playerInput.currentControlScheme.ToLower() == "touch";
    }

    void saveHolding(CallbackContext ctx)
    {
        if (ctx.valueType.FullName == "System.Single")
        {
            isHolding = ctx.ReadValue<float>() == 1;
        }
        if (ctx.valueType.FullName == "UnityEngine.Vector2" && !ctx.performed )
        {
            isHolding = false;
        }

    }

    void sendShot()
    {
        if (!uiblocker.BlockedByUI)
        {
            Debug.Log("Sending shot");

            Character.Instance.sendShot(frame);
            frame = 0;
            setFrame(0);
            Character.Instance.isShotting = false;
        }

    }

    public void onShoot(CallbackContext ctx)
    {
        saveHolding(ctx);
        if (ctx.valueType.FullName == "UnityEngine.Vector2" && isHolding)
        {
            Vector2 val = ctx.ReadValue<Vector2>();
            if (val.y > 0)
            {
                if (frame < frameCount - 1)
                {
                    frame += force;
                    setFrame((int)frame);
                }
                else
                {
                    frame = frameCount - 1;
                }

            }
            if (val.y < 0)
            {
                if (frame >= -frameIndex)
                {
                    frame -= force;
                    setFrame((int)frame);
                }
                else
                {
                    frame = 0;
                }
            }
        }
        if (!isHolding && frame != 0)
        {
            sendShot();
        }
    }
    public void onDrag(CallbackContext ctx)
    {

    }

    private void setFrame(int frameIndex)
    {
        Vector2 newOffset = new Vector2(frameIndex * (1.0f / frameCount), 0);
        meshRenderer.material.SetTextureOffset("_BaseMap", newOffset);
    }

}
