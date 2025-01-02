using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Management;

public class Portal : MonoBehaviour
{
    [SerializeField] private int index;

    private AudioSource audioSource;

    public int Index { get => index; set => index = value; }

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void GoToOtherDimension()
    {
        audioSource.Play();
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
