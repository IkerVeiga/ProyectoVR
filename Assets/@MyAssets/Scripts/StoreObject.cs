using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreObject : MonoBehaviour
{
    private bool grabbing = false;

    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log(GetInstanceID());
        DontDestroyOnLoad(gameObject);
        Debug.Log(ObjectManager.Instance);
        bool result = ObjectManager.Instance.RegisterObject(gameObject);
        if (result == false) Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Portal" && grabbing == true)
        {
            ObjectManager.Instance.ChangeObject(this.gameObject);
        }
    }

    public void Grab()
    {
        grabbing = true;
    }

    public void Release()
    {
        grabbing = false;
    }

}
