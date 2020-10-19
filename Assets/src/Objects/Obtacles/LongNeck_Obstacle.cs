using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNeck_Obstacle: MonoBehaviour,IObstacle
{
    private bool hasInit =false;

    public ObstacleState State { get;set; }
    public GameObject gObject { get; set ; }

    public Animator animator;

    public /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        gObject = this.gameObject;
    }


    public void activate(){
        if(animator != null){
            animator.SetBool("startAction",true);
        }
        
        
    }

    public void stopAnimation(){
        animator.SetBool("startAction",false);
    }

    public void hello(){
    }

    // Update is called once per frame
    void Update()
    {
    }
}
