using System;
using System.Collections;
using UnityEngine;

public class Player2 : MonoBehaviour, ConnectedObject
{
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    BallPointer ballPointer;
    Animator animator;
    private DrawLineMessage json;
    private Color color;
    public ObjectState state;
    public SObject sObject;

    void Start()
    {
        
        animator = GetComponent<Animator>();
        sObject = GetComponent<SObject>();
        sObject.refreshTime = 1f;
        setNormalSize(); 
    }

    void setNormalSize()
    {
        this.transform.localScale = new Vector3(2, 2, 2);
    }
    public void onMessage(ObjectMessage m)
    {
        if (animator != null)
        {
                
            if (m.message == "trigger_ShootAnim")
            {

                animator.SetTrigger("Shooting");
                Character.Instance.canRotate = true;
            }
            if (m.message == "Snap_true")
            {
                animator.SetBool("Snapped", true);
            }
            if (m.message == "Snap_false")
            {

                animator.SetBool("Snapped", false);
            }
            if (m.message == "Trigger_Hello")
            {
                Debug.Log("Sey hello");
                animator.SetTrigger("Hello");
            }
            if (m.message.Contains("draw_line"))
            {
                string[] s = m.message.Split('@');
                json = JsonUtility.FromJson<DrawLineMessage>(s[1]);
                Debug.Log(json.x1);

               color = new Color(1.0f, 1f, 1.0f);
              
            }
            //Debug.Log(m.message);
        }
    }
    private void FixedUpdate() {
        if(json != null){
             Debug.DrawLine(new Vector3(json.x1,gameObject.transform.position.y,json.y1), new Vector3(json.x2,gameObject.transform.position.y,json.y2), color);
        }
         
    }
    public void setState(ObjectState state)
    {

        this.state = state;
        if (state.owner == Client.Instance.room.SessionId)
        {
            Character.Instance.ballPointer = ballPointer;
            Character.Instance.player = this;
            CameraController.Instance.player = this;
        }
        else
        {
            ballPointer = GetComponentInChildren<BallPointer>();
            Destroy(ballPointer.gameObject);
        }
    }

    public void sendMessageToRoom(string m)
    {
        throw new System.NotImplementedException();
    }
}

public class DrawLineMessage
{
    public float x1, x2, y1, y2;
}