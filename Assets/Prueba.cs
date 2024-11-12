using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Prueba : MonoBehaviour
{
    public ARPlaneManager planeMananger;
    public GameObject gameObject;
    
    // Start is called before the first frame update
    void Start()
    {
        planeMananger.planesChanged += Pruebita;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void Pruebita(ARPlanesChangedEventArgs args)
    {
        args.added.ForEach((plane) => Instantiate(gameObject, plane.center, Quaternion.identity));
    }
}
