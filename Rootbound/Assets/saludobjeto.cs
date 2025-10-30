// Archivo: SaludObjeto.cs (Actualizado para el nuevo flujo de muerte)
using UnityEngine;

public class SaludObjeto : MonoBehaviour
{
    public float vidaActual = 100f;

    private SpawnEnemigos spawnManager;

    // Referencia al script principal de lógica
    private MovimientoEnemigo movimientoEnemigo;
    private Collider enemigoCollider;

    void Start()
    {
        spawnManager = FindAnyObjectByType<SpawnEnemigos>();
        movimientoEnemigo = GetComponent<MovimientoEnemigo>();
        enemigoCollider = GetComponent<Collider>();

        if (spawnManager == null)
        {
            Debug.LogError("¡ERROR! No se encontró el SpawnEnemigos Manager en la escena.");
        }
    }

    public void RecibirDano(float cantidadDano)
    {
        if (vidaActual <= 0) return;

        vidaActual -= cantidadDano;

        Debug.Log(gameObject.name + " recibió daño. Vida restante: " + vidaActual);

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        if (vidaActual > 0) vidaActual = 0;

        Debug.Log(gameObject.name + " ha muerto. Notificando a SpawnManager.");

        // =============================================================
        // 🔑 LIMPIEZA INMEDIATA Y FORZADA
        // =============================================================

        // 1. Deshabilitar Lógica de IA/Física
        if (movimientoEnemigo != null)
        {
            movimientoEnemigo.enabled = false;
        }

        // 2. DESHABILITAR COLLIDERS Y SCRIPTS DE DAÑO EN HIJOS (Incluyendo HitboxEnemigo.cs)
        Collider[] allColliders = GetComponentsInChildren<Collider>();
        foreach (Collider col in allColliders)
        {
            if (col.enabled)
            {
                // Buscamos cualquier script de daño (como HitboxEnemigo.cs)
                MonoBehaviour[] scriptsEnCollider = col.GetComponents<MonoBehaviour>();

                foreach (MonoBehaviour script in scriptsEnCollider)
                {
                    if (script != null && script.enabled &&
                        (script.GetType().Name.Contains("HitboxEnemigo") || script.GetType().Name.Contains("Dano")))
                    {
                        script.enabled = false;
                    }
                }
                col.enabled = false; // Deshabilita el Collider
            }
        }

        // 3. DESHABILITAR EL COLLIDER PRINCIPAL
        if (enemigoCollider != null)
        {
            enemigoCollider.enabled = false;
        }

        // =============================================================
        // LÓGICA DE JUEGO (Notificación y Muerte/Reciclaje)
        // =============================================================

        if (spawnManager != null)
        {
            // El SpawnManager decide si destruirlo o inhabilitarlo.
            spawnManager.EnemigoMuerto(gameObject);
        }
        else
        {
            Destroy(gameObject, 0.05f);
        }
    }
}