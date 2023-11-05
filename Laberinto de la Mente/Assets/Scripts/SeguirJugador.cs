using UnityEngine;
using UnityEngine.AI;

public class SeguirJugador : MonoBehaviour
{
    public Transform jugador;
    private NavMeshAgent navMeshAgent;
    private Animator anim;

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

            // Establece el parámetro "Move" en true para cambiar a la animación de caminar
            anim.SetBool("Move", navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance);
        }
    }
}
