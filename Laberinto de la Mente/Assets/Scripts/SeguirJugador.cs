using UnityEngine;

public class SeguirJugador : MonoBehaviour
{
    public Transform jugador;
    public float velocidadMovimiento = 5.0f;
    public float rangoDeteccion = 10f; // Alcance de detección configurable desde el Inspector
    private Animator anim;
    private float alturaInicial;

    void Start()
    {
        anim = GetComponent<Animator>(); // Obtén el Animator del enemigo
        alturaInicial = transform.position.y; // Guarda la posición Y inicial
    }

    void Update()
    {
        // Verifica si el jugador está dentro del rango de detección
        float distanciaAlJugador = Vector3.Distance(transform.position, jugador.position);

        if (distanciaAlJugador < rangoDeteccion)
        {
            // Calcula la dirección hacia el jugador sin movimiento en el eje Y
            Vector3 direccion = (jugador.position - transform.position);
            direccion.y = 0;
            direccion.Normalize();

            // Mueve el enemigo hacia el jugador sin cambiar su altura
            transform.position = new Vector3(transform.position.x, alturaInicial, transform.position.z);
            transform.Translate(direccion * velocidadMovimiento * Time.deltaTime);

            // Establece el parámetro "Move" en true para cambiar a la animación de caminar
            anim.SetBool("Move", true);

            // Rota el enemigo para que mire al jugador
            transform.LookAt(jugador);
        }
        else
        {
            // Establece el parámetro "Move" en false para cambiar a la animación idle
            anim.SetBool("Move", false);
        }
    }
}
