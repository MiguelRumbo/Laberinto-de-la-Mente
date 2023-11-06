using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public SpawnEnemy spawnScript; // Asigna el script de SpawnEnemy en el Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo")) // Asegúrate de que el enemigo tenga un tag "Enemigo"
        {
            // Destruye el enemigo
            Destroy(other.gameObject);
        }
    }
}
