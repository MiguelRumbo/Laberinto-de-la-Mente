using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab; // Debes asignar el prefab del enemigo en el inspector.
    public Transform cubeTransform; // Debes asignar la transformación del cubo en el inspector.

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
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("No se ha asignado el prefab del enemigo o la transformación del cubo.");
        }
    }
}
