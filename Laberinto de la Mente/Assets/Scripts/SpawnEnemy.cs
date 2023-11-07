using System.Collections;
using UnityEngine;


public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform playerTransform;
    public Transform[] spawnPointsPlantaAlta;
    public Transform[] spawnPointsPlantaBaja;
    public Animator animator;
    public Light playerLight;

    private bool canSpawn = true;
    private float minCooldownDuration = 30f;
    private float maxCooldownDuration = 60f;
    private float lastEnemyDestroyedTime = 0f;
    private bool jugadorEnPlantaAlta = false;

    void Start()
    {
        StartCoroutine(SpawnEnemyPeriodically());
    }

    IEnumerator SpawnEnemyPeriodically()
    {
        while (true)
        {
            if (canSpawn)
            {
                Spawn(jugadorEnPlantaAlta);
                canSpawn = false;
                lastEnemyDestroyedTime = Time.time;
            }
            yield return null;
        }
    }

    void Spawn(bool jugadorEnPlantaAlta)
    {
        if (enemyPrefab != null)
        {
            Transform[] spawnPoints = jugadorEnPlantaAlta ? spawnPointsPlantaAlta : spawnPointsPlantaBaja;

            if (spawnPoints.Length > 0)
            {
                int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
                Transform spawnPoint = spawnPoints[randomSpawnIndex];
                Vector3 spawnPosition = spawnPoint.position;

                GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                float lifetime = Random.Range(10f, 15f);
                Destroy(newEnemy, lifetime);

                SeguirJugador scriptSeguirJugador = newEnemy.GetComponent<SeguirJugador>();

                if (scriptSeguirJugador != null)
                {
                    scriptSeguirJugador.jugador = playerTransform;
                }

                animator.enabled = true;
            }
            else
            {
                Debug.LogError("No se ha asignado puntos de spawn para la planta actual.");
            }
        }
        else
        {
            Debug.LogError("No se ha asignado el prefab del enemigo.");
        }
    }

    public void SetPlayerEnPlantaAlta(bool enPlantaAlta)
    {
        jugadorEnPlantaAlta = enPlantaAlta;
    }

    void Update()
    {
        if (!canSpawn && Time.time - lastEnemyDestroyedTime >= Random.Range(minCooldownDuration, maxCooldownDuration))
        {
            canSpawn = true;
        }

        /* Debug.Log("Tiempo de cooldown actual: " + (Time.time - lastEnemyDestroyedTime)); */
        
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");

        if (enemigos.Length == 0)
        {
            animator.enabled = false;
            playerLight.enabled = true;
        }
    }
}