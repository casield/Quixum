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

    RectTransform rectTransform;

    void Start()
    {
        rectTransform = (RectTransform)transform;
        for (int i = 0; i < repeat; i++)
        {
            GameObject rect = new GameObject();
            Image img = rect.AddComponent<Image>();
            BoardRect br = rect.AddComponent<BoardRect>();
            br.setIndex(i);
            rect.transform.parent = this.transform;
            boardRects.Add(br);
        }
    }

    public void onDropObject(ArenaShopItem item)
    {
        RectTransform itemTrans = (RectTransform)item.transform;
        // rectTransform.rect.Overlaps(itemTrans.rect);
        rectTransform.ForceUpdateRectTransforms();
        if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, item.transform.position))
        {
            foreach (var br in boardRects)
            {
                br.OnDrop(item);
            }
        }
        else
        {
            GameMessages.Instance.showMessage("Do you want to erase this object?", 10);
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
