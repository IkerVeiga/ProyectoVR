using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : Pipe
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material onMaterial;
    [SerializeField] private GameObject camera;

    protected override void EventConfiguration()
    {
        connectEvent += TurnOn;
        disconnectEvent += TurnOff;
    }

    public void TurnOn()
    {
        transform.GetChild(1).GetComponent<Renderer>().material = onMaterial;
        camera.SetActive(true);
    }

    public void TurnOff()
    {
        transform.GetChild(1).GetComponent<Renderer>().material = defaultMaterial;
        camera.SetActive(false);
    }

}
