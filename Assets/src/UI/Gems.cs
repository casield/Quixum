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
        audioSource = GetComponent<AudioSource>();

    }
    void Start()
    {
        Client.Instance.addReadyListener(init);

    }

    private void init()
    {
        //
        client = Client.Instance;
      
            Client.Instance.room.State.users.OnAdd+=onAddUser;
            Client.Instance.room.State.users.OnChange+=onAddUser;
        

    }
        private void onAddUser(UserState value, string key)
    {
        if(value.sessionId == Client.Instance.room.SessionId){
            this.gemsText.text = "" + value.gems;
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
