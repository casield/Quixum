using System;
using System.Collections;
using System.Collections.Generic;
using Colyseus.Schema;
using TMPro;
using UnityEngine;

public class Gasoline : MonoBehaviour
{
    // Start is called before the first frame update

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>

    public static Gasoline Instance;
    private TextMeshProUGUI text;
    void Awake()
    {
        Instance = this;
        text = this.GetComponentInChildren<TextMeshProUGUI>();
    }
    void Start()
    {
        Client.Instance.addReadyListener(init);
    }

    private void init()
    {
     
            Client.Instance.room.State.users.OnAdd += onAddUser;
            Client.Instance.room.State.users.OnChange += onAddUser;
        
    }


    private void onAddUser(UserState value, string key)
    {
        if (value.sessionId == Client.Instance.room.SessionId)
        {
            /* Debug.Log("OnAddUser "+JsonUtility.ToJson(value));
             value.OnChange += OnUserChange;*/
            Debug.Log("Changes in Gasoline");
            text.text = "" + value.gasoline;
        }

    }

}
