using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < text.Length; i++)
        {
            this.GetComponent<TextMeshProUGUI>().text += text[i];
            yield return new WaitForSeconds(time);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(DisplayText());
    }

    private void OnDisable()
    {
        this.GetComponent<TextMeshProUGUI>().text = "";
    }
}
