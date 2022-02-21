using System;
using System.Collections;
using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;
using UnityEngine.UIElements;
public class ShopController : MonoBehaviour
{
    VisualElement root;
    Button closeButton;
    public GameObject canvas;

    void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        Init();
        closeButton.clicked += Close;
    }
    private void OnDisable()
    {
        closeButton.clicked -= Close;
    }

    private void Init()
    {
        // Create some list of data, here simply numbers in interval [1, 1000]
        root = GetComponent<UIDocument>().rootVisualElement;
        CreateList();
        closeButton = root.Q<Button>("close-shop");
    }

    private void CreateList()
    {
        const int itemCount = 100;
        var items = new List<string>(itemCount);
        for (int i = 1; i <= itemCount; i++)
            items.Add(i.ToString());

        // The "makeItem" function will be called as needed
        // when the ListView needs more items to render
        Func<VisualElement> makeItem = () => new Label();

        // As the user scrolls through the list, the ListView object
        // will recycle elements created by the "makeItem"
        // and invoke the "bindItem" callback to associate
        // the element with the matching data item (specified as an index in the list)
        Action<VisualElement, int> bindItem = (e, i) => (e as Label).text = items[i];

        var listView = root.Q<ListView>();
        listView.makeItem = makeItem;
        listView.bindItem = bindItem;
        listView.itemsSource = items;
        listView.selectionType = SelectionType.Multiple;

        // Callback invoked when the user double clicks an item
        listView.onItemsChosen += Debug.Log;
    }

    public void Open()
    {
        root.style.visibility = Visibility.Visible;
    }

    public void Buy()
    {

        Character.Instance.player.sendMessageToRoom("buy:");
    }

    public void Close()
    {
        QuixConsole.Log("Close");
        root.style.visibility = Visibility.Hidden;
    }
}