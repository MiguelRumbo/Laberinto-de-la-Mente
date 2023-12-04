using UnityEngine;
using UnityEngine.AI;

public class EnemigoKidsScript : MonoBehaviour
{
    public Transform jugador;
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    private bool isInDetectionZone = false;
    private float distanciaParaSeguir = 10f; // Ajusta la distancia a tu preferencia
    private bool isFollowingPlayer = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // Llama a la función para moverse de manera aleatoria
        StartCoroutine(MoverseAleatoriamente());
    }

    void Update()
    {
        if (jugador != null)
        {
            float distanciaAlJugador = Vector3.Distance(transform.position, jugador.position);

            // Verifica si el jugador está dentro de la distancia para seguir
            if (distanciaAlJugador <= distanciaParaSeguir)
            {
                isFollowingPlayer = true;
                // Establece la posición del objetivo del agente de navegación al jugador
                navMeshAgent.SetDestination(jugador.position);

                // Activa la variable Move_Fast en el animator controller
                anim.SetBool("Move_Fast", true);
                // Desactiva la variable Move_Slow en el animator controller
                anim.SetBool("Move_Slow", false);
            }
            else
            {
                isFollowingPlayer = false;

                // Desactiva la variable Move_Fast en el animator controller
                anim.SetBool("Move_Fast", false);
                // Activa la variable Move_Slow en el animator controller
                anim.SetBool("Move_Slow", true);
            }

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
                anim.SetBool("Move_Slow", isFollowingPlayer || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance);
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

    // Función para moverse aleatoriamente
    private System.Collections.IEnumerator MoverseAleatoriamente()
    {
        while (true)
        {
            if (!isFollowingPlayer)
            {
                // Genera una posición aleatoria en el NavMesh
                Vector3 randomDestination = ObtenerPosicionAleatoriaEnNavMesh();
                navMeshAgent.SetDestination(randomDestination);

                // Espera un tiempo antes de generar una nueva posición aleatoria
                yield return new WaitForSeconds(Random.Range(3f, 8f));
            }
            else
            {
                // Espera un tiempo antes de volver a verificar la posición del jugador
                yield return new WaitForSeconds(1f);
            }
        }
    }

    // Función para obtener una posición aleatoria en el NavMesh
    private Vector3 ObtenerPosicionAleatoriaEnNavMesh()
    {
        NavMeshHit hit;
        Vector3 randomPosition = Vector3.zero;

        // Intenta obtener una posición aleatoria en el NavMesh
        if (NavMesh.SamplePosition(new Vector3(Random.Range(-10f, 10), 0f, Random.Range(-10, 10)), out hit, 20f, NavMesh.AllAreas))
        {
            randomPosition = hit.position;
        }

        return randomPosition;
    }
}
