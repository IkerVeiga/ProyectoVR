using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captura : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ScreenCapture.CaptureScreenshot("Screenshot.png");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
