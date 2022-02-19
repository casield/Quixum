using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GUIController : MonoBehaviour
{
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>

    public Transform canvas;
    private bool closeOpen = false;
    Dictionary<Transform, bool> childStatusMap = new Dictionary<Transform, bool>();
    void Awake()
    {

    }
    void Start()
    {
        WakeUpAllChild();
    }

    void WakeUpAllChild()
    {
        for (int a = 0; a < canvas.childCount; a++)
        {
            childStatusMap.Add(canvas.GetChild(a), canvas.GetChild(a).gameObject.activeSelf);
            canvas.GetChild(a).gameObject.SetActive(true);
        }
        closeOpen = true;
    }
    void RestartChildStatus()
    {
        if (closeOpen)
        {
            foreach (KeyValuePair<Transform, bool> item in childStatusMap)
            {
                item.Key.gameObject.SetActive(item.Value);
            }
            closeOpen = false;
        }
    }
    void Update()
    {
        RestartChildStatus();
    }

}
