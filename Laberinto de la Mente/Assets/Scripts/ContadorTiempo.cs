using UnityEngine;
using TMPro;

public class ContadorTiempo : MonoBehaviour
{
    public TextMeshProUGUI textoContador;
    private float tiempoTranscurrido = 0f;
    private bool contadorActivo = false;

    void Start()
    {
        // Establecemos el contador como activo y reiniciamos el contador al inicio del juego
        ReiniciarContador();
    }

    void Update()
    {
        if (contadorActivo)
        {
            tiempoTranscurrido += Time.deltaTime;
            ActualizarTextoContador(tiempoTranscurrido);
        }
    }

    void OnApplicationQuit()
    {
        // Al cerrar la aplicación, guardar el estado del contador en PlayerPrefs
        PlayerPrefs.SetInt("ContadorActivo", contadorActivo ? 1 : 0);
        PlayerPrefs.Save();

        if (contadorActivo)
        {
            // Al cerrar la aplicación, guardar el tiempo transcurrido en PlayerPrefs
            PlayerPrefs.SetFloat("TiempoTranscurrido", tiempoTranscurrido);
            PlayerPrefs.Save();
        }
    }

    void ActualizarTextoContador(float tiempo)
    {
        // Convierte el tiempo en minutos, segundos y milisegundos
        int minutos = Mathf.FloorToInt(tiempo / 60);
        int segundos = Mathf.FloorToInt(tiempo % 60);
        int milisegundos = Mathf.FloorToInt((tiempo * 1000) % 1000);

        // Actualiza el TextMeshPro con el nuevo tiempo
        textoContador.text = minutos.ToString("00") + ":" + segundos.ToString("00") + ":" + milisegundos.ToString("000");
    }

    public void ReiniciarContador()
    {
        contadorActivo = true;
        tiempoTranscurrido = 0f;
        ActualizarTextoContador(tiempoTranscurrido);
    }
}
