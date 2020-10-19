using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfClub : MonoBehaviour
{
    // Start is called before the first frame update

    private float force = 0;
    private float forceSpeed = 8.2f;

    private bool adding = true;
    void Start()
    {
        
    }
     void shootingAnimationEnded(){
        if(!Input.GetMouseButton(0) && Character.Instance.isShotting){

          //  Debug.Log(Character.Instance.GolfClubController.GetCurrentAnimatorStateInfo(0).);
            
            Character.Instance.sendShot(force);
            Character.Instance.isShotting = false;
            force = 0;
        }
    }

    void reachedMax(){
        adding = false;
    }
    void reachedMin(){
        adding = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0)){
            Character.Instance.AnimationController.SetBool("isShotting",false);
        }

        if(Input.GetMouseButton(0)){
            if(adding){
                force+=forceSpeed;
            }else{
                force-=forceSpeed;
            }
            
        }
    }
}
