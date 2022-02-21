using System;
using System.Collections;
using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update

    public Button shopButton;
    public Label gemsLabel;

    public UIDocument inGameDoc;
    public ShopController shop;

    public ScrollView shopListView;
    void Start()
    {

        shopButton = inGameDoc.rootVisualElement.Q<Button>("shop-button");
        gemsLabel = inGameDoc.rootVisualElement.Q<Label>("gems-label");

        shopButton.clicked += () =>
        {
            QuixConsole.Log("Clickded");
            shop.Open();
        };
        Client.Instance.addReadyListener(init);
    }

    private void init()
    {
         Client.Instance.user.userState.OnChange += OnUserChange;
    }

    private void OnUserChange(List<DataChange> changes)
    {
       foreach (var ch in changes)
        {
            if (ch.Field == "gems")
            {
                gemsLabel.text =""+ch.Value;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
