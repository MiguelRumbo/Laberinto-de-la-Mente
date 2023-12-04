using UnityEngine;

public class Cuadro : MonoBehaviour
{
    // Variable para almacenar los objetos encontrados
    private static int cuadroEncontradas = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que colisionó tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            // Destruir el objeto actual
            Destroy(gameObject);

            // Incrementar el contador de cuadro encontradas
            cuadroEncontradas++;

            // Verificar si se han encontrado todas los cuadros (en este caso, 1)
            if (cuadroEncontradas == 1)
            {
                // Guardar la información de que se consiguieron los objetos
                PlayerPrefs.SetInt("CuadroEncontrado", 1);

                // Mostrar mensaje en la consola cuando se encuentra la cobija
                Debug.Log("¡Has encontrado el cuadro y puedes ir a la salida!");
            }
        }
    }
}
