using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCameraOptimization : MonoBehaviour
{

    [SerializeField] private List<GameObject> mirrors;
    private Plane[] cameraFrustrumPlanes;

    // Update is called once per frame
    void Update()
    {
        cameraFrustrumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        mirrors.ForEach(mirror =>
        {
            if (GeometryUtility.TestPlanesAABB(cameraFrustrumPlanes, mirror.GetComponent<Renderer>().bounds))
            {
                mirror.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                mirror.transform.GetChild(0).gameObject.SetActive(false);
            }
        });
    }
}
