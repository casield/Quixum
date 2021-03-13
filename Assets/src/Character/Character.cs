using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Interactions;
using static UnityEngine.InputSystem.InputAction;

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
    bool sendStop = false;

    public ArcArrow arcArrow;
    private bool movingPad = false;
    private bool sendJump = false;

    public Player2 player;

    public Material defaultMaterial;




    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Instance = this;
        inputControl = new InputControl();
        lookAt = gameObject;

        arcArrow = GetComponentInChildren<ArcArrow>();

        inputControl.Normal.Use_Power1.performed+=onUserPower1;

    }

    private async void onUserPower1(CallbackContext obj)
    {
        await this.client.room.Send("use_Power1");
    }

    void Start()
    {

        rotationSpeed = 36f / Screen.width;
        foreach (InputDevice item in InputSystem.devices)
        {
            Debug.Log("Device: " + item.displayName);
        }
    }

    public void onMove(CallbackContext ctx)
    {
        Vector2 v = ctx.ReadValue<Vector2>();

        if (ctx.performed)
        {
            sendStop = false;
            message.x = Convert.ToInt16(v.x * 100f);
            message.y = Convert.ToInt16(v.y * 100f);
            //message.y = float.Parse(v.y.ToString("0.00"));

            sendMoveMessage();
            movingPad = true;
        }
    }

    async void sendMoveMessage()
    {
        if (client != null)
        {
            if (client.room != null)
            {
                /*message.rotX = camara.transform.forward.x;
                message.rotZ = camara.transform.forward.z;*/
                if(player != null){
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
       if(inputControl.Normal.Jump.phase == InputActionPhase.Started ){
           jumpmessage.uID = Character.Instance.player.state.uID;
          await this.client.room.Send("jump",jumpmessage);
          this.sendJump = true;
       }
      /* if(inputControl.Normal.Jump.phase == InputActionPhase.Waiting && sendJump){
          await this.client.room.Send("jump",Character.Instance.player.state.owner);
          sendJump = false;
       }*/

      // Debug.Log(inputControl.Normal.Jump.phase);
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
        if (inputControl.Normal.Move.ReadValue<Vector2>() == Vector2.zero && !sendStop)
        {
            sendStop = true;
            message.x = 0;
            message.y = 0;
            sendMoveMessage();
            Debug.Log("Sending stop");
        }

       jumpControl();


    }

}
