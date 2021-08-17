using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UISceneManager Instance;
    public Camera toHideCamera;

    void Awake()
    {
        Instance = this;
        toHideCamera.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
