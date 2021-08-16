using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class BallPointer : MonoBehaviour
{

    private MeshRenderer meshRenderer;

    private int frameCount = 8;

    private float force = .1f;

    public float velocity = 0;
    InputControl inputControl;
    private bool shotSended = false;

    // Start is called before the first frame update
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>

    void Awake()
    {

        meshRenderer = GetComponent<MeshRenderer>();

    }

    private void Start()
    {
        inputControl = Character.Instance.inputControl;
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (inputControl.Normal.Shoot.ReadValue<float>() == 1)
        {
            velocity += force;
            setFrame((int)velocity);

            if (velocity >= frameCount)
            {
                velocity = 0;
            }
            Character.Instance.arcArrow.velocity = (velocity + 1) * 10;
            shotSended = false;
        }
        else
        {
            if (velocity != 0 && !shotSended)
            {
                sendShot();

            }

        }
    }


    public void sendShot()
    {
        if (!uiblocker.BlockedByUI)
        {
            Debug.Log("Sending shot");

            Character.Instance.sendShot(velocity, Character.Instance.arcArrow.angle);
            velocity = 0;
            setFrame(0);
            Character.Instance.isShotting = false;
            shotSended = true;
        }

    }
    private void setFrame(int frameIndex)
    {
        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
        Vector2 newOffset = new Vector2(frameIndex * (1.0f / frameCount), 0);
        meshRenderer.material.SetTextureOffset("_BaseMap", newOffset);
    }

}
