using UnityEngine;

public class PersecucionEnemigo : MonoBehaviour
{
    [Header("Persecución")]
    public float velocidadMovimiento = 3.5f;
    public float rangoPersecucionArbol = 5f;
    public float rangoPrioridadJugador = 8f;
    // Factor de fuerza para la rotación y movimiento (mayor en rb no cinemático)
    public float fuerzaRotacion = 10f;

    private Transform objetivoActual;
    private Rigidbody rb;

    [Header("Tags de Objetivos")]
    public string tagJugador = "Player";
    public string tagArbol = "Arbol";

    // NUEVA BANDERA: Controlada por HitboxAtaque.cs
    private bool estaAtacando = false;
    private bool estaEnColisionConJugador = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("CortaDistancia necesita un Rigidbody.");
        }

        if (rb != null)
        {
            // Ajustamos el amortiguamiento a un valor bajo para que no frene el movimiento
            rb.linearDamping = 0.5f; // Valor bajo, pero no cero, para mantener estabilidad.
            rb.angularDamping = 0.5f;
        }
    }

    // Llamada por HitboxAtaque.cs para detener/reanudar el movimiento.
    public void SetAttacking(bool isAttacking)
    {
        estaAtacando = isAttacking;
        // La lógica de detener la velocidad está en FixedUpdate()
    }


    void FixedUpdate()
    {
        if (rb == null) return;

        // Si está atacando, detener todo movimiento y salir
        if (estaAtacando)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            return;
        }

        // --- LÓGICA DE PERSECUCIÓN ---
        BuscarObjetivo();

        if (objetivoActual != null)
        {
            PerseguirObjetivo(objetivoActual);
        }
        else if (!estaEnColisionConJugador)
        {
            // Frena al enemigo si no hay objetivo.
            // Si el drag es alto, esto no es estrictamente necesario, pero es seguro.
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    // Lógica de colisión del Rigidbody (Para anular el empuje)
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagJugador))
        {
            // Anulamos la velocidad para que el jugador no pueda empujar al enemigo.
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            estaEnColisionConJugador = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagJugador))
        {
            estaEnColisionConJugador = false;
        }
    }

    void BuscarObjetivo()
    {
        objetivoActual = null;
        GameObject jugadorGO = GameObject.FindWithTag(tagJugador);
        GameObject arbolGO = GameObject.FindWithTag(tagArbol);

        float distanciaJugador = float.MaxValue;
        float distanciaArbol = float.MaxValue;

        if (jugadorGO != null)
        {
            distanciaJugador = Vector3.Distance(transform.position, jugadorGO.transform.position);
        }
        if (arbolGO != null)
        {
            distanciaArbol = Vector3.Distance(transform.position, arbolGO.transform.position);
        }

        // PRIORIDAD 1: Jugador cerca (prioridad absoluta)
        if (jugadorGO != null && distanciaJugador <= rangoPrioridadJugador)
        {
            objetivoActual = jugadorGO.transform;
            return;
        }

        // PRIORIDAD 2: Jugador lejos o no disponible. Perseguir al más cercano dentro de rango.
        bool arbolEsAccesible = arbolGO != null && distanciaArbol <= rangoPersecucionArbol;
        bool jugadorEsAccesible = jugadorGO != null;

        if (arbolEsAccesible && jugadorEsAccesible)
        {
            objetivoActual = (distanciaJugador < distanciaArbol) ? jugadorGO.transform : arbolGO.transform;
        }
        else if (arbolEsAccesible)
        {
            objetivoActual = arbolGO.transform;
        }
        else if (jugadorEsAccesible)
        {
            objetivoActual = jugadorGO.transform;
        }
    }

    void PerseguirObjetivo(Transform objetivo)
    {
        Vector3 direccion = (objetivo.position - transform.position).normalized;

        // Rotación
        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.fixedDeltaTime * fuerzaRotacion);

        // --- CAMBIO CLAVE: Usamos linearVelocity para movimiento no cinemático ---
        // Esto le da control físico al Rigidbody.
        rb.linearVelocity = direccion * velocidadMovimiento;
    }
}