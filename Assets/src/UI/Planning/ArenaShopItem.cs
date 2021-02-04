using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArenaShopItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public List<Vector2> spaces = new List<Vector2>();
    public BoardRect boardRect;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
        PlanningUI.Instance.setDraggingObject(this);
        if(boardRect != null){
            boardRect.Disable();
        }
    }

    private void setSpaces(){
        spaces.Add(new Vector2(1,0));
        spaces.Add(new Vector2(2,0));
        spaces.Add(new Vector2(-1,0));
        spaces.Add(new Vector2(-2,0));
    }

    public void setBoardRect(BoardRect br){
        this.boardRect = br;
    }

    public void OnDrag(PointerEventData eventData)
    {
        /*
        *Needed to work!!
        */
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PlanningUI.Instance.dropObject();
    }

    // Start is called before the first frame update
    void Start()
    {
        setSpaces();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
