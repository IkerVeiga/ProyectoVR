using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Management;

public class PortalGrab : MonoBehaviour
{
    private ARSession arSession;

    public void GoToOtherDimension()
    {
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        if (SceneManager.GetActiveScene().name == "AR")
        {
            SceneManager.LoadScene("VR", LoadSceneMode.Single);
        }

    }
}
