using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;

public class PortalGrab : MonoBehaviour
{
    public void PortalGrabSceneChange()
    {
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();

        if (SceneManager.GetActiveScene().name == "AR")
        {
            SceneManager.LoadScene("VR", LoadSceneMode.Single);
        }
    }
}
