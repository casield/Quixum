using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using static UnityEngine.InputSystem.InputAction;

public class Character : MonoBehaviour
{
    public Camera camara;
    public Client client;
    private float rotationSpeed;
    float xRot, yRot = 0f;
    Vector2 lastMousePos = new Vector2();
    Vector3 paddingChar = new Vector3(0, 0, -2.5f);
    Vector3 paddingPointer = new Vector3(0, 0, 2f);
    bool hasInit, hasSetChar = false;

    public Vector3 pointOfContact = new Vector3();


    Quaternion savedAxis = Quaternion.identity;

    public Animator GolfClubController;
    public static Character Instance { get; private set; }

    public bool isShotting = false;

    public GameObject charMesh;
    public bool canRotate = true;
    private bool ballIsMoving = false;

    public RectTransform fingerImage;
    public InputControl inputControl;

    public bool isDragging = false;
    private GameObject lookAt;
    private bool rotateY = false;
    public Transform cameraFollow;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Instance = this;
       inputControl = new InputControl();
        inputControl.Enable();
        lookAt = gameObject;
    }


    void Start()
    {
        rotationSpeed = 36f / Screen.width;
        foreach (InputDevice item in InputSystem.devices)
        {
            Debug.Log("Device: "+item.displayName);
        }
    }

    void OnEnable()
    {
        transform.rotation = savedAxis;
    }

    void OnDisable()
    {
        savedAxis = transform.rotation;
    }
    

    public void onDrag(CallbackContext ctx)
    {
        if (isDragging && canShoot() && !uiblocker.BlockedByUI && this.enabled)
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
        client.room.State.world.objects.OnChange += OnBallChange;
    }

    private void OnBallChange(ObjectState value, string key)
    {
        if (value.owner.sessionId == client.room.SessionId)
        {


        }
    }

    public void removeListeners()
    {
        client.room.State.turnState.players.OnChange -= onPlayersChange;
    }

    private void onPlayersChange(TurnPlayerState value, string key)
    {
        if (value.user.sessionId == client.room.SessionId)
        {
            ballIsMoving = value.ballisMoving;
        }

    }

    public void showCharacter()
    {

        charMesh.SetActive(true);
    }
    public void hideCharacter()
    {

        charMesh.SetActive(false);
    }
    public async void stopBall(CallbackContext ctx)
    {
        if(ctx.performed){
            await client.room.Send("stop", null);
        }
        
    }
    public async void sendShot(float force)
    {
        if (canShoot() && !uiblocker.BlockedByUI)
        {
            yRot = 0;

            Vector3 direction = transform.forward;

            Quaternion rotation = transform.rotation;
            // savedRotation =
            savedRotation = rotation;
            await client.room.Send("shoot", new CustomVector3(direction.x, direction.y, direction.z,
                    force,
                    rotation.x, rotation.y, rotation.z, rotation.w, pointOfContact.x, pointOfContact.y));
            GolfClubController.SetBool("isShotting", false);
            pointOfContact.x = 0;
            pointOfContact.y = 0;
            //ShootDirection.Instance.reset();




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
                hideCharacter();
            }
            if (isDragging && canShoot() && !uiblocker.BlockedByUI && canRotate)
            {
                lookAt = gameObject;
                rotateY = false;
                GolfClubController.SetBool("isShotting", true);

                isShotting = true;
            }
        }



    }
    void LateUpdate()
    {
        if (hasInit)
        {
            if (!canShoot())
            {
                watchBall();
            }
            else
            {
                if (watchingBall)
                {
                    transform.rotation = savedRotation;
                }
                setChar();
            }

        }
    }
    Vector3 currentSmoothVel = Vector3.zero;
    float currentAngleVel = 0;

    float smoothVelocity = 1.3f;
    private Quaternion savedRotation;
    private bool watchingBall = false;

    void watchBall()
    {
        if (!GameMessages.i.gameObject.activeSelf)
        {
            GameMessages.i.showMessage("Waiting others players");
        }
        watchingBall = true;
        GameObject ball = giveBall();
        cameraFollow.position = ball.transform.position;
        Vector3 targetPosition = cameraFollow.transform.TransformPoint(new Vector3(0, 50, -80));
        //transform.LookAt(cameraFollow.transform);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentSmoothVel, smoothVelocity);
        var target_rot = Quaternion.LookRotation(cameraFollow.position - transform.position);
        var delta = Quaternion.Angle(transform.rotation, target_rot);
        if (delta > 0.0f)
        {
            var t = Mathf.SmoothDampAngle(delta, 0.0f, ref currentAngleVel, smoothVelocity);
            t = 1.0f - t / delta;
            transform.rotation = Quaternion.Slerp(transform.rotation, target_rot, t);
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
        if(ball != null){
             transform.position = ball.transform.position + paddingChar;
        }
       
        //transform.rotation = savedRotation;

    }


    void rotateView(float axis, float axisY, bool rotateY, GameObject lookAt)
    {
        
            xRot += rotationSpeed * axis * 2;
            yRot += rotationSpeed * axisY * 2;
            GameObject ball = client.golfballs[client.room.SessionId].gameObject;
            if (rotateY)
            {
                transform.rotation = Quaternion.Euler(-yRot, xRot, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, xRot, 0);
            }
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


    bool canShoot()
    {

        return client.room.State.turnState.players[client.room.SessionId].shots > 0 && !ballIsMoving;
    }
    class CustomVector3
    {
        public float x, y, z;
        public float force;
        public float rotx, roty, rotz, rotw;
        public float contactx, contacty;


        public CustomVector3(float x, float y, float z, float force, float rotx, float roty, float rotz, float rotw, float contactx, float contacty)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.force = force;
            this.rotx = rotx;
            this.roty = roty;
            this.rotz = rotz;
            this.rotw = rotw;
            this.contactx = contactx;
            this.contacty = contacty;
            // this.pointw = pointw; 
        }
    }

}
