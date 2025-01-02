using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private Collider collider;
    [SerializeField] private GameObject leftGate;
    [SerializeField] private GameObject rightGate;
    [SerializeField] private GameObject rotationPointLeft;
    [SerializeField] private GameObject rotationPointRight;
    [SerializeField] private float increment = 1f;
    [SerializeField] private float waitTime = 0.01f;


    public void OpenGate()
    {
        collider.enabled = false;
        StartCoroutine(OpenCoroutine());
    }

    private IEnumerator OpenCoroutine()
    {
        while (leftGate.transform.localRotation.eulerAngles.y < 90)
        {
            leftGate.transform.RotateAround(rotationPointLeft.transform.position, Vector3.up, increment);
            rightGate.transform.RotateAround(rotationPointRight.transform.position, Vector3.up, -increment);
            yield return new WaitForSeconds(waitTime);
        }
    }


}
