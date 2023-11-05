using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab; // Debes asignar el prefab del enemigo en el inspector.
    public Transform cubeTransform; // Debes asignar la transformación del cubo en el inspector.
    public Transform playerTransform; // Debes asignar la transformación del jugador en el inspector.

    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        if (enemyPrefab != null && cubeTransform != null)
        {
            // Obtén la posición del cubo.
            Vector3 spawnPosition = cubeTransform.position;

            // Genera el enemigo en la posición del cubo.
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

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
            Debug.LogError("No se ha asignado el prefab del enemigo, la transformación del cubo o la transformación del jugador.");
        }
    }
}
