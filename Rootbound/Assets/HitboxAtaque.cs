using UnityEngine;
using System.Collections;

public class HitboxAtaque : MonoBehaviour
{
    [Header("Parámetros de Ataque")]
    public float dañoAtaque = 10f;
    public float tiempoEntreAtaques = 1f; // Ataca cada 1 segundo
    private bool puedeAtacar = true;

    private PersecucionEnemigo scriptPrincipal;

    void Start()
    {
        // Obtener la referencia al script del padre (CortaDistancia)
        scriptPrincipal = GetComponentInParent<PersecucionEnemigo>();
        if (scriptPrincipal == null)
        {
            Debug.LogError("HitboxAtaque requiere el script PersecucionEnemigo en el objeto padre.");
        }
    }

    // El jugador entra en el área de ataque (Trigger)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scriptPrincipal.jugadorDetectado = true;
            // Inicia el ciclo de ataque si es posible
            if (puedeAtacar)
            {
                StartCoroutine(CicloDeAtaque(other.gameObject));
            }
        }
    }

    // El jugador permanece en el área de ataque (Trigger)
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && puedeAtacar)
        {
            // El ciclo de ataque solo permite un ataque por segundo
            StartCoroutine(CicloDeAtaque(other.gameObject));
        }
    }

    // El jugador sale del área de ataque (Trigger)
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scriptPrincipal.jugadorDetectado = false;
        }
    }

    IEnumerator CicloDeAtaque(GameObject jugador)
    {
        puedeAtacar = false;

        // 1. Ejecutar el Ataque
        Debug.Log("CortaDistancia ATACÓ al jugador por " + dañoAtaque + " daño. Próximo ataque en 1s.");

        // **NOTA: Aquí debes implementar la lógica para que el jugador reciba daño.**
        // Ejemplo: jugador.GetComponent<ScriptDelJugador>().RecibirDaño(dañoAtaque);

        // 2. Esperar el tiempo de recarga (1s)
        yield return new WaitForSeconds(tiempoEntreAtaques);

        // 3. Permitir el siguiente ataque
        puedeAtacar = true;
    }
}