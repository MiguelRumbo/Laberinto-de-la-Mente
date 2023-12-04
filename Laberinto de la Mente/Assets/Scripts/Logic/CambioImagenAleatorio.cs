using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CambioImagenConFade : MonoBehaviour
{
    public Image imagen;
    public Sprite[] imagenes;
    public float tiempoDeEspera = 2.0f;
    public float tiempoDeFade = 1.0f;

    private CanvasGroup canvasGroup;

    private void Start()
    {
        if (imagen == null || imagenes.Length == 0)
        {
            Debug.LogError("Falta asignar el componente Image o las imágenes en el inspector.");
            return;
        }

        // Agrega un CanvasGroup al objeto si no existe
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // Inicia el cambio de imágenes
        StartCoroutine(CambiarImagenConFade());
    }

    IEnumerator CambiarImagenConFade()
    {
        while (true)
        {
            foreach (Sprite sprite in imagenes)
            {
                // Configura la imagen y el CanvasGroup
                imagen.sprite = sprite;
                canvasGroup.alpha = 0;

                // Realiza el fade-in
                while (canvasGroup.alpha < 1)
                {
                    canvasGroup.alpha += Time.deltaTime / tiempoDeFade;
                    yield return null;
                }

                // Espera el tiempo especificado
                yield return new WaitForSeconds(tiempoDeEspera);
            }
        }
    }
}
