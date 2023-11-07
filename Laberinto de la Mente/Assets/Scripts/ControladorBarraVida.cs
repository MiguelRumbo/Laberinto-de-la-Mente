using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControladorBarraVida : MonoBehaviour
{
    public Image healthBarImage; // Referencia al componente Raw Image de la barra de vida.
    public float damageAmount = 0.25f; // Cantidad de daño al entrar en el trigger.
    public float recoveryRate = 0.1f; // Tasa de recuperación de vida por segundo.
    public float timeToStartRecovery = 5f; // Tiempo para comenzar a recuperar vida después de no recibir daño.
    public string gameOverSceneName = "NombreDeTuEscenaGameOver";

    private float lastDamageTime; // Momento en que se recibió el último daño.

    private void Start()
    {
        lastDamageTime = Time.time; // Inicializa el tiempo del último daño al inicio del juego.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ReduceHealth();
        }
    }

    private void Update()
    {
        // Calcula el tiempo transcurrido desde el último daño.
        float timeSinceLastDamage = Time.time - lastDamageTime;

        if (timeSinceLastDamage >= timeToStartRecovery)
        {
            StartRecovery();
        }
    }

    public void ReduceHealth()
    {
        // Asegúrate de que la barra de vida no baje de 0.
        if (healthBarImage.fillAmount > 0)
        {
            healthBarImage.fillAmount -= damageAmount;
            lastDamageTime = Time.time; // Actualiza el tiempo del último daño.
        }

        // Verifica si la barra de vida llega a cero y cambia a la escena de Game Over.
        if (healthBarImage.fillAmount <= 0)
        {
            SceneManager.LoadScene(gameOverSceneName);
        }
    }

    private void StartRecovery()
    {
        // Asegúrate de que la barra de vida no supere 1.
        if (healthBarImage.fillAmount < 1)
        {
            healthBarImage.fillAmount = Mathf.Clamp(healthBarImage.fillAmount + recoveryRate * Time.deltaTime, 0f, 1f);
        }
    }
}
