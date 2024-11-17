using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStatue : MonoBehaviour
{
    public GameObject statue; // El objeto a rotar
    public float rotationAmount; // Grados de rotaci�n
    public float duration; // Tiempo en segundos para completar la rotaci�n

    private bool isRotating = false; // Evita que inicies m�ltiples rotaciones a la vez

    public void rotateStatue()
    {
        if (!isRotating) // Verificar que no haya otra rotaci�n en curso
        {
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
    }
}
