using System;
using System.Collections;
using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine.UIElements;

public class GemsLabel : Label
{
    // Start is called before the first frame update
    public new class UxmlFactory : UxmlFactory<GemsLabel, UxmlTraits> { };
    public new class UxmlTraits : Label.UxmlTraits { };
    public GemsLabel()
    {
        this.text = "" + 0000000;
        this.RegisterCallback<GeometryChangedEvent>(GeometryChange);
    }

    private void GeometryChange(GeometryChangedEvent evt)
    {
        Client.Instance.OnReadyListener+=Init;
        
        this.UnregisterCallback<GeometryChangedEvent>(GeometryChange);
    }

    private void Init()
    {
        Client.Instance.user.userState.OnChange += OnUserChange;
        Client.Instance.OnReadyListener-=Init;

    }

    private void OnUserChange(List<DataChange> changes)
    {
        foreach (var ch in changes)
        {
            if (ch.Field == "gems")
            {
                this.text = "" + ch.Value;
            }
        }
    }
}
