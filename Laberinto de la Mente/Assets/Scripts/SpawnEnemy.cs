using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab; // Asigna el prefab del enemigo en el inspector.
    public Transform playerTransform; // Asigna la transformación del jugador en el inspector.
    public Transform[] spawnPoints; // Asigna los cubos hijos como puntos de spawn en el inspector.

    private bool canSpawn = true;
    private float minCooldownDuration = 30f; // Mínima duración del cooldown en segundos
    private float maxCooldownDuration = 60f; // Máxima duración del cooldown en segundos
    private float lastEnemyDestroyedTime = 0f; // Almacena el tiempo en que se destruyó el último enemigo.

    void Start()
    {
        StartCoroutine(SpawnEnemyPeriodically());
    }

    IEnumerator SpawnEnemyPeriodically()
    {
        while (true)
        {
            if (canSpawn && GameObject.FindObjectOfType<SeguirJugador>() == null)
            {
                Spawn();
                canSpawn = false;
                lastEnemyDestroyedTime = Time.time;
            }
            yield return null;
        }
    }

    void Spawn()
    {
        if (enemyPrefab != null && spawnPoints.Length > 0)
        {
            // Elije aleatoriamente uno de los puntos de spawn.
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomSpawnIndex];

            // Obtén la posición del punto de spawn.
            Vector3 spawnPosition = spawnPoint.position;

            // Genera el enemigo en la posición del punto de spawn.
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // Establece un tiempo de vida aleatorio entre 10 y 15 segundos.
            float lifetime = Random.Range(10f, 15f);
            Destroy(newEnemy, lifetime);

            // Obtén la referencia al script "SeguirJugador" del nuevo enemigo
            SeguirJugador scriptSeguirJugador = newEnemy.GetComponent<SeguirJugador>();

            if (scriptSeguirJugador != null)
            {
                // Asigna el transform del jugador al script "SeguirJugador" del enemigo
                scriptSeguirJugador.jugador = playerTransform;
            }
        }
        else
        {
            Debug.LogError("No se ha asignado el prefab del enemigo o puntos de spawn.");
        }
    }

    void Update()
    {
        // Verifica si ha pasado el tiempo de cooldown y permite el spawn si es el caso.
        if (!canSpawn && Time.time - lastEnemyDestroyedTime >= Random.Range(minCooldownDuration, maxCooldownDuration))
        {
            canSpawn = true;
        }
    }
}
