using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BotonReintentar : MonoBehaviour
{
    // Método para reintentar la última escena cargada.
    public void Reintentar()
    {
        // Obtiene el nombre de la última escena de PlayerPrefs y carga esa escena.
        string lastScene = PlayerPrefs.GetString("LastScene", "");
        if (!string.IsNullOrEmpty(lastScene))
        {
            SceneManager.LoadScene(lastScene);
        }
        else
        {
            // Puedes manejar este caso de acuerdo a tus necesidades (por ejemplo, cargar una escena predeterminada).
            Debug.LogWarning("No se encontró la última escena. Manejar caso.");
        }
    }
}
