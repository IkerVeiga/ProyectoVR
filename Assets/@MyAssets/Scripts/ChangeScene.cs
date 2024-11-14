using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Management;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private ARSession arSession;
    [SerializeField] private XROrigin xrOriginAR;
    private GameObject[] arObjects;
    // Start is called before the first frame update
    void Start()
    {
        arObjects = GameObject.FindGameObjectsWithTag("AR");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        actionAsset.FindAction("ChangeScene").performed += changeVRAR;
    }


    public void changeVRAR(InputAction.CallbackContext context)
    {
        if(SceneManager.GetActiveScene().name == "AR")
        {
            foreach(GameObject o in arObjects)
            {
                o.SetActive(false);
            }
            XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
            if (XRGeneralSettings.Instance.Manager.activeLoader != null)
            {
                XRGeneralSettings.Instance.Manager.StartSubsystems();
            }
            SceneManager.LoadScene("VR");


        }
        if(SceneManager.GetActiveScene().name == "VR")
        {
            if (XRGeneralSettings.Instance.Manager.activeLoader != null)
            {
                XRGeneralSettings.Instance.Manager.StopSubsystems();
                XRGeneralSettings.Instance.Manager.DeinitializeLoader();
            }
            SceneManager.LoadScene("AR");
            foreach (GameObject o in arObjects)
            {
                o.SetActive(true);
            }
        }
    }
}

