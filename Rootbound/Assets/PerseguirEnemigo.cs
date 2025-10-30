using UnityEngine;

public class PersecucionEnemigo : MonoBehaviour
{
    [Header("Salud")]
    public float saludActual = 100f;
    private bool estaVivo = true;

    [Header("Persecución")]
    public float velocidadMovimiento = 3.5f;
    public float rangoPersecucionArbol = 5f; // Distancia máxima para perseguir al árbol
    private Transform objetivoActual;
    private Rigidbody rb;

    [Header("Tags de Objetivos")]
    public string tagJugador = "Player";
    public string tagArbol = "Arbol";

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("CortaDistancia necesita un Rigidbody.");
        }
    }

    void FixedUpdate()
    {
        if (!estaVivo || rb == null) return;

        // Llama a la lógica de selección de objetivo en cada frame de física
        BuscarObjetivo();

        if (objetivoActual != null)
        {
            PerseguirObjetivo(objetivoActual);
        }
    }

    void BuscarObjetivo()
    {
        // 1. PRIORIDAD MÁXIMA: Jugador
        GameObject jugadorGO = GameObject.FindWithTag(tagJugador);
        if (jugadorGO != null)
        {
            objetivoActual = jugadorGO.transform;
            return; // Si encontramos al jugador, perseguimos al jugador y terminamos la búsqueda.
        }

        // 2. PRIORIDAD SECUNDARIA: Árbol (solo si está dentro del rango)
        GameObject arbolGO = GameObject.FindWithTag(tagArbol);

        if (arbolGO != null)
        {
            float distanciaArbol = Vector3.Distance(transform.position, arbolGO.transform.position);

            if (distanciaArbol <= rangoPersecucionArbol)
            {
                objetivoActual = arbolGO.transform;
                return;
            }
        }

        objetivoActual = null; // No hay objetivos válidos para perseguir
    }

    void PerseguirObjetivo(Transform objetivo)
    {
        Vector3 direccion = (objetivo.position - transform.position).normalized;

        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.fixedDeltaTime * 10f);

        Vector3 nuevaPosicion = rb.position + direccion * velocidadMovimiento * Time.fixedDeltaTime;
        rb.MovePosition(nuevaPosicion);
    }

    // --- Métodos de Salud (omitiendo por brevedad, asume que están aquí) ---
    public void RecibirDaño(float daño) { /* ... */ }
    void Morir() { /* ... */ }
}