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
using UnityEngine.XR.Interaction.Toolkit;

public class PortalManager : MonoBehaviour
{

    private class PortalData
    {
        public PortalData(GameObject portal)
        {
            this.portal = portal;
            this.position = portal.transform.position;
            this.rotation = portal.transform.rotation;
            this.index = portal.GetComponent<Portal>().Index;
        }


        public Vector3 position;
        public Quaternion rotation;
        public int index;
        public GameObject portal;
    }

    public static PortalManager Instance;

    [SerializeField] private List<PortalData> ARportals = new List<PortalData>();
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
            DontDestroyOnLoad(gameObject); //Esto lo he cambiado desde que funcionaba el cambio de escena en las gafas
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(this);
        }
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
        XRGeneralSettings.Instance.Manager.StartSubsystems();

        if (scene.name == "AR")
        {
            SpawnExistingPortals();
            StartCoroutine(SpawnNewPortal());
            wallManager = FindObjectOfType<WallManager>();
        } else if (scene.name == "VR")
        {
            VRportals = FindObjectsOfType<Portal>()
                .OrderBy(p => p.Index)
                .Select(p => p.gameObject)
                .ToList();

            GameObject origin = FindObjectOfType<XROrigin>().gameObject;
            origin.transform.position = VRportals[traversedPortalIndex].transform.GetChild(0).position;
            ObjectManager.Instance.RepositionCrateObjectsVR(VRportals[traversedPortalIndex]);
        }
    }

    private void SpawnExistingPortals()
    {
        foreach (PortalData data in ARportals)
        {
            GameObject instance = Instantiate(portalPrefabs[0], data.position, data.rotation);
            GameObject realPortal = instance.transform.GetChild(1).gameObject;
            realPortal.SetActive(true);
            realPortal.GetComponent<Portal>().Index = data.index;
            instance.transform.GetChild(0).gameObject.SetActive(false);
            data.portal = realPortal;
        }
    }

    private IEnumerator SpawnNewPortal()
    {
        yield return new WaitForSeconds(3);
        Quaternion rotation = Quaternion.identity;
        Vector3 position = new Vector3(0, 0, ARportals.Count);

        try
        {
            ARPlane randomPlane = wallManager.Walls[UnityEngine.Random.Range(0, wallManager.Walls.Count)];
            float height = UnityEngine.Random.Range(-(randomPlane.extents.y - buffer), randomPlane.extents.y - buffer);

            rotation = Quaternion.FromToRotation(Vector3.up, randomPlane.normal);
            position = new Vector3(randomPlane.center.x, randomPlane.center.y + height, randomPlane.center.z);

            Vector3 rotatedNormal = Quaternion.Euler(0, 90, 0) * randomPlane.normal;
            position += rotatedNormal * UnityEngine.Random.Range(-(randomPlane.extents.x - buffer), randomPlane.extents.x - buffer);

            position += randomPlane.normal * offset;
        } catch (Exception e)
        {
            Debug.LogWarning(e.ToString());
        }
        
        GameObject instance = Instantiate(portalPrefabs[0], position, rotation);
        GameObject realPortal = instance.transform.GetChild(1).gameObject;
        realPortal.SetActive(true);
        realPortal.GetComponent<Portal>().Index = ARportals.Count;
        realPortal.SetActive(false);
        ARportals.Add(new PortalData(realPortal));
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
