using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorBarraVida : MonoBehaviour
{
    public Image healthBarImage;
    public float damageAmount = 0.15f;
    public float recoveryRate = 0.1f;
    public float timeToStartRecovery = 5f;
    public string gameOverSceneName = "";
    private float lastDamageTime;

    private void Start()
    {
        lastDamageTime = Time.time; // Inicializa el tiempo del último daño al inicio del juego.

        // Busca el objeto "ImageBarra" en la jerarquía de la escena.
        healthBarImage = GameObject.Find("Pantalla/HealthBar/ImageBarra")?.GetComponent<Image>();

        if (healthBarImage == null)
        {
            Debug.LogError("Error: No se encontró el objeto ImageBarra en la jerarquía.");
        }
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
        float timeSinceLastDamage = Time.time - lastDamageTime;

        if (timeSinceLastDamage >= timeToStartRecovery)
        {
            StartRecovery();
        }
    }

    public void ReduceHealth()
    {
        if (healthBarImage != null && healthBarImage.fillAmount > 0)
        {
            healthBarImage.fillAmount -= damageAmount;
            lastDamageTime = Time.time;

            if (healthBarImage.fillAmount <= 0)
            {
                // Guarda el nombre de la escena actual antes de cargar la escena de game over.
                PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
                SceneManager.LoadScene(gameOverSceneName);
            }
        }
    }

    private void StartRecovery()
    {
        if (healthBarImage != null && healthBarImage.fillAmount < 1)
        {
            healthBarImage.fillAmount = Mathf.Clamp(healthBarImage.fillAmount + recoveryRate * Time.deltaTime, 0f, 1f);
        }
    }
}
