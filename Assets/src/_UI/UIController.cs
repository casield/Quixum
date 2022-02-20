using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update

    public Button shopButton;
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        shopButton = root.Q<Button>("shopButton");

        shopButton.clicked += ()=>{
            QuixConsole.Log("Clickded");
            Character.Instance.player.sendMessageToRoom("buy:");
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
