using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Colyseus;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

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

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        for (int a = 0; a < canvas.childCount; a++)
        {
            childStatusMap.Add(canvas.GetChild(a), canvas.GetChild(a).gameObject.activeSelf);
            canvas.GetChild(a).gameObject.SetActive(true);
        }
        closeOpen = true;

    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
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

}
