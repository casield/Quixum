using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    // Start is called before the first frame update

    public Button button;
    private UnityAction onCloseFunc;
    void Start()
    {
        button.onClick.AddListener(clickButton);
    }

    public void clickButton(){
       
        /*LeanTween.scale(transform.parent.gameObject,new Vector3(0,0,0),0.2f).setEase(LeanTweenType.easeInBack).setOnComplete(()=>{
             transform.parent.gameObject.SetActive(!transform.parent.gameObject.activeSelf);
             transform.parent.localScale = Vector3.one;
        });*/
        if(onCloseFunc != null){
            onCloseFunc();
        }
       
    }

    public void onClose(UnityAction func){
        onCloseFunc = func;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
