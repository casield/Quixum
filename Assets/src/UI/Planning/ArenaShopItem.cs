using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArenaShopItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public List<Vector2> spaces = new List<Vector2>();
    public BoardRect boardRect;
    private Image image;
    private ArenaItemState state;

    private Transform originalParent;
    private BoardRect lastBoard;
    private BoardRect rectOnDrag;

    private void setSpaces()
    {
        var hh = (state.height);
        var ww = (state.width);
        for (var y = 0; y < hh; y++)
        {
            for (var x = 0; x < ww; x++)
            {
                spaces.Add(new Vector2(x, y));
            }
        }
    }

    public void Instantiate(ArenaItemState item)
    {
        state = item;
        setSpaces();
        Sprite sprite = Resources.Load<Sprite>("Images/Arena/" + item.type);
        image = this.gameObject.GetComponent<Image>();


        image.sprite = sprite;

        item.OnRemove += destroy;
        if (item.position != null)
        {
            int xx = PlanningUI.Instance.planningBoard.getI((int)item.position.x, (int)item.position.y);
            BoardRect rect = PlanningUI.Instance.planningBoard.boardRects[xx];
            colorRects(rect, true);
        }

    }
    internal void destroy()
    {
        if (lastBoard != null)
        {
            colorRects(lastBoard, false);
        }

        Destroy(this.gameObject);
    }

    public void setBoardRect(BoardRect br)
    {
        this.boardRect = br;
        this.transform.position = this.boardRect.transform.position;
        if (boardRect != null)
        {
            ((RectTransform)this.transform).sizeDelta = ((RectTransform)boardRect.transform).sizeDelta;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        PlanningUI.Instance.setDraggingObject(this);
        if (boardRect != null)
        {
            boardRect.Disable();
            colorRects(boardRect, false);
        }

    }
    public void OnDrag(PointerEventData eventData)
    {
        /*
        *Needed to work!!
        */
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (var item in results)
        {
            BoardRect rect = item.gameObject.GetComponent<BoardRect>();
            if (rect != null)
            {
                colorRects(rect, true);

                if (rectOnDrag != null && rectOnDrag != rect)
                {
                    colorRects(rectOnDrag, false);
                    rectOnDrag = rect;
                }
                else
                {
                    rectOnDrag = rect;
                    Debug.Log("one time");
                }




            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {


        /* PlanningUI.Instance.dropObject();*/
        lastBoard = boardRect;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        bool inPlanningBoard = false;
        foreach (var item in results)
        {

            Debug.Log("Results " + item.gameObject.name);
            BoardRect rect = item.gameObject.GetComponent<BoardRect>();
            PlanningBoard planningBoard = item.gameObject.GetComponent<PlanningBoard>();
            if (rect != null)
            {
                if (isMine())
                {

                    moveToPosition(rect);
                    updateState();
                    colorRects(rect, true);
                    rect.OnDrop(this);
                    tryBuy();
                }
                else
                {
                    StartCoroutine(WaitBuyResponse(rect));
                }

            }
            if (planningBoard != null)
            {
                inPlanningBoard = true;
            }

        }

        if (!inPlanningBoard && isMine())
        {
            TruthMessage.Instance.setActions(sell, cancelSell);
            TruthMessage.Instance.show("Sell " + this.state.type, "Do you wanna sell " + this.state.type + " for " + this.state.price / 2 + " gems?", "Yes", "No");
        }

        PlanningUI.Instance.dropObject();

    }

    private void cancelSell()
    {
        Debug.Log(("Cancel sell " + lastBoard.transform.position.ToString()));

        setBoardRect(lastBoard);
        colorRects(lastBoard, true);
    }

    private async void sell()
    {
        await Client.Instance.room.Send("sellItem", state);
    }

    public bool isMine()
    {
        return Client.Instance.userState.board.ContainsKey(this.state.uID);
    }

    private void moveToPosition(BoardRect rect)
    {
        Vector2 pos = PlanningUI.Instance.planningBoard.getXY(rect.index);
        state.position.x = pos.x;
        state.position.y = pos.y;


    }
    private IEnumerator WaitBuyResponse(BoardRect rect)
    {
        moveToPosition(rect);
        tryBuy();

        rect.OnDrop(this);
        yield return new WaitForSeconds(.5f);
    }


    private void updateState()
    {
        PlanningUI.Instance.planningBoard.droppedItems[state.uID] = this;
    }

    private async void tryBuy()
    {
        if (Client.Instance.userState != null)
        {
            await Client.Instance.room.Send("buyItem", state);
        }
    }

    public void colorRects(BoardRect rect, bool activate)
    {
        foreach (Vector2 space in spaces)
        {

            Vector2 pos = PlanningUI.Instance.planningBoard.getXY(rect.index);
            int i = PlanningUI.Instance.planningBoard.getI((int)pos.x + (int)space.x, (int)pos.y + (int)space.y);
            if (i < PlanningUI.Instance.planningBoard.boardRects.Count - 1 && i > 0)
            {
                if (activate)
                {
                    PlanningUI.Instance.planningBoard.boardRects[i].Activate();
                }
                else
                {
                    PlanningUI.Instance.planningBoard.boardRects[i].Disable();
                }

            }

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        originalParent = this.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
