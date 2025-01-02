using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Pipe
{
    [SerializeField] private Gate gate;
    private bool actuated = false;
    [Header("Leaver Rotation")]
    [SerializeField] private float increment = 1f;
    [SerializeField] private float waitTime = 0.01f;
    [SerializeField] private float rotationAngle = 90f;
    [SerializeField] Transform leaverShaft;
    public void OpenGate()
    {
        if (actuated || !isConnected) return;
        gate.OpenGate();
        actuated = true;
        StartCoroutine(FlipLeaver());
    }

    private IEnumerator FlipLeaver()
    {
        Debug.Log(leaverShaft.localRotation.eulerAngles.z);
        for (int i = 0; i < rotationAngle / increment; i++)
        {
            Debug.Log(leaverShaft.localRotation.eulerAngles.z);
            leaverShaft.Rotate(-Vector3.forward, increment);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
