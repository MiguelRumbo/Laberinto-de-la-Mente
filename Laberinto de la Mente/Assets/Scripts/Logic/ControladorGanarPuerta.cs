using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorGanarPuerta : MonoBehaviour
{
    public bool doorOpen = false;
    public float doorOpenAngle = 90f;
    public float doorCloseAngle = 0.0f;
    public float smooth = 3.0f;
    public string nextScene = "";

    public void ChangeDoorState()
    {
        doorOpen = !doorOpen;
    }

    void Update()
    {
        if(doorOpen)
        {
            Quaternion targetRotation = Quaternion.Euler(-90, doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
            CargarSiguienteEscena(nextScene);
        }
        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(-90, doorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smooth * Time.deltaTime);
        }
    }

    private void CargarSiguienteEscena(string nextScene)
    {
        // Verifica si la escena con el nombre proporcionado existe
        if (SceneManager.GetSceneByName(nextScene).IsValid())
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.LogWarning("La escena con el nombre '" + nextScene + "' no fue encontrada.");
        }
    }
}