using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GematoriumGem : ConnectedObject
{
    public GematoriumGem()
    {
    }

    private void Start() {
        var target = this.gameObject.AddComponent<LookTarget>();
        target.setState(state);
    }
}