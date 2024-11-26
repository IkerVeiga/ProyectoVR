using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antorch : MonoBehaviour
{
    public Light pointLight; // Referencia a la luz
    public float targetIntensity = 5f; // Intensidad objetivo
    public float duration = 2f; // Tiempo en segundos para completar el cambio

    void Start()
    {
        if (pointLight == null)
        {
            pointLight = GetComponent<Light>();
        }

        // Inicia la corutina cíclica
        StartCoroutine(CycleLightIntensity(targetIntensity, duration));
    }

    public IEnumerator CycleLightIntensity(float target, float time)
    {
        float startIntensity = pointLight.intensity; // Intensidad inicial

        while (true) // Bucle infinito para el ciclo
        {
            // Fase de subida: De startIntensity a target
            yield return StartCoroutine(ChangeLightIntensity(startIntensity, target, time));

            // Fase de bajada: De target a startIntensity
            yield return StartCoroutine(ChangeLightIntensity(target, startIntensity, time));
        }
    }

    private IEnumerator ChangeLightIntensity(float from, float to, float time)
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            pointLight.intensity = Mathf.Lerp(from, to, elapsedTime / time);
            yield return null; // Espera al siguiente frame
        }

        pointLight.intensity = to; // Asegura que la intensidad llegue exactamente al valor final
    }
}
