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

    private void Activate()
    {
        Debug.Log("Activate");
        image.color = new Color(1, 1, 1, .5f);
        gameObject.name = "Activated";

        if (item != null)
        {
            foreach (Vector2 space in item.spaces)
            {
                Vector2 pos = PlanningUI.Instance.planningBoard.getXY(index);
                int i = PlanningUI.Instance.planningBoard.getI((int)pos.x + (int)space.x, (int)pos.y + (int)space.y);
                if (i < PlanningUI.Instance.planningBoard.boardRects.Count - 1 && i > 0)
                {
                    PlanningUI.Instance.planningBoard.boardRects[i].Activate();
                }

            }
        }

    }

    public void Disable(){
        image.color = new Color(1, 1, 1, 1);
        if (item != null)
        {
            foreach (Vector2 space in item.spaces)
            {
                Vector2 pos = PlanningUI.Instance.planningBoard.getXY(index);
                int i = PlanningUI.Instance.planningBoard.getI((int)pos.x + (int)space.x, (int)pos.y + (int)space.y);

                if (i < PlanningUI.Instance.planningBoard.boardRects.Count - 1 && i > 0)
                {
                    PlanningUI.Instance.planningBoard.boardRects[i].Disable();
                }
                item = null;
            }
        }
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
