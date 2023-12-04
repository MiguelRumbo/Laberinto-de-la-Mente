using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorGanarFinal : MonoBehaviour
{
    public string escenaTodos;
    public string escenaNadieSalio;
    public string escenaLucia;
    public string escenaLuciaDaniel;
    public string escenaLuciaSofia;
    public string escenaCreditos;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el collider es el del jugador
        if (other.CompareTag("Player"))
        {
            bool spawnEnemy = PlayerPrefs.GetInt("SpawnEnemy") == 1;
            bool spawnKid1 = PlayerPrefs.GetInt("SpawnKid1") == 1;
            bool cuadroEncontrado = PlayerPrefs.GetInt("CuadroEncontrado") == 1;

            string nextScene = "";

            // Se encontraron todos los objetos
            if (!spawnEnemy && !spawnKid1 && cuadroEncontrado)
            {
                nextScene = escenaTodos;
            }
            // No se encontro ningun objeto
            else if (spawnEnemy && spawnKid1 && !cuadroEncontrado)
            {
                nextScene = escenaNadieSalio;
            }
            // Solo se encontro el cuadro
            else if (spawnEnemy && spawnKid1 && cuadroEncontrado)
            {
                nextScene = escenaLucia;
            }
            // Solo se encontraron los anillos y el cuadro
            else if (!spawnEnemy && spawnKid1 && cuadroEncontrado)
            {
                nextScene = escenaLuciaDaniel;
            }
            // Solo se encontro la cobija y el cuadro
            else if (spawnEnemy && !spawnKid1 && cuadroEncontrado)
            {
                nextScene = escenaLuciaSofia;
            }
            // Si no es ninguna de las opciones anteriores
            else
            {
                nextScene = escenaCreditos;
            }

            // Carga la siguiente escena por nombre
            SceneManager.LoadScene(nextScene);
        }
    }
}
