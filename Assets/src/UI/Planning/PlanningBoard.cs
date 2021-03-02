using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlanningBoard : MonoBehaviour
{
    // Start is called before the first frame update
    static int width = 17;
    static int height = 9;
    int repeat = width * height;
    GameObject dragging;
    public List<BoardRect> boardRects = new List<BoardRect>();
    public Dictionary<string, ArenaShopItem> droppedItems = new Dictionary<string, ArenaShopItem>();
    public Sprite rectSprite;


    RectTransform rectTransform;
    GridLayoutGroup layout;

    void Start()
    {
        rectTransform = (RectTransform)transform;
        for (int i = 0; i < repeat; i++)
        {
            GameObject rect = new GameObject();
            rect.name = "Planning Board Rect";
            Image img = rect.AddComponent<Image>();
            img.sprite = rectSprite;
            BoardRect br = rect.AddComponent<BoardRect>();
            br.setIndex(i);
            rect.transform.SetParent(this.transform);
            boardRects.Add(br);
        }

        Client.Instance.addReadyListener(init);
    }

    private void init()
    {
       Client.Instance.userState.board.OnAdd += onAddItem;
       Client.Instance.userState.board.OnChange += onChangeItem;
    }

    private void onChangeItem(ArenaItemState value, string key)
    {
       BoardRect b = boardRects[getI((int)value.position.x,(int)value.position.y)];
        ArenaShopItem itm = droppedItems[value.uID];
        itm.setBoardRect(b);
        Debug.Log("Item changed");
    }

    private void onAddItem(ArenaItemState value, string key)
    {
        GameObject shopItem = new GameObject();
        shopItem.name = value.type;
        Image img = shopItem.AddComponent<Image>();
        ArenaShopItem itm = shopItem.AddComponent<ArenaShopItem>();
        itm.Instantiate(value);
        shopItem.transform.SetParent(layout.transform.parent);
        droppedItems[value.uID] = itm;
    
        Debug.Log("Added item at "+value.position.x+" / "+value.position.y);
        itm.setBoardRect(boardRects[getI((int)value.position.x,(int)value.position.y)]);
        
    }

    private void OnGUI()
    {
        layout = GetComponent<GridLayoutGroup>();
        float num = ((RectTransform)this.transform).rect.width / width;
        layout.cellSize = new Vector2(num, num);
    }


    public int getI(int x, int y)
    {
        return x + width * y;
    }

    public Vector2 getXY(int i)
    {
        int x = i % width;    // % is the "modulo operator", the remainder of i / width;
        int y = i / width;    // where "/" is an integer division
        return new Vector2(x, y);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
