using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>

    public Client client;
    private bool hasInit = false;
    void Start()
    {
     client = Client.Instance;   
    }
    public void Init(){

    }

    public bool canRun(){
        return hasInit;
    }

    public void DUpdate(){

    }
    void Update()
    {
        if(canRun()){
            DUpdate();
        }else{
            client = Client.Instance;
            if(client.room != null && !hasInit){
                hasInit = true;
                Init();
            }
        }
        
    }
}
