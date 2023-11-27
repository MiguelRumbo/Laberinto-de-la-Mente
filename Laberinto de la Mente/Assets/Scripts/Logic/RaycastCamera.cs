using UnityEngine;

public class RaycastCamera : MonoBehaviour
{
    public float raycastDistance = 10f; // La distancia máxima del raycast

    void Update()
    {
        // Obtener la posición y la dirección de la cámara
        Vector3 cameraPosition = transform.position;
        Vector3 cameraForward = transform.forward;

        // Realizar un raycast desde la cámara
        RaycastHit hit;
        if (Physics.Raycast(cameraPosition, cameraForward, out hit, raycastDistance))
        {
            // Dibujar una línea en la escena desde la cámara hasta el punto de impacto
            Debug.DrawLine(cameraPosition, hit.point, Color.red);
        }
        else
        {
            // Si no hay impacto, dibujar una línea recta hacia adelante
            Vector3 endPosition = cameraPosition + cameraForward * raycastDistance;
            Debug.DrawLine(cameraPosition, endPosition, Color.green);
        }
    }
}
