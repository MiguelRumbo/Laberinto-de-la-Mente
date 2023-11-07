using UnityEngine;

public class ControladorPlantas : MonoBehaviour
{
    private string plantaActual = "Baja"; // Iniciar con la planta baja por defecto

    void Start()
    {
        Debug.Log("El jugador está en la Planta " + plantaActual + ".");
    }

    public void CambioDePlanta(string planta)
    {
        plantaActual = planta; // Actualiza la planta actual cuando cambia el jugador de planta
    }
}
