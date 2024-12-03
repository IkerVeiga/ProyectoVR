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
    private GameObject pipeGO;
    private int steps;
    private bool flipy;
    private bool flipz;

    private void Update()
    {
        if (pipeGO == null) return;
        flipy = false;
        flipz = false;
        //if (pipeGO.transform.localRotation.eulerAngles.x > 45 && pipeGO.transform.localRotation.eulerAngles.x < 135)
        //{
        //    steps = 1;
        //    transform.rotation = Quaternion.Euler(90, 0, 0);
        //}
        //else if (pipeGO.transform.localRotation.eulerAngles.x > 135 && pipeGO.transform.localRotation.eulerAngles.x < 225)
        //{
        //    steps = 2;
        //    transform.rotation = Quaternion.Euler(180, 0, 0);
        //}
        //else if (transform.localRotation.eulerAngles.x > 225 && pipeGO.transform.localRotation.eulerAngles.x < 315)
        //{
        //    steps = 3;
        //    transform.rotation = Quaternion.Euler(270, 0, 0);
        //}

        transform.rotation = Quaternion.Euler(SnapRotation(transform.localRotation.eulerAngles.x),0, 0);

        //if (pipeGO.transform.localRotation.eulerAngles.y > 90 && pipeGO.transform.localRotation.eulerAngles.y < 270)
        //{
        //    flipy = true;
        //    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);
        //}
        //if (pipeGO.transform.localRotation.eulerAngles.z > 90 && pipeGO.transform.localRotation.eulerAngles.z < 270)
        //{
        //    flipz = true;
        //    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 180);
        //}
    }

    public void OnHoverEnter(HoverEnterEventArgs args)
    {
        pipeGO = args.interactableObject.transform.gameObject;
    }

    public void OnHoverExit(HoverExitEventArgs args)
    {
        pipeGO = null;
    }

    public void PlacePipe(SelectEnterEventArgs args)
    {
        Debug.Log(pipeGO);
        GrabablePipe pipe = pipeGO.GetComponent<GrabablePipe>();

        pipe.RotateConnections(steps, flipy, flipz);
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
