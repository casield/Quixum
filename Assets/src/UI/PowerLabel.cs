
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class PowerLabel : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI priceText;
    public Image powerImage;

    private Dictionary<string, Sprite> powersDic = new Dictionary<string, Sprite>();
    public Button button;
    public RectTransform rectTransform;


    private bool followFingerBool = false;

    private PowerState powerState;


    private UnityAction onClickAction;

    public Image BackGroundImage;

    public bool displayCircle = false;

    public Sprite circleBackgroundSprite;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        button.onClick.AddListener(onClickAction);
    }
    public void onClick(UnityAction action)
    {
        onClickAction = action;
    }

    public async void buy()
    {
        await Client.Instance.room.Send("buy-power", powerState);
    }
    public async void activate()
    {
        Debug.Log("Activating: " + powerState.type);
        await Client.Instance.room.Send("activate-power", powerState);
    }

    public void load(PowerState state)
    {
        powersDic.Add("AddOneShot", Resources.Load<Sprite>("Images/Powers/4x/AddOneShot"));
        powersDic.Add("CreateBox", Resources.Load<Sprite>("Images/Powers/4x/CreateBox"));
        if (displayCircle)
        {
            BackGroundImage.sprite = circleBackgroundSprite;
            BackGroundImage.SetNativeSize();
            BackGroundImage.transform.localScale = new Vector3(1, 1, 1);
            //BackGroundImage.transform.localScale = new Vector3(circleBackgroundSprite.)
        }
        priceText.text = "" + state.cost;
        gameObject.name = "PowerLabel " + state.uID;
        Debug.Log(state.type);
        if(powersDic.ContainsKey(state.type)){
            powerImage.sprite = powersDic[state.type];
        }
        
        if (displayCircle)
        {
            powerImage.SetNativeSize();
        }


        powerState = state;
    }
}
