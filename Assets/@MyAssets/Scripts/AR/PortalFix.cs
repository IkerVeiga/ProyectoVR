using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFix : MonoBehaviour
{
    [SerializeField] private GameObject realPortal;
    [SerializeField] private float focusTime;
    private float timer;
    public bool raycasted = false;

    public Transform portalTransform;

    

    public float rayTargeted()
    {
        Debug.Log("Raycasted");
        timer += Time.deltaTime;
        if (timer >= focusTime)
        {
            realPortal.SetActive(true);
            gameObject.SetActive(false);
            raycasted = true;
        }
        return timer / focusTime;
    }
}

