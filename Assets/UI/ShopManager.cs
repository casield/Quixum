using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UIElements;

public class ShopManager : ListView
{
    public new class UxmlFactory : UxmlFactory<ShopManager, UxmlTraits> { };
    public new class UxmlTraits : ListView.UxmlTraits { };

    public ShopManager()
    {
        this.style.backgroundColor = Color.cyan;
        this.style.height = Length.Percent(100);
        CreateItems();
    }

    private void CreateItems()
    {
        // Create some list of data, here simply numbers in interval [1, 1000]
        const int itemCount = 10;
        var items = new List<string>(itemCount);
        for (int i = 1; i <= itemCount; i++)
            items.Add(i.ToString());

        // The "makeItem" function will be called as needed
        // when the ListView needs more items to render
        Func<VisualElement> makeItem = () => new VisualElement();

        // As the user scrolls through the list, the ListView object
        // will recycle elements created by the "makeItem"
        // and invoke the "bindItem" callback to associate
        // the element with the matching data item (specified as an index in the list)
        Action<VisualElement, int> bindItem = BindItem;

        this.makeItem = makeItem;
        this.bindItem = bindItem;
        this.itemsSource = items;

        // Callback invoked when the user double clicks an item

        // Callback invoked when the user chang
    }

    private void BindItem(VisualElement ve, int index)
    {
        Button buyButon = new Button();
        buyButon.text = "Buy";
        buyButon.RegisterCallback<ClickEvent>((e) =>
        {
            BuyItem();
        });
        ve.Add(buyButon);
    }

    public void BuyItem()
    {
        Character.Instance.player.sendMessageToRoom("buy:");
    }
}
