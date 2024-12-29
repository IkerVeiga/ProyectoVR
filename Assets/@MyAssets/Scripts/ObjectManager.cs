using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AYellowpaper.SerializedCollections;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance;

    [SerializedDictionary("Name", "GameObject")] public SerializedDictionary<string, GameObject> VRObjects = new SerializedDictionary<string, GameObject>();
    [SerializedDictionary("Name", "GameObject")] public SerializedDictionary<string, GameObject> ARObjects = new SerializedDictionary<string, GameObject>();

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

}
