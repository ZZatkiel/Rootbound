using UnityEngine;

public class PersecucionEnemigo : MonoBehaviour
{
    [Header("Salud")]
    public float saludActual = 100f;
    private bool estaVivo = true;

    [Header("Persecución (Transformación)")]
    public float velocidadMovimiento = 3.5f;
    private Transform objetivoJugador;
    private Rigidbody rb;

    // Variable para que el HitboxAtaque pueda referenciar al jugador
    [HideInInspector] public bool jugadorDetectado = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Buscar al jugador por Tag
        GameObject jugadorGO = GameObject.FindWithTag("Player");
        if (jugadorGO != null)
        {
            objetivoJugador = jugadorGO.transform;
        }
        else
        {
            Debug.LogError("PersecucionEnemigo: No se encontró un objeto con el tag 'Player'.");
        }
    }

    void FixedUpdate() // Usamos FixedUpdate para la lógica de Rigidbody
    {
        if (!estaVivo || objetivoJugador == null || rb == null) return;

        PerseguirJugador();
    }

    void PerseguirJugador()
    {
        // 1. Calcular la dirección hacia el jugador
        Vector3 direccion = (objetivoJugador.position - transform.position).normalized;

        // 2. Rotar para mirar al jugador (opcional)
        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.fixedDeltaTime * 10f);

        // 3. Mover usando el Rigidbody
        Vector3 nuevaPosicion = rb.position + direccion * velocidadMovimiento * Time.fixedDeltaTime;
        rb.MovePosition(nuevaPosicion);
    }

    // --- Métodos de Salud ---

    public void RecibirDaño(float daño)
    {
        if (!estaVivo) return;

        saludActual -= daño;
        if (saludActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        estaVivo = false;
        // Detener el movimiento forzadamente si el Rigidbody no es Kinematic
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        Debug.Log(gameObject.name + " ha sido destruido.");
        Destroy(gameObject, 3f);
    }
}