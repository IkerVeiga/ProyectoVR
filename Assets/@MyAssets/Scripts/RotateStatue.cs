using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStatue : MonoBehaviour
{
    public GameObject statue; // El objeto a rotar
    public float rotationAmount; // Grados de rotaci�n
    public float duration; // Tiempo en segundos para completar la rotaci�n

    private AudioSource statueAudioSource; // AudioSource asociado a la estatua
    private bool isRotating = false; // Evita que inicies m�ltiples rotaciones a la vez

    private void Start()
    {
        // Obtener el AudioSource desde la estatua
        if (statue != null)
        {
            statueAudioSource = statue.GetComponent<AudioSource>();
            Debug.Log("Se ha encontrado Audio Source");

            if (statueAudioSource == null)
            {
                Debug.LogError("No se encontr� un AudioSource en el objeto de la estatua.");
            }
        }
        else
        {
            Debug.LogError("El objeto estatua no est� asignado en el script.");
        }
    }

    public void rotateStatue()
    {
        if (!isRotating) // Verificar que no haya otra rotaci�n en curso
        {
            // Iniciar la reproducci�n del audio en bucle
            if (statueAudioSource != null)
            {
                statueAudioSource.loop = true;
                if (!statueAudioSource.isPlaying)
                {
                    statueAudioSource.Play();
                    Debug.Log("Reproduciendo Sonido");
                }
            }

            StartCoroutine(Rotate(rotationAmount, duration));
        }
    }

    private IEnumerator Rotate(float rotationY, float time)
    {
        isRotating = true;

        // Rotaci�n inicial y objetivo
        Quaternion startRotation = statue.transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0, rotationY, 0);

        float elapsedTime = 0f;

        // Interpolar la rotaci�n durante `time` segundos
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            statue.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / time);
            yield return null; // Esperar al siguiente frame
        }

        // Asegurarse de que alcanza la rotaci�n exacta al final
        statue.transform.rotation = targetRotation;

        isRotating = false;

        // Detener la reproducci�n del audio
        if (statueAudioSource != null && statueAudioSource.isPlaying)
        {
            statueAudioSource.Stop();
            Debug.Log("Parando sonido");
        }
    }
}
