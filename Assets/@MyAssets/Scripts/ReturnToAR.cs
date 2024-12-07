using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;

public class ReturnToAR : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMyDimension()
    {
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        string dataToKeep = this.gameObject.layer.ToString();
        StaticData.valueToKeep = dataToKeep;
        if (SceneManager.GetActiveScene().name == "AR")
        {
            SceneManager.LoadScene("VR", LoadSceneMode.Single);
        }

    }
}
