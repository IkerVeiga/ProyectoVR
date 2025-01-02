using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Console : MonoBehaviour
{
    [SerializeField] private List<string> textCarrousel;
    [SerializeField] private int index;
    [SerializeField] private float timeBetweenCharacters;
    [SerializeField] private TextMeshProUGUI tmp;
    [Header("Botón")]
    [SerializeField] private GameObject button;
    [SerializeField] private float blinkTime;
    [SerializeField] Animation buttonAnimation;
    [SerializeField] XRSimpleInteractable buttonInteractable;
    private bool blinkButton = true;
    void Start()
    {
        index = 0;
        tmp.text = textCarrousel[index];
        StartCoroutine(BlinkButton());
    }

    private IEnumerator BlinkButton()
    {
        while (true)
        {
            if (blinkButton) button.GetComponent<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(blinkTime);
            if (blinkButton) button.GetComponent<Renderer>().material.color = Color.green;
            yield return new WaitForSeconds(blinkTime);
        }
    }

    public void IncrementIndex()
    {
        buttonAnimation.Play();
        index++;
        tmp.text = "";
        StartCoroutine(DisplayText());
        if (index >= textCarrousel.Count - 1)
        {
            buttonInteractable.enabled = false;
            blinkButton = false;
        }
    }

    public IEnumerator DisplayText()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < textCarrousel[index].Length; i++)
        {
            tmp.text += textCarrousel[index][i];
            yield return new WaitForSeconds(timeBetweenCharacters);
        }
    }
}
