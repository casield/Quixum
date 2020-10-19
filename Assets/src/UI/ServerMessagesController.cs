using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ServerMessagesController : MonoBehaviour
{
    // Start is called before the first frame update
    public CanvasGroup errorWindow;
    public TextMeshProUGUI errorText;
    public static ServerMessagesController Instance;

    void Awake()
    {
        Instance = this;
        
    }

    void close(){
        gameObject.SetActive(false);

    }
    void Start()
    {
        errorWindow.GetComponentInChildren<CloseButton>().onClose(close);
    }

    public void showError(string error)
    {
        gameObject.SetActive(true);
        errorWindow.gameObject.SetActive(true);
        errorWindow.alpha = 0;
        errorText.text = error;
        LeanTween.alphaCanvas(errorWindow, 1, 0.3f).setEaseInBack();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
