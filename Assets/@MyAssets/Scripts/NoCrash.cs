using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;

public class NoCrash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        SceneManager.LoadScene("VR", LoadSceneMode.Single);
    }

}
