using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFixRaycast : MonoBehaviour
{
    [SerializeField] private GameObject raycastOrigin;
    private float percentaje;

    private void Update()
    {
        if (Physics.Raycast(raycastOrigin.transform.position, raycastOrigin.transform.forward, out RaycastHit hit))
        {
            GameObject target = hit.collider.gameObject;
            if (target.tag == "Portal")
            {
                percentaje = target.GetComponent<PortalFix>().rayTargeted();
            }
        }
    }
}
