using UnityEngine;
using UnityEngine.SceneManagement;

public class Cobija : MonoBehaviour
{
    // Variable para almacenar los objetos encontrados
    private static int cobijasEncontradas = 0;

    // Referencia al script ControladorGanar
    public ControladorGanarPuerta controladorGanarPuerta;

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que colisionó tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            // Destruir el objeto actual
            Destroy(gameObject);

            // Incrementar el contador de cobijas encontradas
            cobijasEncontradas++;

            // Verificar si se han encontrado todas las cobijas (en este caso, 1)
            if (cobijasEncontradas == 1)
            {
                // Establecer la variable objetosConseguidos en el script ControladorGanarPuerta
                if (controladorGanarPuerta != null)
                {
                    ControladorGanarPuerta.objetosConseguidos = true;

                    // Mostrar mensaje en la consola cuando se encuentra la cobija
                    Debug.Log("¡Has encontrado la cobija y puedes ir a la salida!");
                }
                else
                {
                    Debug.LogWarning("Advertencia: ControladorGanarPuerta no asignado en el inspector.");
                }
            }
        }
    }
}
