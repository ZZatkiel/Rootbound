using UnityEngine;

public class HitboxAtaque : MonoBehaviour
{
    [Header("Configuración de Daño")]
    public float dañoAtaque = 10f;
    public float tiempoEntreAtaques = 1f; // Cooldown de 1 segundo

    // Bandera para controlar el tiempo entre ataques
    private bool puedeAtacar = true;

    [Header("Tags Objetivo")]
    public string tagObjetivo1 = "Player";
    public string tagObjetivo2 = "Arbol";

    // Referencia al script de persecución en el objeto padre (el enemigo)
    private PersecucionEnemigo persecucionScript;

    void Start()
    {
        persecucionScript = GetComponentInParent<PersecucionEnemigo>();
        if (persecucionScript == null)
        {
            Debug.LogError("HitboxAtaque no encontró PersecucionEnemigo en los padres.");
        }
    }

    // CAMBIO CLAVE: Usamos OnTriggerStay para detectar colisión continua
    void OnTriggerStay(Collider other)
    {
        // 1. Verificación de lógica de ataque
        if (!puedeAtacar || !EsObjetivoValido(other.gameObject))
        {
            return;
        }

        // 2. Intentar obtener el script de Salud
        Salud saludObjetivo = other.GetComponent<Salud>();

        if (saludObjetivo != null)
        {
            // --- INICIO DEL CICLO DE ATAQUE ---

            // a) Aplicar daño instantáneo
            saludObjetivo.RecibirDano(dañoAtaque);
            Debug.Log(gameObject.transform.root.name + " golpeó a " + other.gameObject.name + " e hizo " + dañoAtaque + " de daño.");

            // b) Iniciar el Cooldown y la detención
            puedeAtacar = false;

            // Detener el movimiento mientras dura el cooldown
            if (persecucionScript != null)
            {
                persecucionScript.SetAttacking(true);
            }

            // Programar la reactivación del ataque y el movimiento
            Invoke("RestablecerAtaque", tiempoEntreAtaques);
        }
    }

    // Usamos OnTriggerExit para asegurarnos de que el enemigo puede volver a moverse
    // si el objetivo se aleja antes de que termine el cooldown.
    void OnTriggerExit(Collider other)
    {
        if (EsObjetivoValido(other.gameObject) && persecucionScript != null)
        {
            // Si el objetivo se va, el enemigo debería dejar de estar en modo ataque
            // para empezar a perseguir de nuevo (si puede).
            persecucionScript.SetAttacking(false);
        }
    }

    bool EsObjetivoValido(GameObject obj)
    {
        return obj.CompareTag(tagObjetivo1) ||
               obj.CompareTag(tagObjetivo2);
    }

    void RestablecerAtaque()
    {
        puedeAtacar = true;

        // El script de persecución debe decidir si se mueve o se queda quieto
        // Si el objetivo sigue dentro del trigger, OnTriggerStay llamará de nuevo al ataque
        // en el siguiente frame (si el objetivo sigue ahí).
        if (persecucionScript != null)
        {
            persecucionScript.SetAttacking(false);
        }
    }
}