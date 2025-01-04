using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGenerator : MonoBehaviour
{
    private void Start()
    {
        //GetComponent<Pipe>().ConnectToCircuit();
        //StartCoroutine(ReconectCoroutine());
    }

    private IEnumerator ReconectCoroutine()
    {
        while (true)
        {
            //yield return new WaitForSeconds(2);
            //GetComponent<Pipe>().Disconnect();
            yield return new WaitForSeconds(5);
            Debug.Log("Power on");
            GetComponent<Pipe>().ConnectToCircuit();
        }
    }

}
