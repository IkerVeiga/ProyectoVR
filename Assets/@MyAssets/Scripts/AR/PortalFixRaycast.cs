using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalFixRaycast : MonoBehaviour
{
    [SerializeField] private GameObject raycastOrigin;
    [SerializeField] private Slider slider;

    private void Update()
    {
        if (Physics.Raycast(raycastOrigin.transform.position, raycastOrigin.transform.forward, out RaycastHit hit))
        {
            GameObject target = hit.collider.gameObject;
            if (target.tag == "Portal")
            {
                slider.gameObject.SetActive(true);
                PortalFix portalFix = target.GetComponent<PortalFix>();
                if (portalFix != null)
                {
                    slider.value = portalFix.rayTargeted();
                }
                else
                {
                    slider.gameObject.SetActive(false);
                }
            }
            else
            {
                slider.gameObject.SetActive(false);
            }
        }
        else
        {
            slider.gameObject.SetActive(false);
        }
    }
}