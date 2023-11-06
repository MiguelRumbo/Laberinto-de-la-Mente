using UnityEngine;
using UnityEngine.AI;

public class SeguirJugador : MonoBehaviour
{
    public Transform jugador;
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    private bool isInDetectionZone = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (jugador != null)
        {
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
                anim.SetBool("Move", navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance);
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
