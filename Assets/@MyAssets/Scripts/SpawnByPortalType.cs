using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnByPortalType : MonoBehaviour
{
    private string portal;

    [SerializeField] private Transform villageSpawn;
    [SerializeField] private Transform mazeSpawn;
    [SerializeField] private Transform houseSpawn;
    [SerializeField] private Transform xrOrigin;
    [SerializeField] private GameObject debugPanel;

    // Start is called before the first frame update
    void Start()
    {

        portal = StaticData.valueToKeep;
        debugPanel.GetComponentInChildren<TextMeshProUGUI>().text = portal;
        if (portal == "6")
        {
            xrOrigin.position = villageSpawn.position;
        }
        else if (portal == "7")
        {
            xrOrigin.position = mazeSpawn.position;
        }
        else
        {
            xrOrigin.position = houseSpawn.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
