using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;
    private GameObject cameraFollow;

    Vector3 currentSmoothVel = Vector3.zero;
    float currentAngleVel = 0;

    float smoothVelocity = 2f;

    public static CameraController Instance {get;private set;}
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        cameraFollow = new GameObject("Camera Follow");
       // Client.Instance.addReadyListener(init);
    }

    private void init()
    {
        Debug.Log("Init");
       // Debug.Log(o);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player != null)
        {
            cameraFollow.transform.position = player.transform.position;
            Vector3 targetPosition = cameraFollow.transform.TransformPoint(new Vector3(90, 90, 0));
            //transform.LookAt(cameraFollow.transform);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentSmoothVel, smoothVelocity);
            var target_rot = Quaternion.LookRotation(cameraFollow.transform.position - transform.position);
            var delta = Quaternion.Angle(transform.rotation, target_rot);
            if (delta > 0.0f)
            {
                var t = Mathf.SmoothDampAngle(delta, 0.0f, ref currentAngleVel, smoothVelocity);
                t = 1.0f - t / delta;
                transform.rotation = Quaternion.Slerp(transform.rotation, target_rot, t);
                //transform.Rotate(0,90,0);
            }
        }
    }
}
