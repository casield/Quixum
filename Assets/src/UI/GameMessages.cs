using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMessages : MonoBehaviour
{
    public static GameMessages Instance;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>

    public TextMeshProUGUI textMessage;

    Vector3 savedPosition;
    void Awake()
    {
        Instance = this;
    }
    
    public void showMessage(string text)
    {
       // gameObject.transform.localScale = new Vector3(1,0,1);
        gameObject.SetActive(true);
        textMessage.text = text;
        //textMessage.alpha = 0;
        LeanTween.scaleY(this.gameObject,1,1.3f).setEaseInElastic();

    }
    public void showMessage(string text, int time){
        showMessage(text);

        StartCoroutine(tickToClose(time));
    }
    IEnumerator tickToClose(int time){
        yield return new WaitForSeconds(time);
        hideMessage();
    }
    public void hideMessage()
    {
        LeanTween.scaleY(this.gameObject,0,1.3f).setEaseOutElastic().setOnComplete(()=>{
             gameObject.SetActive(false);
        });
       //
    }
    // Start is called before the first frame update

}
