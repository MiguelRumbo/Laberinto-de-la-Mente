using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnEnemyV4 : MonoBehaviour
{
    [Header("Enemy Prefabs and Spawn Points")]
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;

    [Header("Player Settings")]
    public Transform playerTransform;
    public Animator animator;
    public Light playerLight;

    [Header("UI Settings")]
    public TextMeshProUGUI lifetimeText;
    public TextMeshProUGUI cooldownText;

    [Header("Enemy Settings")]
    public float lifetime = 30f;
    public float cooldownDuration = 1f;

    [Header("Testing Controls")]
    public bool spawnEnemy = false;
    public bool spawnKid1 = false;

    private bool canSpawn = true;
    private float lastEnemySpawnTime = 0f;
    private float cooldownTimer = 0f;

    void Start()
    {
        lastEnemySpawnTime = Time.time;
        spawnEnemy = PlayerPrefs.GetInt("SpawnEnemy") == 1; // Recuperar el valor de SpawnEnemy
        spawnKid1 = PlayerPrefs.GetInt("SpawnKid1") == 1; // Recuperar el valor de SpawnKid1
        StartCoroutine(SpawnEnemyPeriodically());
    }

    IEnumerator SpawnEnemyPeriodically()
    {
        while (true)
        {
            // Obtener el estado de objetosConseguidos desde las otras escenas
            bool objetosConseguidosGanar = PlayerPrefs.GetInt("ObjetosConseguidos") == 1;
            bool objetosConseguidosGanarPuerta = PlayerPrefs.GetInt("ObjetosConseguidos") == 2;
            if (canSpawn)
            {
                Spawn(objetosConseguidosGanar, objetosConseguidosGanarPuerta);
                canSpawn = false;
                lastEnemySpawnTime = Time.time;
                cooldownTimer = cooldownDuration;
            }
            yield return null;
        }
    }

    void Spawn(bool objetosConseguidosGanar, bool objetosConseguidosGanarPuerta)
    {
        if (enemyPrefabs != null && enemyPrefabs.Length > 0)
        {
            if (spawnPoints.Length > 0)
            {
                List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

                for (int i = 0; i < enemyPrefabs.Length; i++)
                {
                    if (availableSpawnPoints.Count == 0)
                    {
                        Debug.LogError("No hay suficientes puntos de spawn disponibles.");
                        break;
                    }

                    int randomSpawnIndex = Random.Range(0, availableSpawnPoints.Count);
                    Transform spawnPoint = availableSpawnPoints[randomSpawnIndex];
                    availableSpawnPoints.RemoveAt(randomSpawnIndex);

                    Vector3 spawnPosition = spawnPoint.position;
                    GameObject newEnemy = null;

                    // BonBon_T-Pose siempre debe spawnear
                    if (i == 0)
                    {
                        newEnemy = Instantiate(enemyPrefabs[i], spawnPosition, Quaternion.identity);
                    }

                    // Enemy, solo debe spawanear junto con BonBon, cuando los objetosConseguidos, de ControladorGanarPuerta, no fueron conseguidos (false)
                    if (i == 1 && !objetosConseguidosGanarPuerta && PlayerPrefs.GetInt("SpawnEnemy") == 1)
                    {
                        newEnemy = Instantiate(enemyPrefabs[i], spawnPosition, Quaternion.identity);
                    }

                    // Kid 1 solo debe spawanear junto con BonBon, cuando cuando los objetosConseguidos, de ControladorGanar, no fueron conseguidos (false)
                    if (i == 2 && !objetosConseguidosGanar && PlayerPrefs.GetInt("SpawnKid1") == 1)
                    {
                        newEnemy = Instantiate(enemyPrefabs[i], spawnPosition, Quaternion.identity);
                    }

                    if (newEnemy != null)
                    {
                        StartCoroutine(DestroyEnemyAfterLifetime(newEnemy, lifetime));

                        SeguirJugador scriptSeguirJugador = newEnemy.GetComponent<SeguirJugador>();
                        if (scriptSeguirJugador != null)
                        {
                            scriptSeguirJugador.jugador = playerTransform;
                        }
                    }
                }

                // Activar el animator cuando se han creado enemigos
                animator.enabled = true;
            }
            else
            {
                Debug.LogError("No se ha asignado puntos de spawn.");
            }
        }
        else
        {
            Debug.LogError("No se han asignado prefabs de enemigos.");
        }
    }

    IEnumerator DestroyEnemyAfterLifetime(GameObject enemy, float enemyLifetime)
    {
        yield return new WaitForSeconds(enemyLifetime);
        Destroy(enemy);
        CheckAnimatorState();
    }

    void CheckAnimatorState()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");

        if (enemigos.Length == 0)
        {
            // Desactivar el animator cuando no hay enemigos
            animator.enabled = false;
            playerLight.enabled = true;
        }
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
    }
}
