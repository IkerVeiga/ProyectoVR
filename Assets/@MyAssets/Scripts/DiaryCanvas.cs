using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class DiaryCanvas : MonoBehaviour
{
    public Transform camera;

    // Start is called before the first frame update
    void Start()
    {
        if (camera == null) camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        FollowRotation();
    }

    public void FollowRotation()
    {
        this.transform.LookAt(camera);
    }
}
