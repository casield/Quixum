using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacle
{
    void hello();
    void activate();
    GameObject gObject{get;set;}
}
