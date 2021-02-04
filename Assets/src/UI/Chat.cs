using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Chat : MonoBehaviour
{
    // Start is called before the first frame update

    public Button chatButton;
    public ScrollRect chatWindow;

    public TMP_InputField input;
    public GameObject messagePrefab;

    public Client client;

    private bool hasInit = false;

    private ArrayList onNewMessageList = new ArrayList();

    public static Chat Instance;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Instance = this;
    }

    public void addNewMessageListener(Func<bool> func)
    {
        onNewMessageList.Add(func);
    }

    void Start()
    {
        Client.Instance.addReadyListener(init);
        chatButton.onClick.AddListener(() =>
        {
            if (!chatWindow.gameObject.transform.parent.gameObject.activeSelf)
            {
                open();
            }
            else
            {
                close();
            }

        });



    }

    public void open()
    {
        chatWindow.gameObject.transform.parent.gameObject.SetActive(true);
    }
      public void close()
    {
        chatWindow.gameObject.transform.parent.gameObject.SetActive(false);

    }

    void init()
    {
    }

    void newMessage(Message message, int i)
    {
        GameObject meso = Instantiate(messagePrefab, chatWindow.content.gameObject.transform);
        meso.SetActive(true);
        //

        TMP_Text[] arrTxt = meso.GetComponentsInChildren<TMP_Text>();

        arrTxt[1].text = message.message;
        arrTxt[0].text = message.user.sessionId;

        arrTxt[1].GraphicUpdateComplete();

        Canvas.ForceUpdateCanvases();
        //chatWindow.content.GetComponent<VerticalLayoutGroup>().SetLayoutVertical();
        

        //meso.GetComponent<HorizontalLayoutGroup>().SetLayoutHorizontal();
        chatWindow.content.ForceUpdateRectTransforms();
        chatWindow.LayoutComplete();
        
         
        //chatWindow.content.

         //Canvas.ForceUpdateCanvases();
         chatWindow.verticalNormalizedPosition = Mathf.Clamp(chatWindow.verticalNormalizedPosition, 0f, 1f);

        chatWindow.content.position = new Vector3(chatWindow.content.position.x, chatWindow.content.position.y + chatWindow.content.sizeDelta.y + 30, 0);
        foreach (Func<bool> item in onNewMessageList)
        {
            item();
        }


    }
    public async void enviar()
    {

        await client.room.Send("chat", input.text);
        input.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Character.Instance.inputControl.Normal.SendMessage.ReadValue<float>() == 1)
        {
            //chatWindow.gameObject.SetActive(!chatWindow.gameObject.activeSelf);
            input.ActivateInputField();
            if (input.text != "")
            {
                enviar();
            }

        }
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {


    }
}
