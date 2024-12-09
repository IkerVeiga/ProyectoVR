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
    public ChangeScene Instance;
    private ARSession arSession;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
        XRGeneralSettings.Instance.Manager.StartSubsystems();
    }

    


    //public void changeVRAR(InputAction.CallbackContext context)
    //{
    //    XRGeneralSettings.Instance.Manager.StopSubsystems();
    //    XRGeneralSettings.Instance.Manager.DeinitializeLoader();

    //    if (SceneManager.GetActiveScene().name == "AR")
    //    {
    //        SceneManager.LoadScene("VR", LoadSceneMode.Single);

    //    } else if (SceneManager.GetActiveScene().name == "VR")
    //    {
    //        SceneManager.LoadScene("AR", LoadSceneMode.Single);
    //    }
    //}
}

