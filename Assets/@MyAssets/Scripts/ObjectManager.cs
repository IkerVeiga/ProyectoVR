using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AYellowpaper.SerializedCollections;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance;

    [SerializedDictionary("Name", "GameObject")] public SerializedDictionary<string, GameObject> VRObjects = new SerializedDictionary<string, GameObject>();
    [SerializedDictionary("Name", "GameObject")] public SerializedDictionary<string, GameObject> ARObjects = new SerializedDictionary<string, GameObject>();
    [SerializeField] List<GameObject> ObjectsInCrate = new List<GameObject>();

    private void Awake()
    {
        Debug.Log("Holadsfas");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        if (scene.name == "AR")
        {
            foreach (var obj in ARObjects.Values)
            {
                obj.SetActive(true);
            }
            foreach (var obj in VRObjects.Values)
            {
                obj.SetActive(false);
            }
            RepositionCrateObjectsAR();
        }
        else if (scene.name == "VR")
        {
            foreach (var obj in ARObjects.Values)
            {
                obj.SetActive(false);
            }
            foreach (var obj in VRObjects.Values)
            {
                obj.SetActive(true);
            }
        }
        
    }

    public bool RegisterObject(GameObject gameObject)
    {
        if (SceneManager.GetActiveScene().name == "AR")
        {
            return ARObjects.TryAdd(gameObject.name, gameObject);
        }
        else if (SceneManager.GetActiveScene().name == "VR")
        {
            return VRObjects.TryAdd(gameObject.name, gameObject);
        }
        return false;
    }

    public void AddObjectToCrate(GameObject gameObject)
    {
        ObjectsInCrate.Add(gameObject);
        ARObjects.Remove(gameObject.name);
        VRObjects.Remove(gameObject.name);
    }

    public void RemoveObjectFromCrate(GameObject gameObject)
    {
        ObjectsInCrate.Remove(gameObject);
        if (SceneManager.GetActiveScene().name == "AR")
        {
            ARObjects.Add(gameObject.name, gameObject);
        }
        else if (SceneManager.GetActiveScene().name == "VR")
        {
            VRObjects.Add(gameObject.name, gameObject);
        }
    }

    private void RepositionCrateObjectsAR()
    {
        GameObject crate = FindObjectOfType<ObjectTransfer>().gameObject;
        foreach (var obj in ObjectsInCrate)
        {
            obj.transform.position = crate.transform.position;
            obj.GetComponent<XRGrabInteractable>().enabled = false;
            obj.GetComponent<XRGrabInteractable>().enabled = true;
        }
    }

    public void RepositionCrateObjectsVR(GameObject portal) //Esto duele muchoo. (Lo llama PortalManager)
    {
        Transform crate = portal.transform.GetChild(3);
        foreach (var obj in ObjectsInCrate)
        {
            obj.transform.position = crate.position;
            obj.GetComponent<XRGrabInteractable>().enabled = false;
            obj.GetComponent<XRGrabInteractable>().enabled = true;
        }
    }







}
