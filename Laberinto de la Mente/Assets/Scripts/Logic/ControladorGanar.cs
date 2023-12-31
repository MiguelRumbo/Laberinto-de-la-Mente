using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorGanar : MonoBehaviour
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
                PlayerPrefs.SetInt("ObjetosConseguidos", 1);
                PlayerPrefs.SetInt("SpawnKid1", 0); // No spawnear Kid 1
            }
            else
            {
                PlayerPrefs.SetInt("SpawnKid1", 1); // Spawnear Kid 1
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
