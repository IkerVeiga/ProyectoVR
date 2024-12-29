using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using Unity.XR.CoreUtils;
using System;
using UnityEngine.XR.Management;

public class PortalManager : MonoBehaviour
{
    public static PortalManager Instance;

    [SerializeField] private List<Transform> ARportals = new List<Transform>();
    [SerializeField] private List<GameObject> VRportals = new List<GameObject>();
    [SerializeField] private WallManager wallManager;

    public List<GameObject> portalPrefabs;
    [SerializeField] private float offset;
    [SerializeField] private float buffer;

    private int traversedPortalIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
        XRGeneralSettings.Instance.Manager.StartSubsystems();

        if (scene.name == "AR")
        {
            SpawnNewPortal();
        } else if (scene.name == "VR")
        {
            VRportals = FindObjectsOfType<Portal>()
                .OrderBy(p => p.Index)
                .Select(p => p.gameObject)
                .ToList();

            GameObject origin = FindObjectOfType<XROrigin>().gameObject;
            origin.transform.position = VRportals[traversedPortalIndex].transform.position;
        }
    }

    private void SpawnNewPortal()
    {
        Quaternion rotation = Quaternion.identity;
        Vector3 position = Vector3.zero;

        try
        {
            ARPlane randomPlane = wallManager.Walls[UnityEngine.Random.Range(0, wallManager.Walls.Count)];
            float height = UnityEngine.Random.Range(-(randomPlane.extents.y - buffer), randomPlane.extents.y - buffer);

            rotation = Quaternion.FromToRotation(Vector3.up, randomPlane.normal);
            position = new Vector3(randomPlane.center.x, randomPlane.center.y + height, randomPlane.center.z);

            Vector3 rotatedNormal = Quaternion.Euler(0, 90, 0) * randomPlane.normal;
            position += rotatedNormal * UnityEngine.Random.Range(-(randomPlane.extents.x - buffer), randomPlane.extents.x - buffer);

            position += randomPlane.normal * offset;
        } catch (NullReferenceException e)
        {
            Debug.LogWarning(e.ToString());
        }
        
        GameObject instance = Instantiate(portalPrefabs[0], position, rotation);
        GameObject realPortal = instance.transform.GetChild(1).gameObject;
        realPortal.SetActive(true);
        realPortal.GetComponent<Portal>().Index = ARportals.Count;
        realPortal.SetActive(false);
        ARportals.Add(instance.transform);
        Debug.Log("Portal spawned");
    }

    public void GoToOtherDimension(int portalIndex)
    {
        traversedPortalIndex = portalIndex;

        if (SceneManager.GetActiveScene().name == "AR")
        {
            SceneManager.LoadScene("Intermediate", LoadSceneMode.Single);
        }
        else if (SceneManager.GetActiveScene().name == "VR")
        {
            VRportals.Clear();
            SceneManager.LoadScene("AR", LoadSceneMode.Single);
        }
    }



}
