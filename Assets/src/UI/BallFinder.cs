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

    private Finder finder;
    // Start is called before the first frame update
    void Start()
    {
        //  sprite = transform.
        sprite = GetComponent<Sprite>();

        Client.Instance.addReadyListener(init);

        finder = GetComponent<Finder>();
    }

    private void init()
    {
        client = Client.Instance;
        character = Character.Instance;

    }

    // Update is called once per frame
    void Update()
    {
      

        if(finder.findObject == null){
            if (client != null)
            {
                if (client.golfballs != null && client.golfballs.ContainsKey(client.room.SessionId))
                {
                    finder.findObject = client.golfballs[Client.Instance.room.SessionId].gameObject;
                }

            }
        }
    }
}
