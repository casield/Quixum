using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlanningBoard : MonoBehaviour
{
    // Start is called before the first frame update
    static int width = 14;
    static int height = 7;
    int repeat = width * height;
    GameObject dragging;
    public List<BoardRect> boardRects = new List<BoardRect>();
    public Dictionary<string,ArenaShopItem> droppedItems = new Dictionary<string, ArenaShopItem>();
    public Sprite rectSprite;


    RectTransform rectTransform;

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
            rect.transform.parent = this.transform;
            boardRects.Add(br);
        }
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
