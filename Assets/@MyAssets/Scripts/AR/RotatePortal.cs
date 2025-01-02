using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePortal : MonoBehaviour
{
    [SerializeField] private float step;
    [SerializeField] private float delayTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        while (true)
        {
            transform.Rotate(Vector3.up, step);
            yield return new WaitForSeconds(delayTime);
        }
    }

    
}
