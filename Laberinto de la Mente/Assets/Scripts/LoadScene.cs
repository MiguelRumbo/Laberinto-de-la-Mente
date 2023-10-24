using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para acceder a componentes UI
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class LoadScene : MonoBehaviour
{
    public GameObject panel; // Arrastra el objeto del panel aquí desde el inspector
    public float velocidadTransparencia = 1.0f; // La velocidad a la que aumenta la transparencia

    private bool panelActivo = false;
    private float transparenciaActual = 0.0f;

    void Start()
    {
        panel.SetActive(false); // Asegúrate de que el panel esté desactivado al inicio
    }

    void Update()
    {
        if (panelActivo)
        {
            // Aumenta la transparencia del panel al 100%
            transparenciaActual += velocidadTransparencia * Time.deltaTime;
            transparenciaActual = Mathf.Clamp01(transparenciaActual);
            panel.GetComponent<Image>().color = new Color(1, 1, 1, transparenciaActual);
        }
    }

    public void CambiarEscenaConPanel(string nombreDeLaEscena)
    {
        // Activa el panel antes de cambiar de escena
        panel.SetActive(true);
        panelActivo = true;

        // Inicia el proceso de cambio de escena
        StartCoroutine(CargarEscena(nombreDeLaEscena));
    }

    IEnumerator CargarEscena(string nombreDeLaEscena)
    {
        yield return new WaitForSeconds(1.0f); // Espera 1 segundo (ajusta según tu preferencia)
        SceneManager.LoadScene(nombreDeLaEscena);
    }
}
