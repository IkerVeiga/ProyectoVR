using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class PortalManager : MonoBehaviour
{
    public static PortalManager instance { get; private set; }

    public List<GameObject> portals = new List<GameObject>();
    [SerializeField] private WallManager wallManager;

    [SerializeField] private TextMeshProUGUI debug;

    public List<GameObject> portalPrefabs;
    [SerializeField] private float offset;
    [SerializeField] private float buffer;

    private void Start()
    {
        Debug.Log(wallManager);
        StartCoroutine(SpawnCoroutine());
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Update()
    {
        debug.text = portals[0].ToString();
    }

    private IEnumerator SpawnCoroutine()
    {
        while (portals.Count <= 3)
        {
            yield return new WaitForSeconds(5);
            SpawnPortal();
        }
    }

    private void SpawnPortal()
    {
        ARPlane randomPlane = wallManager.Walls[Random.Range(0, wallManager.Walls.Count)];
        float height = Random.Range(-(randomPlane.extents.y - buffer), randomPlane.extents.y - buffer);

        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, randomPlane.normal);
        Vector3 position = new Vector3(randomPlane.center.x, randomPlane.center.y + height, randomPlane.center.z);

        Vector3 rotatedNormal = Quaternion.Euler(0, 90, 0) * randomPlane.normal;
        position += rotatedNormal * Random.Range(-(randomPlane.extents.x - buffer), randomPlane.extents.x - buffer);

        position += randomPlane.normal * offset;
        GameObject instance = Instantiate(portalPrefabs[0], position, rotation);
        //portals.Add(instance);
        Debug.Log("Portal spawned");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach(GameObject portal in portals)
        {
            Instantiate(portal, portal.transform.position, portal.transform.rotation);
        }
    }

}
