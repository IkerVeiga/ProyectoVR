using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoRayo : MonoBehaviour
{
    public int maxRebotes = 5; // M�ximo de rebotes del rayo
    public float rayoDistancia = 100f; // Distancia m�xima del rayo
    public LineRenderer lineRenderer; // Componente para visualizar el rayo
    public Material material; // Material para cambiar el color del rayo

    public Transform objetoMover; // Objeto que se mover�
    public Vector3 posicionInicialObjeto; // Posici�n inicial del objeto
    public float nuevaPosicionY; // Nueva posici�n Y del objeto cuando choca con "Final"
    public float velocidadTransicion; // Velocidad de la transici�n de movimiento

    private bool haChocadoConFinal = false; // Estado del rayo

    void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        // Ancho constante del rayo
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        // Opcional: Configurar curva de ancho
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0f, 0.05f); // Inicio del rayo
        curve.AddKey(1f, 0.1f); // Final del rayo
        lineRenderer.widthCurve = curve;

        // Guardar la posici�n inicial del objeto
        if (objetoMover != null)
        {
            posicionInicialObjeto = objetoMover.position;
        }
    }

    void Update()
    {
        DibujarRayo();
        MoverObjeto();
    }

    void DibujarRayo()
    {
        Vector3 origen = transform.position;
        Vector3 direccion = transform.forward; // Direcci�n inicial del rayo
        haChocadoConFinal = false;

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, origen);

        for (int i = 0; i < maxRebotes; i++)
        {
            if (Physics.Raycast(origen, direccion, out RaycastHit hit, rayoDistancia))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(i + 1, hit.point);

                // Si el rayo choca con un objeto con tag "Final"
                if (hit.collider.CompareTag("Final"))
                {
                    haChocadoConFinal = true;
                    break; // No necesitamos seguir rebotando
                }
                else if (hit.collider.CompareTag("Espejo"))
                {
                    // Si el rayo choca con un espejo, calcula la nueva direcci�n
                    direccion = Vector3.Reflect(direccion, hit.normal);
                    origen = hit.point;
                }
                else
                {
                    // Si no es ni espejo ni final, det�n el rayo
                    break;
                }
            }
            else
            {
                // Si no choca con nada, extiende el rayo hasta el l�mite
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(i + 1, origen + direccion * rayoDistancia);
                break;
            }
        }

        // Cambiar color del rayo seg�n el resultado del rebote
        CambiarColorRayo(haChocadoConFinal);
    }

    void CambiarColorRayo(bool chocoConFinal)
    {
        if (chocoConFinal)
        {
            material.color = Color.green; // Cambiar a verde si choca con "Final"
        }
        else
        {
            material.color = Color.red; // Cambiar a rojo si no choca con "Final"
        }
        lineRenderer.material = material; // Actualizar el material del LineRenderer
    }

    void MoverObjeto()
    {
        if (objetoMover != null)
        {
            Vector3 posicionObjetivo;

            if (haChocadoConFinal)
            {
                // Nueva posici�n en Y si choca con "Final"
                posicionObjetivo = new Vector3(objetoMover.position.x, nuevaPosicionY, objetoMover.position.z);
            }
            else
            {
                // Volver a la posici�n inicial si no hay colisi�n
                posicionObjetivo = posicionInicialObjeto;
            }

            // Interpolaci�n para un movimiento suave
            objetoMover.position = Vector3.Lerp(objetoMover.position, posicionObjetivo, Time.deltaTime * velocidadTransicion);

            // Comprobar si estamos suficientemente cerca del objetivo
            if (Vector3.Distance(objetoMover.position, posicionObjetivo) < 0.01f)
            {
                // Forzar la posici�n exacta para evitar overshooting
                objetoMover.position = posicionObjetivo;
            }
        }
    }
}
