using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoardRect : MonoBehaviour
{
    Image image;
    ArenaShopItem item;
    private bool activated = false;

    public int index { get; private set; }
    public void OnDrop(ArenaShopItem item)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform)transform, item.transform.position))
        {
            this.item = item;
            item.setBoardRect(this);
            Activate();

        }
    }

    public void Activate()
    {
        image.color = new Color(0, 0, .7f, .3f);
        activated = true;

    }

    public void Disable()
    {

        image.color = new Color(1, 1, 1, 1);
        activated = false;
    }

    internal void setIndex(int i)
    {
        index = i;
    }


    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
