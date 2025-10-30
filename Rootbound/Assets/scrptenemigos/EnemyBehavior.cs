using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform target;  // El objetivo (el árbol o el jugador)
    public float speed = 2f;  // Velocidad de movimiento
    public int damage = 10;   // Daño al árbol o jugador

    private void Update()
    {
        // Si hay un objetivo asignado (el árbol o el jugador)
        if (target != null)
        {
            // Mover al enemigo hacia el objetivo
            Vector3 direction = target.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si el enemigo colisiona con el árbol o el jugador, inflige daño
        if (collision.gameObject.CompareTag("Tree"))
        {
            Debug.Log("El árbol está siendo atacado");
            // Aquí puedes agregar lógica para reducir la vida del árbol si lo deseas.
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("El jugador está siendo atacado");
            // Aquí puedes agregar lógica para reducir la vida del jugador si lo deseas.
        }
    }
}
