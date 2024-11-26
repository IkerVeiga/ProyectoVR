using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFix : MonoBehaviour
{
    [SerializeField] private float focusTime;
    [SerializeField] private GameObject realPortal;

    private Renderer theRenderer;
    private float timer;

    void Start()
    {
        theRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (theRenderer.isVisible == true)
        {
            timer += Time.deltaTime;
        }
        if (timer >= focusTime)
        {
            realPortal.SetActive(true);
        }
    }
}
