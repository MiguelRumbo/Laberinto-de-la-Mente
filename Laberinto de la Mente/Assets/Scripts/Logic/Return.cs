using UnityEngine;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
    private string nombreUltimaEscena;

    void Start()
    {
        // Al inicio, guarda el nombre de la escena actual.
        nombreUltimaEscena = SceneManager.GetActiveScene().name;
    }

    // Método para regresar a la última escena almacenada.
    public void RegresarALaUltimaEscena()
    {
        SceneManager.LoadScene(nombreUltimaEscena);
    }

    // Método para establecer el nombre de la última escena.
    public void SetUltimaEscena(string nombreEscena)
    {
        nombreUltimaEscena = nombreEscena;
    }
}
