using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaShop : MonoBehaviour
{
    // Start is called before the first frame update
    public GridLayoutGroup layout;
    void Start()
    {
        Client.Instance.addReadyListener(init);
        layout = GetComponentInChildren<GridLayoutGroup>();
    }

    private void init()
    {
        Client.Instance.userState.shop.OnChange += onShopChange;
        Client.Instance.userState.shop.OnAdd += onShopChange;
    }

    private void onShopChange(ArenaItemState value, string key)
    {
        GameObject shopItem = new GameObject();
        shopItem.name = value.type;
        Image img = shopItem.AddComponent<Image>();
        ArenaShopItem itm = shopItem.AddComponent<ArenaShopItem>();
        itm.Instantiate(value);
        shopItem.transform.parent = layout.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
