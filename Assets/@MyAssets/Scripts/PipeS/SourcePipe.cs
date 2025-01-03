using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourcePipe : Pipe
{
    public bool isOn { get; set; }

    public void ConnectAsSource()
    {
        DisconnectAsSource();
        Debug.Log("Connection");
        isOn = true;
        ConnectToCircuit();
    }

    public void DisconnectAsSource()
    {
        isOn = false;
        Disconnect();
    }

    public void Toggle()
    {
        if (isOn)
        {
            DisconnectAsSource();
        }
        else
        {
            ConnectAsSource();
        }
    }

    protected override void EventConfiguration() { }
}
