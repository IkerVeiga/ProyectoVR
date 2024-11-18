using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using System.Collections;

public class PortalManager : MonoBehaviour
{
    private List<GameObject> portals = new List<GameObject>();
    [SerializeField] private WallManager wallManager;

    [SerializeField] private GameObject portalPrefab;
    [SerializeField] private float offset;
    [SerializeField] private float buffer;

    private void Start()
    {
        Debug.Log(wallManager);
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while(true)
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
        GameObject instance = Instantiate(portalPrefab, position, rotation);
        portals.Add(instance);
        Debug.Log("Portal spawned");
    }


}
