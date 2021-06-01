using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float minimumDistance = .2f;
    private float maximumTime = 1;

    private InputControl inputControl;

    private Vector3 startPosition;
    private float startTime;
    private Vector3 endPosition;
    private float endTime;
    private LineRenderer lineRenderer;
    public Material mat;

    private SwipeMessage swipeMessage;

    private void Awake()
    {
        inputControl = Character.Instance.inputControl;
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.SetPositions(new Vector3[] { new Vector2(0, 0), new Vector2(0, 100) });
        swipeMessage = new SwipeMessage();
    }
    private void OnEnable()
    {
        /*inputControl.Normal.Swipe.started += SwipeStart;
        inputControl.Normal.Swipe.canceled += SwipeEnd;*/
        inputControl.Swipe.Touch.started += OnTouchStart;
        inputControl.Swipe.Touch.canceled += OnTouchEnd;

    }



    private void OnTouchStart(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Touch start");
        SwipeStart();
    }
    private void OnTouchEnd(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Touch end");
        SwipeEnd();
    }

    private void OnDisable()
    {
        inputControl.Swipe.Touch.started -= OnTouchStart;
        inputControl.Swipe.Touch.canceled -= OnTouchEnd;
    }



    private void SwipeStart()
    {
        endTime = Time.fixedTime;
        startPosition = inputControl.Swipe.Position.ReadValue<Vector2>();

        /*startPosition.x /=Screen.width;
        startPosition.y /=Screen.height;*/
        Debug.Log("Swipe start" + startPosition.ToString());
    }
    private void SwipeEnd()
    {
        startTime = Time.fixedTime;
        endPosition = inputControl.Swipe.Position.ReadValue<Vector2>();
        //endPosition.z=-1;
        Debug.Log("Swipe end" + endPosition.ToString());
        DetectSwipe();
    }

    private async void DetectSwipe()
    {
        //  if (Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) <= maximumTime)
        //  {
        //Debug.DrawLine(startPosition, endPosition, Color.red,2,false);

        float dy = startPosition.y - endPosition.y;
        float dX = endPosition.x - startPosition.x;

        float rads = Mathf.Atan2(dy, dX);

        float degrees = rads;
        degrees*=-1;



        swipeMessage.degree = degrees;
        await Character.Instance.client.room.Send("swipe", swipeMessage);
        Debug.Log("Angle" + degrees);
        //  }
    }



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
