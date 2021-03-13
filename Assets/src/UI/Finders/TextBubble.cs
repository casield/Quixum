using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBubble : MonoBehaviour
{
    // Start is called before the first frame update
    public Image BubbleTail;
    private Finder finder;
    public TextMeshProUGUI text;

    private RectTransform rectTransform;
    private VerticalLayoutGroup textContainer;
    private Canvas canvas;
    private string savedText;

    public float maxDistance = 2000;
    void Start()
    {
        rectTransform = (RectTransform)transform;
        finder = GetComponent<Finder>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        textContainer = GetComponentInChildren<VerticalLayoutGroup>();
        canvas = GetComponentInParent<Canvas>();
        changeText(text.text);

        finder.padding = new Vector3(0, 70, 0);
    }

    

    public void changeText(string text)
    {
        savedText = text;
       GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public void setFindObject(GameObject gm){
        GetComponent<Finder>().findObject = gm;
    }

    // Update is called once per frame
    void Update()
    {
        if (!finder.IsTargetVisible())
        {
            text.text = "...";
        }
        else
        {
            text.text = savedText;
        }
        RectTransform textContainerRect = ((RectTransform)textContainer.transform);


        float y = (textContainerRect.rect.height / 2);




        if (finder.IsTargetVisible())
        {

            float distance = Vector3.Distance(Camera.main.transform.position, finder.findObject.transform.position);
            distance = Mathf.Clamp(distance, 0, maxDistance);
            float porcentage = 1 - (distance / maxDistance);
            Debug.Log(porcentage);
            transform.localScale = new Vector3(porcentage, porcentage, porcentage);
            y *= porcentage;
            //finder.padding *= porcentage;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

      //  Vector2 pos = new Vector2(transform.position.x, (textContainerRect.position.y - y)*canvas.scaleFactor);
        BubbleTail.transform.localPosition = new Vector3(0,-(textContainerRect.rect.height / 2),0);


    }
}
