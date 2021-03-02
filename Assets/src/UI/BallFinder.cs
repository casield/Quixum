using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFinder : MonoBehaviour
{
    private Sprite sprite;
    SObject golfBall;
    private Character character;
    private Client client;
    // Start is called before the first frame update
    void Start()
    {
        //  sprite = transform.
        sprite = GetComponent<Sprite>();

        Client.Instance.addReadyListener(init);
    }

    private void init()
    {
        client = Client.Instance;
        character = Character.Instance;

    }

    // Update is called once per frame
    void Update()
    {
        if (golfBall != null)
        {
            Vector3 pos = CameraController.Instance.dCamera.WorldToScreenPoint(golfBall.gameObject.transform.position);
            Vector3 clampedPoint = new Vector3(
                  Mathf.Clamp(pos.x, 0, Screen.width),
                  Mathf.Clamp(pos.z > 0 ? pos.y : -pos.y, 0, Screen.height), 0);
            transform.position = clampedPoint;

        }
        else
        {
            if (client != null)
            {
                if (client.golfballs != null && client.golfballs.ContainsKey(client.room.SessionId))
                {
                    golfBall = client.golfballs[Client.Instance.room.SessionId];
                }

            }



        }
    }
}
