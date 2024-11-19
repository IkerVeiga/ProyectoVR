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
<<<<<<< Updated upstream
    [SerializeField] private InputActionAsset actionAsset;
    //[SerializeField] private ARSession arSession;
    //[SerializeField] private XROrigin xrOriginAR;
    //private GameObject[] arObjects;
    // Start is called before the first frame update
    void Start()
=======
    public ChangeScene Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            Instance = this;
            StartCoroutine(changeScene());
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
>>>>>>> Stashed changes
    {
        //arObjects = GameObject.FindGameObjectsWithTag("AR");
        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
        XRGeneralSettings.Instance.Manager.StartSubsystems();
    }


    public void changeVRAR()
    {
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();

        if (SceneManager.GetActiveScene().name == "AR")
        {
            SceneManager.LoadScene("VR", LoadSceneMode.Single);

        } else if (SceneManager.GetActiveScene().name == "VR")
        {
            SceneManager.LoadScene("AR", LoadSceneMode.Single);
        }
    }

    IEnumerator changeScene()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            changeVRAR();
        }

    }
}

