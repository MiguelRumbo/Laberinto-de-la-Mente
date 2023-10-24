using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorBarraVida : MonoBehaviour
{
    public Image healthBarImage; // Referencia al componente Raw Image de la barra de vida.
    public GameObject vidasObject; // Referencia al GameObject que contiene las vidas como hijos.
    public float damageAmount = 0.25f; // Cantidad de daño al entrar en el trigger.
    private int vidasRestantes; // Número de vidas restantes.
    public string gameOverSceneName = "NombreDeTuEscenaGameOver";


    private void Start()
    {
        // Inicializa el número de vidas restantes al número de hijos en el GameObject "Vidas".
        vidasRestantes = vidasObject.transform.childCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ReduceHealth();
        }
    }

    public void ReduceHealth() // Cambiado a "public" para que sea accesible desde fuera de la clase.
    {
        // Asegúrate de que la barra de vida no baje de 0.
        if (healthBarImage.fillAmount > 0)
        {
            healthBarImage.fillAmount -= damageAmount;
        }

        // Si la barra de vida llega a cero, desactiva un hijo de "Vidas" y restablece la barra.
        if (healthBarImage.fillAmount <= 0 && vidasRestantes > 0)
        {
            vidasRestantes--;
            healthBarImage.fillAmount = 1.0f;
            // Desactiva uno de los hijos de "Vidas" según el número de vidas restantes.
            vidasObject.transform.GetChild(vidasRestantes).gameObject.SetActive(false);
        }

        // Verifica si se quedaron sin vidas y cambia a la escena de Game Over.
        if (vidasRestantes == 0)
        {
            SceneManager.LoadScene(gameOverSceneName);
        }
    }

    public void IncreaseHealth(float amount)
    {
        // Asegúrate de que la barra de vida no supere 1.
        if (healthBarImage.fillAmount < 1)
        {
            healthBarImage.fillAmount += amount;
        }
    }
}
