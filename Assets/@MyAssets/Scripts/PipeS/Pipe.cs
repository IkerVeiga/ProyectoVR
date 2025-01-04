using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private List<Pipe> connectedPipes = new List<Pipe>();
    [SerializeField] private Material electricity;
    protected bool isConnected;

    public List<Pipe> ConnectedPipes { get => connectedPipes; }
    public Material Electricity { get => electricity; set => electricity = value; }

    private event Action connectEvent;
    private event Action disconnectEvent; //Sí, me he pasado, lo siento

    private void Start()
    {
        connectedPipes.ForEach(pipe =>
        {
            if (!pipe.connectedPipes.Contains(this))
            {
                pipe.connectedPipes.Add(this);
            }
        });

        EventConfiguration();

    }

    protected virtual void EventConfiguration()
    {
        connectEvent += ShowEnergy;
        disconnectEvent += HideEnergy;
    }

    public void Disconnect()
    {
        if (!isConnected) return;
        isConnected = false;
        disconnectEvent?.Invoke();
        connectedPipes.ForEach(pipe => pipe.Disconnect());
    }

    public void ConnectToCircuit()
    {
        if (isConnected) return;
        isConnected = true;
        connectEvent?.Invoke();
        connectedPipes.ForEach(pipe => pipe.ConnectToCircuit());
    }

    private void ShowEnergy()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        Renderer renderer = transform.GetChild(1).gameObject.GetComponent<Renderer>();
        Material[] materials = new Material[] { renderer.material, electricity };
        renderer.materials = materials;
    }

    private void HideEnergy()
    {
        transform.GetChild(2).gameObject.SetActive(false);
        Renderer renderer = transform.GetChild(1).gameObject.GetComponent<Renderer>();
        Material[] materials = new Material[] { renderer.material };
        renderer.materials = materials;
    }

    private void ChangeMaterialConnect()
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.red;
        }
        else
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                renderer = gameObject.transform.GetChild(i).GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.red;
                }
            }
        }
    }

    private void ChangeMaterialDisconnect()
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.white;
        }
        else //Igual luego quitar este if
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                renderer = gameObject.transform.GetChild(i).GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.white;
                }
            }
        }
    }
}
