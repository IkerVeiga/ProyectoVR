using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableBetweenDimensions : MonoBehaviour
{
    private bool crossingPortal;

    // Start is called before the first frame update
    void Start()
    {
        crossingPortal = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Grabbing()
    {
        if(crossingPortal == true)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Portal")
        {
            crossingPortal = true;
        }
    }
}
