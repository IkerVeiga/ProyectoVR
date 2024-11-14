using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

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
            SceneManager.LoadScene("VR");

        }
        if(SceneManager.GetActiveScene().name == "VR")
        {
            SceneManager.LoadScene("AR");
            foreach (GameObject o in arObjects)
            {
                o.SetActive(true);
            }
        }
    }
}

