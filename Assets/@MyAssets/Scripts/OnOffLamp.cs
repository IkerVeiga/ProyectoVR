using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffLamp : MonoBehaviour
{
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightsOnLightsOff()
    {
        counter++;
        if (counter % 2 != 0)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
