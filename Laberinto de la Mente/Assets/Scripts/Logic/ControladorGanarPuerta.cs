using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorGanarPuerta : MonoBehaviour
{
    // Variable global para almacenar la información de los objetos conseguidos
    public static bool objetosConseguidos = false;
    public string nextScene = "";

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el collider es el del jugador
        if (other.CompareTag("Player"))
        {
            // Si el jugador ha conseguido los objetos necesarios, guarda la información
            if (objetosConseguidos)
            {
                // Guardar la información de que se consiguieron los objetos (puedes usar PlayerPrefs o cualquier otro método para guardar datos entre escenas)
                PlayerPrefs.SetInt("ObjetosConseguidos", 2);
                PlayerPrefs.SetInt("SpawnEnemy", 0); // No spawnear Enemy
            }
            else
            {
                PlayerPrefs.SetInt("SpawnEnemy", 1); // Spawnear Enemy
            }

            // Carga la siguiente escena por nombre
            CargarSiguienteEscena(nextScene);
        }
    }

    private void CargarSiguienteEscena(string nextScene)
    {
        // Carga la escena sin verificar si existe
        SceneManager.LoadScene(nextScene);
    }
}
