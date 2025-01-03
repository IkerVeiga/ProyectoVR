using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = transform.position - new Vector3(0, 0.01f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
