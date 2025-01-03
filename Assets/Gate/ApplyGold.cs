using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyGold : MonoBehaviour
{
    public Material gold;
    // Start is called before the first frame update
    void Start()
    {
        Goldify(gameObject);
    }

    void Goldify(GameObject go)
    {
        if (go.GetComponent<Renderer>() != null)
        {
            go.GetComponent<Renderer>().material = gold;
        }
        else
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Goldify(go.transform.GetChild(i).gameObject);
            }
        }
    }
}
