using UnityEngine;

public class Activation : MonoBehaviour
{
    public GameObject[] objectsToDeactivate;
    public GameObject[] objectsToActivate;

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
