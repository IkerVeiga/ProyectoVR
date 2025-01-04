using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeConfiguration : MonoBehaviour
{
    [SerializeField] private Material electricity;
    [SerializeField] private Material energy;
    [SerializeField] private Material transparentPipe;

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> gos = new List<GameObject>();
        foreach (Pipe pipe in FindObjectsOfType<Pipe>())
        {
            gos.Add(pipe.gameObject);
        }
        gos.ForEach(go =>
        {
            try
            {
                GameObject child2 = go.transform.GetChild(2).gameObject;

                child2.SetActive(false);
                child2.GetComponent<Renderer>().material = energy;

                GameObject child1 = go.transform.GetChild(1).gameObject;

                child1.GetComponent<Renderer>().material = transparentPipe;

                go.GetComponent<Pipe>().Electricity = electricity;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Unexpected structure: " + e.Message);
            }


        });
    }

}