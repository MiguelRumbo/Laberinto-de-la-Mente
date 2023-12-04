using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void Salir()
    {
        Application.Quit();
    }
    /* void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Cerrar la aplicación (juego) en el editor o en una compilación.
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    } */
}
