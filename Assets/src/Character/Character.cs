using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[DefaultExecutionOrder(-1)]
public class Character : MonoBehaviour
{
    public Camera camara;
    public Client client;
    private float rotationSpeed;
    float xRot, yRot = 0f;
    Vector3 paddingChar = new Vector3(0, 0, -2.5f);
    bool hasInit, hasSetChar = false;

    public Vector3 pointOfContact = new Vector3();



    public Animator AnimationController;
    public BallPointer ballPointer;
    public static Character Instance { get; private set; }

    public bool isShotting = false;

    public bool canRotate = true;
    public bool ballIsMoving = false;

    public InputControl inputControl;

    public bool isDragging = false;
    private GameObject lookAt;

    MoveMessage message = new MoveMessage();
    MoveMessage jumpmessage = new MoveMessage();
    GauntletMessage gauntletMessage = new GauntletMessage();
    bool sendStop = false;

    public ArcArrow arcArrow;
    private bool movingPad = false;
    private bool sendJump = false;

    public Player2 player;

    public Material defaultMaterial;

    public Vector2 movm = new Vector2();




    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Instance = this;
        inputControl = new InputControl();
        lookAt = gameObject;

        arcArrow = GetComponentInChildren<ArcArrow>();

        inputControl.Swipe.Gauntlet.performed += onUseGauntlet;

        // inputControl.Normal.Move.started += onMove;
        inputControl.Normal.Move2.canceled += OnMove;
        inputControl.Normal.Move2.started += OnMove;

        inputControl.Normal.Move2.performed += OnMove;

        Debug.Log("Created character.cs");

    }

    private async void onUseGauntlet(CallbackContext obj)
    {

        float number = obj.ReadValue<float>();
        Debug.Log("Using gauntlet " + number);
        gauntletMessage.active = number == 0 ? false : true;
        await this.client.room.Send("gauntlet", gauntletMessage);
        // await this.client.room.Send("createBoxes");
    }

    void Start()
    {

        rotationSpeed = 36f / Screen.width;
        foreach (InputDevice item in InputSystem.devices)
        {
            Debug.Log("Device: " + item.displayName);
        }
    }

    public void OnMove(CallbackContext ctx)
    {
        movm = ctx.ReadValue<Vector2>();
        Debug.Log("on drag inside " + movm);


        Vector2 v = ctx.ReadValue<Vector2>();
        message.x = Convert.ToInt16(v.x * 100f);
        message.y = Convert.ToInt16(v.y * 100f);

        sendMoveMessage();



    }

    async void sendMoveMessage()
    {
        if (client != null)
        {
            if (client.room != null)
            {

                if (player != null)
                {
                    message.uID = player.state.uID;
                    await client.room.Send("move", message);
                }

            }

        }


    }

    void OnEnable()
    {
        inputControl.Enable();
    }

    void OnDisable()
    {
        inputControl.Disable();
    }

    void init()
    {
        if (client.golfballs.Count > 0)
        {
            hasInit = true;
        }

    }
    private async void jumpControl()
    {
        if (inputControl != null)
        {
            if (inputControl.Normal.Jump.phase == InputActionPhase.Started)
            {
                jumpmessage.uID = Character.Instance.player.state.uID;
                await this.client.room.Send("jump", jumpmessage);
                this.sendJump = true;
            }
        }

    }

    public void removeListeners()
    {
        //client.room.State.turnState.players.OnChange -= onPlayersChange;
    }
    public async void stopBall(CallbackContext ctx)
    {
        if (ctx.performed)
        {
            await client.room.Send("stop", null);
        }

    }
    public async void sendShot(float force, float angle)
    {
        angle = 0;
        if (!uiblocker.BlockedByUI)
        {
            ShotMessage sm = new ShotMessage();

            sm.force = force;
            await client.room.Send("shoot", sm);
        }
    }
    void Update()
    {

        jumpControl();


    }

}
