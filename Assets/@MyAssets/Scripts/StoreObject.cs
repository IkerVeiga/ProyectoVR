using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreObject : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log(GetInstanceID());
        DontDestroyOnLoad(gameObject);
        //Debug.Log(ObjectManager.Instance);
        bool result = ObjectManager.Instance.RegisterObject(gameObject);
        if (result == false) Destroy(gameObject);
    }


}
