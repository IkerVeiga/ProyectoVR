using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDiaryText : MonoBehaviour
{
    public string text;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DisplayText()
    {
        for (int i = 0; i < text.Length; i++)
        {
            this.text += text[i];
            yield return new WaitForSeconds(time);
        }
    }

    public void Display()
    {
        StartCoroutine(DisplayText());
    }
}
