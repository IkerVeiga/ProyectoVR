using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameProgression : MonoBehaviour
{
    [SerializeField] private GameObject portalMaze;
    [SerializeField] private GameObject portalHouse;

    PortalManager portalManager;
    // Start is called before the first frame update
    void Start()
    {
        portalManager = GetComponent<PortalManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PortalGrab.contadorPortales == 2)
        {
            portalManager.portalPrefabs.Add(portalMaze);
        } 
        else if(PortalGrab.contadorPortales == 3)
        {

            portalManager.portalPrefabs.Add(portalHouse);
        }
    }
}
