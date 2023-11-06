using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Activation : MonoBehaviour
{
    public GameObject[] objectsToDeactivate;
    public GameObject[] objectsToActivate;
    private BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }
    void Update()
    {
        // Verifica si hay enemigos vivos en el mapa.
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");

        if (enemigos.Length == 0)
        {
            boxCollider.isTrigger = true;
        }
        if (enemigos.Length > 0)
        {
            boxCollider.isTrigger = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Deshabilitar objetos
            foreach (GameObject obj in objectsToDeactivate)
            {
                obj.SetActive(false);
            }

            // Habilitar objetos
            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
            }
        }
    }
}

