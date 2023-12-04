using UnityEngine;

public class ReproductorAleatorio : MonoBehaviour
{
    public AudioClip[] clips; // Asigna tus clips de audio aquí en el inspector de Unity
    private AudioSource audioSource;
    private float tiempoMinimoEntreSonidos = 10f;
    private float tiempoMaximoEntreSonidos = 20f;
    private float tiempoEntreSonidos;
    private float tiempoUltimoSonido;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        tiempoUltimoSonido = Time.time;

        // Reproduce el primer sonido al inicio
        ReproducirSonidoAleatorio();
    }

    void Update()
    {
        // Verifica si ha pasado suficiente tiempo para reproducir otro sonido
        if (Time.time - tiempoUltimoSonido > tiempoEntreSonidos)
        {
            ReproducirSonidoAleatorio();
            tiempoUltimoSonido = Time.time;
        }
    }

    void ReproducirSonidoAleatorio()
    {
        // Verifica si hay clips de audio asignados
        if (clips.Length > 0)
        {
            // Elije un índice de clip aleatorio
            int indiceAleatorio = Random.Range(0, clips.Length);

            // Reproduce el clip seleccionado
            audioSource.clip = clips[indiceAleatorio];
            audioSource.Play();

            // Establece un nuevo tiempo aleatorio entre sonidos
            tiempoEntreSonidos = Random.Range(tiempoMinimoEntreSonidos, tiempoMaximoEntreSonidos);
        }
        else
        {
            Debug.LogWarning("No se han asignado clips de audio en el array.");
        }
    }
}
