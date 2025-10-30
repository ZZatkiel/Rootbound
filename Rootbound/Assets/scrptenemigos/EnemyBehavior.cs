using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform target;  // El objetivo (el �rbol o el jugador)
    public float speed = 2f;  // Velocidad de movimiento
    public int damage = 10;   // Da�o al �rbol o jugador

    private void Update()
    {
        // Si hay un objetivo asignado (el �rbol o el jugador)
        if (target != null)
        {
            // Mover al enemigo hacia el objetivo
            Vector3 direction = target.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si el enemigo colisiona con el �rbol o el jugador, inflige da�o
        if (collision.gameObject.CompareTag("Tree"))
        {
            Debug.Log("El �rbol est� siendo atacado");
            // Aqu� puedes agregar l�gica para reducir la vida del �rbol si lo deseas.
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("El jugador est� siendo atacado");
            // Aqu� puedes agregar l�gica para reducir la vida del jugador si lo deseas.
        }
    }
}
