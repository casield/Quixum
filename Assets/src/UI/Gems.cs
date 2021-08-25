using System;
using System.Collections;
using System.Collections.Generic;
using Colyseus.Schema;
using TMPro;
using UnityEngine;

public class Gems : MonoBehaviour
{
    // Start is called before the first frame update
    Client client;
    public TextMeshProUGUI gemsText;
    private AudioSource audioSource;

    bool registred = false;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {

    }
    void Start()
    {
        Client.Instance.addReadyListener(init);
    }

    private void init()
    {
        //
        client = Client.Instance;

        client.user.userState.OnChange += onUserChange;


    }

    private void onUserChange(List<DataChange> changes)
    {
        foreach (var ch in changes)
        {
            if (ch.Field == "gems")
            {
                setGems(int.Parse(ch.Value.ToString()));
            }
        }
    }


    public void setGems(int gems)
    {
        gemsText.text = "" + gems;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
