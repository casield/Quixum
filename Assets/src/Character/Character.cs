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


    Quaternion savedAxis = Quaternion.identity;

    public Animator AnimationController;
    public BallPointer ballPointer;
    public static Character Instance { get; private set; }

    public bool isShotting = false;

    public bool canRotate = true;
    public bool ballIsMoving = false;

    public InputControl inputControl;

    public bool isDragging = false;
    private GameObject lookAt;
    private bool rotateY = false;
    public Transform cameraFollow;

    public Vector3 cameraSavedPos = new Vector3();
    public Quaternion cameraSavedRot = new Quaternion();
    MoveMessage message = new MoveMessage();
    bool sendStop = false;

    public ArcArrow arcArrow;




    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Instance = this;
        inputControl = new InputControl();
        lookAt = gameObject;
        cameraSavedPos = camara.transform.localPosition;
        cameraSavedRot = camara.transform.localRotation;

        arcArrow = GetComponentInChildren<ArcArrow>();

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
            // Debug.Log(message);

            sendMoveMessage();
            movingPad = true;
        }
        /*if(ctx.canceled && v.x == 0 && v.y == 0){
            message.x = 0;
            message.y = 0;
            sendMoveMessage();
        }*/


    }

    async void sendMoveMessage()
    {
        if (client != null)
        {
            if (client.room != null)
            {
                message.rotX = camara.transform.forward.x;
                message.rotZ = camara.transform.forward.z;
                message.uID = client.room.SessionId;
                await client.room.Send("move", message);
            }

        }


    }

    void OnEnable()
    {
        inputControl.Enable();
        transform.rotation = savedAxis;
    }

    void OnDisable()
    {
        inputControl.Disable();
        savedAxis = transform.rotation;
    }


    public void onDrag(CallbackContext ctx)
    {
        if (isDragging && !uiblocker.BlockedByUI && this.enabled)
        {
            Vector2 delta = ctx.ReadValue<Vector2>();

            rotateView(delta.x, delta.y, rotateY, lookAt);
        }
    }
    public void onClick(CallbackContext ctx)
    {
        isDragging = ctx.ReadValue<float>() == 1 ? true : false;
    }
    void init()
    {
        if (client.golfballs.Count > 0)
        {

            setListeners();
            setChar();
            rotateView(0, 0, false, gameObject);

            hasInit = true;
        }

    }
    public void setListeners()
    {
        client.room.State.turnState.players.OnChange += onPlayersChange;
    }


    public void removeListeners()
    {
        client.room.State.turnState.players.OnChange -= onPlayersChange;
    }

    private void onPlayersChange(TurnPlayerState value, string key)
    {
        if (value.user.sessionId == client.room.SessionId)
        {
            if (savedShots is 0)
            {
                savedShots = value.shots;
            }
            ballIsMoving = value.ballisMoving;
            if (value.shots < savedShots)
            {
                //New shot time;
                canWatchBall = false;
            }
        }

    }

    public void showCharacter()
    {

        // charMesh.SetActive(true);
    }
    public void hideCharacter()
    {

        //charMesh.SetActive(false);
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
        if (canShoot() && !uiblocker.BlockedByUI)
        {
            yRot = 0;

            Vector3 direction = transform.forward;

            Quaternion rotation = transform.rotation;
            // savedRotation =
            savedRotation = rotation;
            ShotMessage sm = new ShotMessage();
            //angle+=15;
            Quaternion qu = Quaternion.Euler(transform.rotation.eulerAngles.x + angle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            sm.force = force;
            sm.angle = new Quat();
            sm.angle.x = qu.x;
            sm.angle.y = qu.y;
            sm.angle.z = qu.z;
            sm.angle.w = qu.w;
            await client.room.Send("shoot", sm);




        }
    }
    void Update()
    {
        if (client.room != null && !hasInit)
        {
            init();
        }
        if (hasInit)
        {

            showCharacter();
            if (!canShoot())
            {
                //hideCharacter();
            }
            if (isDragging && canShoot() && !uiblocker.BlockedByUI && canRotate)
            {
                lookAt = gameObject;
                rotateY = false;

                isShotting = true;
            }
        }
        //        Debug.Log(inputControl.Normal.Move.ReadValue<Vector2>());
        if (inputControl.Normal.Move.ReadValue<Vector2>() == Vector2.zero && !sendStop)
        {
            sendStop = true;
            message.x = 0;
            message.y = 0;
            sendMoveMessage();
        }



    }
    void LateUpdate()
    {
        if (hasInit)
        {
            if (!canShoot() && canWatchBall)
            {
                watchBall();
            }
            else
            {
                if (watchingBall)
                {
                    transform.rotation = savedRotation;
                }
                if (canWatchBall)
                {
                    setChar();
                }

            }

        }
    }
    Vector3 currentSmoothVel = Vector3.zero;
    float currentAngleVel = 0;

    float smoothVelocity = .6f;
    private Quaternion savedRotation;
    private bool watchingBall = false;
    public bool canWatchBall = false;
    private float savedShots = 0;
    private bool movingPad = false;

    void watchBall()
    {
        if (!GameMessages.i.gameObject.activeSelf)
        {
            GameMessages.i.showMessage("Waiting others players");
        }
        watchingBall = true;
        GameObject ball = giveBall();
        if (ball != null)
        {
            cameraFollow.position = ball.transform.position;
            Vector3 targetPosition = cameraFollow.transform.TransformPoint(new Vector3(0, 150, -140));
            //transform.LookAt(cameraFollow.transform);
            camara.transform.position = Vector3.SmoothDamp(camara.transform.position, targetPosition, ref currentSmoothVel, smoothVelocity);
            var target_rot = Quaternion.LookRotation(cameraFollow.position - camara.transform.position);
            var delta = Quaternion.Angle(camara.transform.rotation, target_rot);
            if (delta > 0.0f)
            {
                var t = Mathf.SmoothDampAngle(delta, 0.0f, ref currentAngleVel, smoothVelocity);
                t = 1.0f - t / delta;
                camara.transform.rotation = Quaternion.Slerp(camara.transform.rotation, target_rot, t);
            }
        }


    }

    void setChar()
    {
        if (GameMessages.i.gameObject.activeSelf)
        {
            GameMessages.i.hideMessage();
        }
        watchingBall = false;
        GameObject ball = giveBall();
        if (ball != null)
        {

            //transform.position = ball.transform.position + paddingChar;
            transform.position = ball.transform.position + paddingChar;
            transform.rotation = transform.rotation;
            camara.transform.localPosition = cameraSavedPos;
            camara.transform.localRotation = cameraSavedRot;
        }

        //transform.rotation = savedRotation;

    }


    async void rotateView(float axis, float axisY, bool rotateY, GameObject lookAt)
    {

        xRot += rotationSpeed * axis * 2;
        yRot += rotationSpeed * axisY * 2;
        // GameObject ball = client.golfballs[client.room.SessionId].gameObject;
        if (rotateY)
        {
            transform.rotation = Quaternion.Euler(-yRot, xRot, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, xRot, 0);
        }

        Quaternion qu = Quaternion.Euler(transform.rotation.eulerAngles.x, +transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z);
        Quat quat = new Quat();
        quat.x = qu.x;
        quat.y = qu.y;
        quat.z = qu.z;
        quat.w = qu.w;

       // await client.room.Send("rotate", quat);
        transform.LookAt(gameObject.transform);


        //transform.LookAt(gameObject.transform);


    }

    public GameObject giveBall()
    {
        if (client.golfballs.Count > 0)
        {
            return client.golfballs[client.room.SessionId].gameObject;
        }
        else
        {
            return null;
        }

    }


    public bool canShoot()
    {

        return client.room.State.turnState.players[client.room.SessionId].shots > 0 && !ballIsMoving;
    }

}
