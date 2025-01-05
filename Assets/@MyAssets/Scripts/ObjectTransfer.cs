using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTransfer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<StoreObject>() != null)
        {
            //Debug.Log("Heyyy");
            ObjectManager.Instance.AddObjectToCrate(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<StoreObject>() != null || GetComponentInParent<StoreObject>() != null)
        {
            ObjectManager.Instance.RemoveObjectFromCrate(other.gameObject);
        }
    }
}
