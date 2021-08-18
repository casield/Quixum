 #if UNITY_EDITOR
 using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameMapSwitch : MonoBehaviour
{
    [MenuItem("drokt.com/Go to Maps")]
    static void GoToMaps()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

        EditorSceneManager.OpenScene("Assets/Scenes/MapScene.unity");
    }

    [MenuItem("drokt.com/Go to Main")]
    static void GoToMain()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

        EditorSceneManager.OpenScene("Assets/Scenes/Main.unity");
    }

    [MenuItem("drokt.com/Go to UIScene")]
    static void GoToSceneUI()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

        EditorSceneManager.OpenScene("Assets/Scenes/UIScene.unity");
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
#endif
