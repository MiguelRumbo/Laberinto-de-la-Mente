using UnityEngine;
using UnityEngine.SceneManagement;

public class Anillos : MonoBehaviour
{
    // Variable para almacenar los objetos encontrados
    private static int anillosEncontrados = 0;

    // Referencia al script ControladorGanar
    public ControladorGanar controladorGanar;

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que colisionó tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            // Destruir el objeto actual
            Destroy(gameObject);

            // Incrementar el contador de anillos encontrados
            anillosEncontrados++;

            // Mostrar en la consola la cantidad de anillos encontrados hasta ahora
            Debug.Log("Anillo encontrado. Total: " + anillosEncontrados);

            // Verificar si se han encontrado todos los anillos (en este caso, 3)
            if (anillosEncontrados == 3)
            {
                // Establecer la variable objetosConseguidos en el script ControladorGanar
                ControladorGanar.objetosConseguidos = true;

                // Mostrar mensaje en la consola cuando se encuentran los 3 anillos
                Debug.Log("¡Has encontrado todos los anillos!");
            }
        }
    }
}
