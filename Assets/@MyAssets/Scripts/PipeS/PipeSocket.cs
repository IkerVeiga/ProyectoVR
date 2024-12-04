using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PipeSocket : MonoBehaviour
{
    [SerializedDictionary("Connection", "Pipe")] public SerializedDictionary<Connections, Pipe> connections;
    [Header("Rotation")]
    [SerializeField] private int increment = 1;
    [SerializeField] private float waitTime = 0.01f;
    [SerializeField] SourcePipe source;
    private GrabablePipe pipe;
    private int steps;
    private bool isRotating = false;

    public void Rotate()
    {
        if (isRotating) return;
        isRotating = true;
        StartCoroutine(RotateCoroutine());

    }

    private IEnumerator RotateCoroutine()
    {
        pipe.Disconnect();
        foreach (Pipe p in connections.Values)
        {
            p.ConnectedPipes.Remove(pipe);
        }
        pipe.ConnectedPipes.Clear();
        for (int i = 0; i < 90; i+= increment)
        {
            transform.Rotate(1, 0, 0);
            yield return new WaitForSeconds(waitTime);
        }
        steps = (steps + 1) % 4;
        pipe.RotateConnections(steps, false, false);
        pipe.RotatedConnections.ForEach(c =>
        {
            if (connections.ContainsKey(c))
            {
                pipe.ConnectedPipes.Add(connections[c]);
                connections[c].ConnectedPipes.Add(pipe);
            }
        });
        isRotating = false;
        if (source != null && source.isOn)
        {   
            source.ConnectAsSource();
        }

    }

    public void PlacePipe(SelectEnterEventArgs args)
    {
        pipe = args.interactableObject.transform.gameObject.GetComponent<GrabablePipe>();

        pipe.RotateConnections(steps, false, false);
        pipe.RotatedConnections.ForEach(c =>
        {
            if (connections.ContainsKey(c))
            {
                pipe.ConnectedPipes.Add(connections[c]);
                connections[c].ConnectedPipes.Add(pipe);
            }
        });
        if (source != null && source.isOn)
        {
            source.ConnectAsSource();
        }
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
        if (source.isOn)
        {
            source.ConnectAsSource();
        }
    }

    public int SnapRotation(float rotation)
    {
        // Calculate the remainder when dividing the rotation by 360
        float remainder = rotation % 360;

        // Snap the remainder to the nearest 90 degree step
        int snappedRotation = (int)(Mathf.Round((float)remainder / 90) * 90);

        // Handle negative values
        if (snappedRotation < -270)
            snappedRotation += 360;

        return snappedRotation;
    }
}
