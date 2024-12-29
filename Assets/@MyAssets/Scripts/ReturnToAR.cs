using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;

public class ReturnToAR : MonoBehaviour
{

    public void GoToMyDimension()
    {
        SceneManager.LoadScene("AR");
    }
}
