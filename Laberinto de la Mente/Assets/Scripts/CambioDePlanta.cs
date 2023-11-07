using UnityEngine;

public class CambioDePlanta : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (transform.CompareTag("PlantaAlta"))
            {
                Debug.Log("El jugador está en la Planta Alta.");
                SpawnEnemy spawnEnemy = FindObjectOfType<SpawnEnemy>();
                if (spawnEnemy != null)
                {
                    spawnEnemy.SetPlayerEnPlantaAlta(true);
                }
            }
            else if (transform.CompareTag("PlantaBaja"))
            {
                Debug.Log("El jugador está en la Planta Baja.");
                SpawnEnemy spawnEnemy = FindObjectOfType<SpawnEnemy>();
                if (spawnEnemy != null)
                {
                    spawnEnemy.SetPlayerEnPlantaAlta(false);
                }
            }
        }
    }
}
