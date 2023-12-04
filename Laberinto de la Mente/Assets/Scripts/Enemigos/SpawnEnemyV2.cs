using System.Collections;
using UnityEngine;
using TMPro;

public class SpawnEnemyV2 : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform playerTransform;
    public Transform[] spawnPoints;
    public Animator animator;
    public Light playerLight;
    public TextMeshProUGUI lifetimeText;
    public TextMeshProUGUI cooldownText;

    public float lifetime = 30f;
    public float cooldownDuration = 1f; // El tiempo de cooldown ahora es constante

    private bool canSpawn = true;
    private float lastEnemySpawnTime = 0f;
    private float cooldownTimer = 0f;

    void Start()
    {
        lastEnemySpawnTime = Time.time; // Iniciar el tiempo de spawn al inicio
        StartCoroutine(SpawnEnemyPeriodically());
    }

    IEnumerator SpawnEnemyPeriodically()
    {
        while (true)
        {
            if (canSpawn)
            {
                Spawn();
                canSpawn = false;
                lastEnemySpawnTime = Time.time;
                cooldownTimer = cooldownDuration; // Reiniciar el cooldown al spawnear un enemigo
            }
            yield return null;
        }
    }

    void Spawn()
    {
        if (enemyPrefab != null)
        {
            if (spawnPoints.Length > 0)
            {
                int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
                Transform spawnPoint = spawnPoints[randomSpawnIndex];
                Vector3 spawnPosition = spawnPoint.position;

                GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                StartCoroutine(DestroyEnemyAfterLifetime(newEnemy, lifetime));

                SeguirJugador scriptSeguirJugador = newEnemy.GetComponent<SeguirJugador>();

                if (scriptSeguirJugador != null)
                {
                    scriptSeguirJugador.jugador = playerTransform;
                }

                animator.enabled = true;
            }
            else
            {
                Debug.LogError("No se ha asignado puntos de spawn.");
            }
        }
        else
        {
            Debug.LogError("No se ha asignado el prefab del enemigo.");
        }
    }

    IEnumerator DestroyEnemyAfterLifetime(GameObject enemy, float enemyLifetime)
    {
        yield return new WaitForSeconds(enemyLifetime);
        Destroy(enemy);
    }

    void Update()
    {
        float elapsedTimeSinceLastSpawn = Time.time - lastEnemySpawnTime;

        lifetimeText.text = "Tiempo de vida: " + Mathf.Max(0, lifetime - elapsedTimeSinceLastSpawn).ToString("F2");

        if (!canSpawn && elapsedTimeSinceLastSpawn >= lifetime)
        {
            cooldownTimer -= Time.deltaTime; // Comienza a disminuir el cooldown después de que se acabe el tiempo de vida
            cooldownTimer = Mathf.Max(0, cooldownTimer);

            if (cooldownTimer == 0)
            {
                canSpawn = true;
                lastEnemySpawnTime = Time.time; // Reiniciar el tiempo de spawn al finalizar el tiempo de vida
                cooldownTimer = cooldownDuration; // Reiniciar el cooldown
            }
        }

        cooldownText.text = "Tiempo de cooldown: " + cooldownTimer.ToString("F2");

        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");

        if (enemigos.Length == 0)
        {
            animator.enabled = false;
            playerLight.enabled = true;
        }
    }
}
