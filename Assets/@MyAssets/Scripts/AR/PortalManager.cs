using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using System.Collections;

public class PortalManager : MonoBehaviour
{

    private List<GameObject> portals = new List<GameObject>();
    [SerializeField] private WallManager wallManager;

    public List<GameObject> portalPrefabs;
    [SerializeField] private float offset;
    [SerializeField] private float buffer;

    private void Start()
    {
        Debug.Log(wallManager);
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(5);
            int randomPortal = Random.Range(0, portalPrefabs.Count - 1);
            SpawnPortal(randomPortal);
        }
    }

    private void SpawnPortal(int portalIndex)
    {
        ARPlane randomPlane = wallManager.Walls[Random.Range(0, wallManager.Walls.Count)];
        float height = Random.Range(-(randomPlane.extents.y - buffer), randomPlane.extents.y - buffer);

        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, randomPlane.normal);
        Vector3 position = new Vector3(randomPlane.center.x, randomPlane.center.y + height, randomPlane.center.z);

        Vector3 rotatedNormal = Quaternion.Euler(0, 90, 0) * randomPlane.normal;
        position += rotatedNormal * Random.Range(-(randomPlane.extents.x - buffer), randomPlane.extents.x - buffer);

        position += randomPlane.normal * offset;
        GameObject instance = Instantiate(portalPrefabs[portalIndex], position, rotation);
        portals.Add(instance);
        Debug.Log("Portal spawned");
    }


}
