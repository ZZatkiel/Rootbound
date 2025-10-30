// Archivo: HitboxEnemigo.cs (Versión Final LIMPIA)
using UnityEngine;
// No necesitamos 'System.Collections' porque eliminamos la Coroutine

[RequireComponent(typeof(Collider))]
public class HitboxEnemigo : MonoBehaviour
{
    [Header("Daño")]
    public int dano = 10;
    // Eliminamos: public float cooldownDano
    // Eliminamos: private bool puedeDanar

    // ... (Método Start sin cambios)

    private void OnTriggerStay(Collider other)
    {
        // 1. Verifica la etiqueta (ya no chequeamos 'puedeDanar')
        if (other.CompareTag("Player"))
        {
            // Busca el script de control del jugador
            ControlPersonaje personaje = other.GetComponentInParent<ControlPersonaje>();

            if (personaje != null && !personaje.EstaMuerto)
            {
                // El daño se aplicará inmediatamente si el jugador NO es invulnerable.
                personaje.RecibirDano(dano);

                // 🔑 CLAVE: Si el golpe solo debe ser ÚNICO por activación de animación,
                // puedes forzar la desactivación del Hitbox inmediatamente para 
                // asegurar que no haya repeticiones de daño si el jugador sigue dentro.
                // gameObject.SetActive(false); 
            }
        }
    }

    // Eliminamos el IEnumerator AplicarCooldown()
}