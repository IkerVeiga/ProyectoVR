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
    //[SerializeField] private ARSession arSession;
    //[SerializeField] private XROrigin xrOriginAR;
    //private GameObject[] arObjects;
    // Start is called before the first frame update
    void Start()
    {
        //arObjects = GameObject.FindGameObjectsWithTag("AR");
        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
        XRGeneralSettings.Instance.Manager.StartSubsystems();
    }


    public void changeVRAR(InputAction.CallbackContext context)
    {
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();

        if (SceneManager.GetActiveScene().name == "AR")
        {
            SceneManager.LoadScene("AR2", LoadSceneMode.Single);

        } else if (SceneManager.GetActiveScene().name == "VR")
        {
            SceneManager.LoadScene("AR", LoadSceneMode.Single);
        }
    }
}

