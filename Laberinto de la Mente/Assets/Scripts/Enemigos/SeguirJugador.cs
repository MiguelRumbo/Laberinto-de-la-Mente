using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SeguirJugador : MonoBehaviour
{
    public Transform jugador;
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    private bool isInDetectionZone = false;

    // Distancia para activar Move_Fast
    public float distanciaParaMoveFast = 5f;

    // Velocidad normal y velocidad rápida
    public float velocidadNormal = 3.5f;
    public float velocidadMoveFast = 10f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        // Configura la velocidad inicial del NavMeshAgent
        navMeshAgent.speed = velocidadNormal;
    }

    void Update()
    {
        if (jugador != null)
        {
            float distanciaAlJugador = Vector3.Distance(transform.position, jugador.position);

            if (distanciaAlJugador <= distanciaParaMoveFast)
            {
                // Activa la variable Move_Fast en el animator controller
                anim.SetBool("Move_Fast", true);
                // Desactiva la variable Move_Slow en el animator controller
                anim.SetBool("Move_Slow", false);
                // Cambia la velocidad del NavMeshAgent a la velocidad rápida
                navMeshAgent.speed = velocidadMoveFast;
            }
            else
            {
                // Desactiva la variable Move_Fast en el animator controller
                anim.SetBool("Move_Fast", false);
                // Activa la variable Move_Slow en el animator controller
                anim.SetBool("Move_Slow", true);
                // Restaura la velocidad normal del NavMeshAgent
                navMeshAgent.speed = velocidadNormal;
            }

            // Establece la posición del objetivo del agente de navegación al jugador
            navMeshAgent.SetDestination(jugador.position);

            if (isInDetectionZone)
            {
                // Establece el parámetro "Attack" en true para cambiar a la animación de ataque
                anim.SetBool("Attack", true);
            }
            else
            {
                // Establece el parámetro "Attack" en false para volver a la animación de caminar
                anim.SetBool("Attack", false);

                // Establece el parámetro "Move" en true para cambiar a la animación de caminar
                anim.SetBool("Move_Slow", navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance);
            }
        }
    }

    // Método llamado cuando el enemigo entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Detection"))
        {
            isInDetectionZone = true;
        }
    }

    // Método llamado cuando el enemigo sale del trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Detection"))
        {
            isInDetectionZone = false;
        }
    }
}
