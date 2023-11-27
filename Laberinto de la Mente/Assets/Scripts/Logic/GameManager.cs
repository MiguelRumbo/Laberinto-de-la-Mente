using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool juegoPausado = false;
    public GameObject jugador; // Asigna el objeto del jugador desde el Inspector

    // Update is called once per frame
    void Update()
    {
        // Verificar si se presiona la tecla ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Cambiar el estado de pausa
            juegoPausado = !juegoPausado;

            // Llamar a la función para pausar o reanudar el juego
            if (juegoPausado)
            {
                PausarJuego();
            }
            else
            {
                ReanudarJuego();
            }
        }
    }

    void PausarJuego()
    {
        // Pausar el tiempo en el juego
        Time.timeScale = 0f;

        // Desactivar el objeto del jugador
        if (jugador != null)
        {
            jugador.SetActive(false);
        }

        // Desbloquear el cursor y hacerlo visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Aquí puedes agregar cualquier lógica adicional cuando el juego esté pausado
        Debug.Log("Juego pausado");
    }

    void ReanudarJuego()
    {
        // Reanudar el tiempo en el juego
        Time.timeScale = 1f;

        // Activar el objeto del jugador
        if (jugador != null)
        {
            jugador.SetActive(true);
        }

        // Bloquear el cursor y hacerlo invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Aquí puedes agregar cualquier lógica adicional cuando el juego se reanude
        Debug.Log("Juego reanudado");
    }
}
