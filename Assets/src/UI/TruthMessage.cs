using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;


public class TruthMessage : MonoBehaviour
{
    // Start is called before the first frame update
    public static TruthMessage Instance;
    public UnityAction trueAction;
    public UnityAction falseAction;

    public TextMeshProUGUI titleText, descriptionText, trueText, falseText;
    private float showhideTime = .3f;

    public TruthMessage()
    {
        Instance = this;
    }

    private void Awake()
    {
        this.transform.localScale = Vector3.zero;
    }

    public void setActions(UnityAction trueA, UnityAction falseA)
    {
        trueAction = trueA;
        falseAction = falseA;
    }

    public void show(string title, string description, string trueS, string falseS)
    {
        titleText.text = title;
        descriptionText.text = description;
        trueText.text = trueS;
        falseText.text = falseS;
        this.gameObject.SetActive(true);
        //this.transform.localScale.Set(0, 0, 0);

       // this.transform.LeanScale(new Vector3(1, 1), showhideTime);
    }
    public void hide()
    {
       // this.transform.LeanScale(new Vector3(0, 0), showhideTime).setOnComplete(() =>
      /* {
           this.gameObject.SetActive(false);
       });*/

    }

    public void onTrueClick()
    {
        trueAction();
        hide();
    }
    public void onFalseClick()
    {
        if (falseAction != null)
        {
            falseAction();
        }

        hide();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
