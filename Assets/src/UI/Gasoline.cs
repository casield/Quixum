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
     
           
        
    }


    private void onAddUser(UserState value, string key)
    {
      

    }

}
