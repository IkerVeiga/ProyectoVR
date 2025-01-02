using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MirrorCameraOptimization : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "MainCamera")
        {
            Debug.Log("Camera Entered");
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
