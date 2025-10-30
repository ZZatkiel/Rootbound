using UnityEngine;
using System.Collections;

public class HitboxAtaque : MonoBehaviour
{
    [Header("Parámetros de Ataque")]
    public float dañoAtaque = 10f;
    public float tiempoEntreAtaques = 1f; // Cooldown: 1 segundo
    private bool puedeAtacar = true; // Control del cooldown
    private PersecucionEnemigo scriptPrincipal;

    void Start()
    {
        scriptPrincipal = GetComponentInParent<PersecucionEnemigo>();
        if (scriptPrincipal == null)
        {
            Debug.LogError("HitboxAtaque requiere el script PersecucionEnemigo en el objeto padre.");
        }

        // Si el objeto 'HitboxAtaque' (o su Collider) está deshabilitado por defecto, 
        // ¡DEBE ESTAR HABILITADO AHORA! Si estuviera deshabilitado, OnTriggerStay nunca se llamaría.
        // Asegúrate de que el GameObject 'HitboxAtaque' está ACTIVO en el Editor.
    }

    private void OnTriggerStay(Collider other)
    {
        // Solo verificamos si el objetivo es un Tag válido (Player o Arbol)
        bool esJugador = other.CompareTag(scriptPrincipal.tagJugador);
        bool esArbol = other.CompareTag(scriptPrincipal.tagArbol);

        if ((esJugador || esArbol) && puedeAtacar)
        {
            // Ejecutar el ataque si es un objetivo y no está en cooldown
            StartCoroutine(CicloDeAtaque(other.gameObject));
        }
    }

    IEnumerator CicloDeAtaque(GameObject target)
    {
        puedeAtacar = false;

        // --- 1. ACTIVACIÓN Y EJECUCIÓN DEL ATAQUE ---

        // LOG DE CONFIRMACIÓN: Se ejecutará cada 1s si el objetivo está en rango.
        Debug.Log($"---> [ATAQUE CORTADISTANCIA EJECUTADO] Objetivo: {target.name}. Daño: {dañoAtaque}. Inicio del golpe: {Time.time}");

        // --- 2. COOLDOWN ---
        yield return new WaitForSeconds(tiempoEntreAtaques);

        // --- 3. FINALIZACIÓN DEL ATAQUE ---
        Debug.Log($"<--- [COOLDOWN FINALIZADO]. {target.name} puede ser atacado de nuevo.");

        // Permitir el siguiente ataque
        puedeAtacar = true;
    }
}