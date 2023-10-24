using UnityEngine;
using TMPro;

public class ControladorPulso : MonoBehaviour
{
    public Transform player;
    public string enemigoTag = "Enemigo";
    public TextMeshProUGUI textMeshPro;

    private float bpm = 100f; // Inicialmente a 70 BPM
    private float minBPM = 90f; // Puedes cambiar esto si deseas un mínimo diferente
    private float maxBPM = 180f;
    private float distanciaMaxima = 10f;

    private float pulseVariation = 5f; // Variación máxima en el pulso

    private float pulseChangeCooldown = 1f; // Tiempo de enfriamiento para cambiar el pulso
    private float lastPulseChangeTime = 0f;

    void Update()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag(enemigoTag);
        float distanciaMinima = float.MaxValue;

        // Encuentra la distancia mínima entre el jugador y los enemigos
        foreach (GameObject enemigo in enemigos)
        {
            float distancia = Vector3.Distance(player.position, enemigo.transform.position);
            distanciaMinima = Mathf.Min(distanciaMinima, distancia);
        }

        // Ajusta el BPM en función de la distancia
        bpm = Mathf.Lerp(minBPM, maxBPM, 1f - Mathf.Clamp01(distanciaMinima / distanciaMaxima));

        // Comprueba si es hora de cambiar el pulso
        if (Time.time - lastPulseChangeTime >= pulseChangeCooldown)
        {
            // Agrega una variación aleatoria al pulso entre -pulseVariation y pulseVariation
            float pulseVariationAmount = Random.Range(-pulseVariation, pulseVariation);
            bpm += pulseVariationAmount;

            // Limita el valor del BPM a un rango razonable
            bpm = Mathf.Clamp(bpm, minBPM, maxBPM);

            // Actualiza el TextMeshPro con el valor del BPM
            textMeshPro.text = "" + Mathf.RoundToInt(bpm);

            // Actualiza el tiempo del último cambio de pulso
            lastPulseChangeTime = Time.time;
        }
    }
}
