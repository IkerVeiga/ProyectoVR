using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Management;

public class PortalGrab : MonoBehaviour
{
    public static int contadorPortales;
    private ARSession arSession;
    
    public void CrossPortal()
    {
        StartCoroutine(GoToOtherDimension());
    }

    IEnumerator GoToOtherDimension()
    {
        yield return new WaitForSeconds(0.5f);
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        string dataToKeep = this.gameObject.layer.ToString();
        StaticData.valueToKeep = dataToKeep;
        contadorPortales++;
        if (SceneManager.GetActiveScene().name == "AR")
        {
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene("VR", LoadSceneMode.Single);
        }
        
    }
}
