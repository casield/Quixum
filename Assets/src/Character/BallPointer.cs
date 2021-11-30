using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallPointer : MonoBehaviour
{


    private int frameCount = 8;

    private float force = .1f;

    public float velocity = 0;
    InputControl inputControl;
    private bool shotSended = false;
    private Image shootForce;
    private Sprite[] ballPointerSprites;

    private Image pointer;
    public float pointerObjSize = .2f;
    public float shootObjSize = 1f;
    private GameObject pointerObj;
    private Color color = new Color(1, 1, 1, .4f);
    private Color colorShoot = new Color(1, 1, 1, .9f);
    private GameObject shootObj;

    public Material crossPointerMaterial;


    // Start is called before the first frame update
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>

    void Awake()
    {

        //  meshRenderer = GetComponent<MeshRenderer>();

        ballPointerSprites = Resources.LoadAll<Sprite>("GUI/BallPointer");
        QuixConsole.Log("Ballpointer", ballPointerSprites.Length);
        SetShootForceObj();
        SetPointerObj();


    }

    private void SetShootForceObj()
    {
         shootObj = new GameObject("Shoot Object");
        shootObj.gameObject.transform.parent = this.transform.parent;
        shootObj.transform.localPosition = new Vector3();
        shootForce = shootObj.AddComponent<Image>();
        shootForce.sprite = ballPointerSprites[0];
        RectTransform rectTransform = (RectTransform)shootObj.transform;
        rectTransform.transform.localScale = new Vector3(shootObjSize, shootObjSize, shootObjSize);
        shootForce.color = colorShoot;
        shootObj.SetActive(false);
    }

    private void SetPointerObj()
    {
        pointerObj = new GameObject("Pointer Object");
        pointerObj.gameObject.transform.parent = this.transform.parent;
        pointerObj.transform.localPosition = new Vector3();
        pointer = pointerObj.AddComponent<Image>();
        pointer.sprite = Resources.Load<Sprite>("GUI/CrossBallPointer");
        pointer.material=crossPointerMaterial;
        RectTransform rectTransform = (RectTransform)pointerObj.transform;
        rectTransform.transform.localScale = new Vector3(pointerObjSize, pointerObjSize, pointerObjSize);
        pointer.color = color;
    }

    private void Start()
    {
        inputControl = Character.Instance.inputControl;
        inputControl.Normal.Shoot.started += OnShootStart;
        SetObjectsToLook();
    }

    private void OnShootStart(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        this.gameObject.SetActive(true);
    }

    private void SetObjectsToLook()
    {
        if(Character.Instance!=null && Character.Instance.player!=null){
            LookObject look=Character.Instance.player.lookObject;
            if(look!=null){
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(look.transform.position);
                shootObj.transform.position = screenPosition;
               // pointerObj.transform.position=screenPosition;
            }
              
        }
    }
    void Update()
    {
        if (inputControl.Normal.Shoot.ReadValue<float>() == 1)
        {
            if(velocity>ballPointerSprites.Length-1){
                velocity =0;
            }
            shootObj.SetActive(true);

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
                shootObj.SetActive(false);
                sendShot();

            }

        }
        SetObjectsToLook();
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

        shootForce.sprite = ballPointerSprites[frameIndex];
        /* if (meshRenderer == null)
         {
             meshRenderer = GetComponent<MeshRenderer>();
         }
         Vector2 newOffset = new Vector2(frameIndex * (1.0f / frameCount), 0);
         meshRenderer.material.SetTextureOffset("_BaseMap", newOffset);*/
    }

}
