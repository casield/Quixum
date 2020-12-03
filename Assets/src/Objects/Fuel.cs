using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    // Start is called before the first frame update

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>

    public static Fuel Instance;
    private TextMeshProUGUI text;
    void Awake()
    {
        Instance =this;
        text = this.GetComponentInChildren<TextMeshProUGUI>();
    }
    void Start()
    {
        Client.Instance.addReadyListener(init);
    }

    private void init()
    {
        Client.Instance.room.State.users.OnChange+=OnUserChange;
    }

    private void OnUserChange(UserState value, string key)
    {
       // Debug.Log("Gasoline"+value.gasoline);
        text.text = ""+value.gasoline;
    }
}
