using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PipeSocket : MonoBehaviour
{
    [SerializedDictionary("Connection", "Pipe")] public SerializedDictionary<Connections, Pipe> connections;

    public void PlacePipe(SelectEnterEventArgs args)
    {
        GameObject pipeGO = args.interactableObject.transform.gameObject;
        Debug.Log(pipeGO);
        GrabablePipe pipe = pipeGO.GetComponent<GrabablePipe>();
        pipe.RotateConnections(0);
        pipe.RotatedConnections.ForEach(c =>
        {
            if (connections.ContainsKey(c))
            {
                pipe.ConnectedPipes.Add(connections[c]);
                connections[c].ConnectedPipes.Add(pipe);
            }
        });
    }

    public void RemovePipe(SelectExitEventArgs args)
    {
        GameObject pipeGO = args.interactableObject.transform.gameObject;
        GrabablePipe pipe = pipeGO.GetComponent<GrabablePipe>();
        pipe.Disconnect();
        foreach(Pipe p in connections.Values)
        {
            p.ConnectedPipes.Remove(pipe);
        }
        pipe.ConnectedPipes.Clear();
    }
}
