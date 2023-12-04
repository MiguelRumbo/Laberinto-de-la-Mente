using System.Collections;
using UnityEngine;
using TMPro;

public class SpawnEnemyV3 : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  // Array de prefabs
    public Transform playerTransform;
    public Transform[] spawnPoints;
    public Animator animator;
    public Light playerLight;
    public TextMeshProUGUI lifetimeText;
    public TextMeshProUGUI cooldownText;

    public float lifetime = 30f;
    public float cooldownDuration = 1f;

    private bool canSpawn = true;
    private float lastEnemySpawnTime = 0f;
    private float cooldownTimer = 0f;

    void Start()
    {
        lastEnemySpawnTime = Time.time;
        StartCoroutine(SpawnEnemyPeriodically());
    }

    IEnumerator SpawnEnemyPeriodically()
    {
        while (true)
        {
            if (canSpawn)
            {
                SpawnAll();
                canSpawn = false;
                lastEnemySpawnTime = Time.time;
                cooldownTimer = cooldownDuration;
            }
            yield return null;
        }
    }

    void SpawnAll()
    {
        if (enemyPrefabs != null && enemyPrefabs.Length > 0)
        {
            if (spawnPoints.Length > 0)
            {
                int enemiesToSpawn = Mathf.Min(enemyPrefabs.Length, spawnPoints.Length);

                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
                    GameObject newEnemy = Instantiate(enemyPrefabs[randomEnemyIndex], spawnPoints[i].position, Quaternion.identity);
                    StartCoroutine(DestroyEnemyAfterLifetime(newEnemy, lifetime));

                    SeguirJugador scriptSeguirJugador = newEnemy.GetComponent<SeguirJugador>();

                    if (scriptSeguirJugador != null)
                    {
                        scriptSeguirJugador.jugador = playerTransform;
                    }
                }

                // Verificar si hay al menos un enemigo antes de activar el animator
                if (enemiesToSpawn > 0)
                {
                    animator.enabled = true;
                }
            }
            else
            {
                Debug.LogError("No se ha asignado puntos de spawn.");
            }
        }
        else
        {
            Debug.LogError("No se ha asignado ningún prefab de enemigo.");
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
            cooldownTimer -= Time.deltaTime;
            cooldownTimer = Mathf.Max(0, cooldownTimer);

            if (cooldownTimer == 0)
            {
                canSpawn = true;
                lastEnemySpawnTime = Time.time;
                cooldownTimer = cooldownDuration;
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
