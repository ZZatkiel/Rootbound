using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class ControlPersonaje : MonoBehaviour
{
    [Header("Ataque")]
    public GameObject hitboxAtaque;
    public float duracionAtaque = 0.2f;

    [Header("Salud")]
    public int saludMaxima = 100;

    // [ELIMINADO] Header("Invulnerabilidad")
    // [ELIMINADO] public float tiempoInvulnerabilidad = 0.5f;

    [Header("Control de Juego")]
    public MonoBehaviour spawnManagerScript;

    [Header("Cámara")]
    public CinemachineVirtualCamera playerCamera;

    // --- Referencias de StarterAssets ---
    private ThirdPersonController controllerScript;
    private CharacterController cc;
    private StarterAssetsInputs inputScript;
    private Animator anim;
    private Rigidbody rb;

    private int saludActual;
    private bool estaGolpeando = false;
    private bool estaMuerto = false;
    // [ELIMINADO] private bool esInvulnerable = false;
    public bool EstaMuerto => estaMuerto;

    void Start()
    {
        // OBTENCIÓN DE REFERENCIAS CENTRALIZADA
        controllerScript = GetComponentInParent<ThirdPersonController>();
        cc = GetComponentInParent<CharacterController>();
        inputScript = GetComponentInParent<StarterAssetsInputs>();
        anim = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody>();

        saludActual = saludMaxima;
        Debug.Log($"ControlPersonaje: Inicializado. Salud Máxima: {saludMaxima}");
    }

    void Update()
    {
        if (estaMuerto) return;

        if (Keyboard.current.qKey.wasPressedThisFrame && !estaGolpeando)
        {
            IniciarGolpe();
        }
    }

    /// <summary>
    /// Aplica daño al personaje sin chequeo de invulnerabilidad.
    /// </summary>
    public void RecibirDano(int cantidadDano)
    {
        if (estaMuerto)
        {
            Debug.Log("JUGADOR: Daño ignorado. Ya está muerto.");
            return;
        }

        // [ELIMINADO] Chequeo de esInvulnerable

        saludActual -= cantidadDano;

        Debug.Log($"JUGADOR: Recibió {cantidadDano} de daño. Salud restante: {saludActual}");

        if (saludActual <= 0)
        {
            saludActual = 0;
            Morir();
        }
        // [ELIMINADO] Lógica de invulnerabilidad post-golpe.
    }

    // [ELIMINADO] private void RestablecerInvulnerabilidad()

    // --- Lógica de Ataque (sin cambios) ---

    void IniciarGolpe()
    {
        if (hitboxAtaque == null || estaMuerto) return;

        estaGolpeando = true;
        CancelInvoke(nameof(FinalizarGolpe));

        hitboxAtaque.SetActive(true);
        Invoke(nameof(FinalizarGolpe), duracionAtaque);
    }

    void FinalizarGolpe()
    {
        if (hitboxAtaque != null)
        {
            hitboxAtaque.SetActive(false);
        }
        estaGolpeando = false;
    }

    // --- Lógica de Muerte (sin cambios) ---

    void Morir()
    {
        if (estaMuerto) return;
        estaMuerto = true;
        Debug.Log("JUGADOR: ¡HA MUERTO!");

        // Limpieza de estados
        CancelInvoke(nameof(FinalizarGolpe));
        FinalizarGolpe();
        // [ELIMINADO] CancelInvoke(nameof(RestablecerInvulnerabilidad));

        // =================================================================
        // 1. FREEZE DE POSICIÓN Y DESACTIVACIÓN DE CONTROL
        // =================================================================

        // Desactivar la entrada primero
        if (inputScript != null)
        {
            inputScript.move = Vector2.zero;
            inputScript.jump = false;
            inputScript.enabled = false;
        }

        // Deshabilitar scripts de movimiento
        if (controllerScript != null)
        {
            // 🛑 Silencia el AudioSource del controlador antes de deshabilitarlo
            AudioSource controllerAudioSource = controllerScript.GetComponent<AudioSource>();
            if (controllerAudioSource != null)
            {
                controllerAudioSource.Stop();
                controllerAudioSource.enabled = false;
            }

            controllerScript.enabled = false;
        }

        if (cc != null) cc.enabled = false;

        // Detener animación de forma limpia antes de deshabilitar
        if (anim != null)
        {
            anim.SetBool("IsGrounded", true);
            anim.SetFloat("Speed", 0f);
            anim.enabled = false;
        }

        // Desactivar este script
        this.enabled = false;

        // =================================================================
        // 2. IGNORAR ENEMIGO Y SILENCIO DE AUDIO
        // =================================================================

        // 🛑 IGNORAR ENEMIGO: CAMBIAR LA ETIQUETA EN EL AVATAR 🛑
        if (gameObject.CompareTag("Player"))
        {
            gameObject.tag = "Untagged";
        }

        // Silenciar todos los AudioSources restantes en la jerarquía
        AudioSource[] audioSources = GetComponentsInParent<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            source.Stop();
            source.volume = 0f;
            source.enabled = false;
        }

        // Desactivar Rigidbody (evita empujes)
        if (rb != null)
        {
            if (!rb.isKinematic)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            rb.isKinematic = true;
        }

        // =================================================================
        // 3. CONGELACIÓN DE CÁMARA Y BLOQUEO DE JUEGO
        // =================================================================

        // Detiene el Spawn
        if (spawnManagerScript != null) spawnManagerScript.enabled = false;

        // Desvincular cámara para vista libre
        if (playerCamera != null)
        {
            playerCamera.Follow = null;
            playerCamera.LookAt = null;
            playerCamera.enabled = true;
        }

        // Desactivar Renderizadores y Colliders (hacer al jugador invisible)
        Collider[] allColliders = GetComponentsInChildren<Collider>();
        foreach (Collider col in allColliders) col.enabled = false;

        // Desactivar Skinned Mesh Renderer (el modelo del personaje)
        SkinnedMeshRenderer skinnedRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        if (skinnedRenderer != null) skinnedRenderer.enabled = false;

        // Desactivar Mesh Renderer (si el avatar usa Mesh Renderer en lugar de Skinned)
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null) renderer.enabled = false;
    }
}