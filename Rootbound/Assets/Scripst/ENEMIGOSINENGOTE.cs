using UnityEngine;
using UnityEngine.AI; // ¡Importante! Necesario para NavMeshAgent

public class EnemyAI : MonoBehaviour
{
    public float lookRadius = 10f; // Distancia en la que detecta al jugador
    Transform target;              // Referencia al jugador
    NavMeshAgent agent;            // Referencia al componente de navegación

    void Start()
    {
        // 1. Encontrar al Jugador (asumiendo que tiene la etiqueta "Player")
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform; // Guardar la posición del jugador
        }

        // 2. Obtener el componente Nav Mesh Agent
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target != null)
        {
            // Calcular la distancia al jugador
            float distance = Vector3.Distance(target.position, transform.position);

            // Si el jugador está dentro del radio de visión...
            if (distance <= lookRadius)
            {
                // Ordenar al NavMeshAgent que vaya a la posición del jugador
                agent.SetDestination(target.position);

                // Opcional: Rotar para mirar al jugador
                Vector3 direction = (target.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }
    }
}