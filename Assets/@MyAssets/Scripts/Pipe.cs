using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private List<Pipe> connectedPipes = new List<Pipe>();
    private bool isConnected = false;

    public List<Pipe> ConnectedPipes { get => connectedPipes; }

    private void Start()
    {
        connectedPipes.ForEach(pipe =>
        {
            if (!pipe.connectedPipes.Contains(this))
            {
                pipe.connectedPipes.Add(this);
            }
        });
    }

    public void Disconnect()
    {
        if (!isConnected) return;
        isConnected = false;
        gameObject.GetComponent<Renderer>().material.color = Color.white;
        connectedPipes.ForEach(pipe => pipe.Disconnect());
    }

    public void ConnectToCircuit()
    {
        if (isConnected) return;
        isConnected = true;
        if (gameObject.transform.childCount == 0)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else //Igual luego quitar este if
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Renderer renderer = gameObject.transform.GetChild(i).GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.red;
                }
            }
        }
        
        connectedPipes.ForEach(pipe => pipe.ConnectToCircuit());
    }
}
