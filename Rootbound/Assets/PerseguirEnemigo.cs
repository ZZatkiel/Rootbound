using UnityEngine;

public class PersecucionEnemigo : MonoBehaviour
{
    [Header("Persecución")]
    public float velocidadMovimiento = 3.5f;
    public float rangoPersecucionArbol = 5f;
    private Transform objetivoActual;
    private Rigidbody rb;

    [Header("Tags de Objetivos")]
    public string tagJugador = "Player";
    public string tagArbol = "Arbol";

    private bool estaEnColisionConJugador = false; // Bandera para gestionar el anti-empuje

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("CortaDistancia necesita un Rigidbody.");
        }

        if (rb != null)
        {
            // Corrigiendo propiedades obsoletas y configurando anti-freno
            rb.linearDamping = 0f;       // Reemplaza .drag
            rb.angularDamping = 0.05f;   // Reemplaza .angularDrag
        }
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        BuscarObjetivo();

        if (objetivoActual != null)
        {
            PerseguirObjetivo(objetivoActual);
        }
        // Aseguramos que el enemigo se detenga si no hay objetivo y no está colisionando.
        else if (!estaEnColisionConJugador)
        {
            rb.linearVelocity = Vector3.zero; // Reemplaza .velocity
            rb.angularVelocity = Vector3.zero;
        }
    }

    // Lógica de colisión del Rigidbody (Para anular el empuje)
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagJugador))
        {
            Debug.Log("hola");
            
            // Anulamos la velocidad para que el jugador no pueda empujar al enemigo.
        }// El rb.MovePosition en PerseguirObjetivo lo moverá inmediatamente a la posición deseada.e;
        
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
        // 1. PRIORIDAD MÁXIMA: Jugador
        GameObject jugadorGO = GameObject.FindWithTag(tagJugador);
        if (jugadorGO != null)
        {
            objetivoActual = jugadorGO.transform;
            return;
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
        objetivoActual = null;
    }

    void PerseguirObjetivo(Transform objetivo)
    {
        Vector3 direccion = (objetivo.position - transform.position).normalized;

        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.fixedDeltaTime * 10f);

        // Movimiento físico/cinético usando Rigidbody.MovePosition
        Vector3 nuevaPosicion = rb.position + direccion * velocidadMovimiento * Time.fixedDeltaTime;
        rb.MovePosition(nuevaPosicion);
    }
}