using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesController : MonoBehaviour
{
    // Start is called before the first frame update
    public static BubblesController Instance;
    private void Awake() {
        Instance = this;
    }
    void Start()
    {
        
    }
    public void createBubble(string text,GameObject gameObject, float time){
        GameObject gm = GameObject.Instantiate(Resources.Load<GameObject>("UI/TextBubble"));
        TextBubble tbubble = gm.GetComponent<TextBubble>();
        tbubble.setFindObject(gameObject);
        tbubble.changeText(text);
        gm.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
