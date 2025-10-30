using UnityEngine;
using System.Collections;

public class HitboxAtaque : MonoBehaviour
{
    [Header("Par�metros de Ataque")]
    public float da�oAtaque = 10f;
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

    // El jugador entra en el �rea de ataque (Trigger)
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

    // El jugador permanece en el �rea de ataque (Trigger)
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && puedeAtacar)
        {
            // El ciclo de ataque solo permite un ataque por segundo
            StartCoroutine(CicloDeAtaque(other.gameObject));
        }
    }

    // El jugador sale del �rea de ataque (Trigger)
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
        Debug.Log("CortaDistancia ATAC� al jugador por " + da�oAtaque + " da�o. Pr�ximo ataque en 1s.");

        // **NOTA: Aqu� debes implementar la l�gica para que el jugador reciba da�o.**
        // Ejemplo: jugador.GetComponent<ScriptDelJugador>().RecibirDa�o(da�oAtaque);

        // 2. Esperar el tiempo de recarga (1s)
        yield return new WaitForSeconds(tiempoEntreAtaques);

        // 3. Permitir el siguiente ataque
        puedeAtacar = true;
    }
}