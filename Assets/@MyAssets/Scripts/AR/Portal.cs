using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Management;

public class Portal : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private bool isVisible = false;

    public int Index { get => index; set => index = value; }

    public void GoToOtherDimension()
    {
        PortalManager.Instance.GoToOtherDimension(index);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GoToOtherDimension();
        }
    }
}
