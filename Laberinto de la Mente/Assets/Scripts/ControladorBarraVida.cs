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

        // Busca el objeto "Pantalla" en la escena.
        GameObject pantallaObject = GameObject.Find("Pantalla");

        // Verifica si se encontró el objeto "Pantalla" y lo imprime en la consola.
        if (pantallaObject != null)
        {
            // Busca el objeto "HealthBar" dentro de "Pantalla".
            Transform healthBarTransform = pantallaObject.transform.Find("HealthBar");

            // Verifica si se encontró el objeto "HealthBar" y lo imprime en la consola.
            if (healthBarTransform != null)
            {
                // Busca el objeto "ImageBarra" dentro de "HealthBar".
                Transform imageBarraTransform = healthBarTransform.Find("ImageBarra");

                // Verifica si se encontró el objeto "ImageBarra" y si tiene el componente "Image" adjunto.
                if (imageBarraTransform != null)
                {
                    Image imageComponent = imageBarraTransform.GetComponent<Image>();
                    if (imageComponent != null)
                    {
                        healthBarImage = imageComponent; // Asigna el componente Image a healthBarImage.
                    }
                    else
                    {
                        Debug.LogError("Error: Se encontró el objeto ImageBarra dentro de HealthBar, pero no tiene el componente Image agregado.");
                    }
                }
                else
                {
                    Debug.LogError("Error: No se encontró el objeto ImageBarra dentro de HealthBar.");
                }
            }
            else
            {
                Debug.LogError("Error: No se encontró el objeto HealthBar dentro de Pantalla.");
            }
        }
        else
        {
            Debug.LogError("Error: No se encontró el objeto Pantalla en la escena.");
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
        }

        if (healthBarImage != null && healthBarImage.fillAmount <= 0)
        {
            SceneManager.LoadScene(gameOverSceneName);
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