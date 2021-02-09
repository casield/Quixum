using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shots : MonoBehaviour
{
    // Start is called before the first frame update
    Client client;
    private ArrayList shotsArray = new ArrayList();
    public RectTransform shotDot;

    void Start()
    {
        Client.Instance.addReadyListener(init);
    }

    private void init()
    {
        client = Client.Instance;
        //client.room.State.turnState.players.OnChange += onPlayersChange;
    }

    void setBalls(int initialShots, int shotNumber)
    {
        if (initialShots == shotNumber)
        {
            foreach (Image item in shotsArray)
            {
                item.color = Color.white;
            }
        }
        if (initialShots != shotsArray.Count)
        {
            foreach (Image item in shotsArray)
            {
                Destroy(item.gameObject);
            }
            shotsArray.Clear();
            for (int a = 0; a < initialShots; a++)
            {
                GameObject newS = Instantiate(shotDot.gameObject, transform);
                RectTransform tr = newS.GetComponent<RectTransform>();
                Image img = newS.GetComponent<Image>();
                tr.position = new Vector3(tr.position.x + (25 * a), tr.position.y, tr.position.z);
                shotsArray.Add(img);
            }
           recolorShots(shotNumber);
        }
        else
        {
            recolorShots(shotNumber);
        }



    }

    void recolorShots(int shotNumber)
    {
        for (int a = 0; a < shotsArray.Count; a++)
        {
            Image img = (Image)shotsArray[a];
            if (a >= shotNumber)
            {
                img.GetComponent<Image>().color = new Color(1, 1, 1, .2f);
            }
        }
    }

}
