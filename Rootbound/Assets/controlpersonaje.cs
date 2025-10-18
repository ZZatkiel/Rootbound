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

    // Asigna el objeto que contiene el script SpawnManager
    [Header("Control de Juego")]
    public MonoBehaviour spawnManagerScript;

    // Asigna la cámara virtual (PlayerFollowCamera) y el objeto Main Camera
    [Header("Cámara Fantasma")]
    public CinemachineVirtualCamera playerCamera;
    public GameObject mainCameraObject; // Objeto Main Camera

    private int saludActual;
    private bool estaGolpeando = false;
    private bool estaMuerto = false;
    public bool EstaMuerto => estaMuerto;

    void Start()
    {
        saludActual = saludMaxima;
        Debug.Log("Salud inicial del jugador: " + saludActual);

        // Al inicio, aseguramos que el script fantasma esté deshabilitado.
        if (mainCameraObject != null)
        {
            CamaraFantasma camFantasma = mainCameraObject.GetComponent<CamaraFantasma>();
            if (camFantasma != null) camFantasma.enabled = false;
        }
    }

    public void RecibirDano(int cantidadDano)
    {
        if (estaMuerto) return;
        saludActual -= cantidadDano;
        if (saludActual <= 0) Morir();
    }

    void Morir()
    {
        if (estaMuerto) return;
        estaMuerto = true;

        Debug.Log("¡Modo Fantasma Activado! Control transferido a la cámara.");

        // =================================================================
        // 1. BLOQUEO DE MOVIMIENTO DEL PERSONAJE Y DESTRUCCIÓN
        // =================================================================

        // Desactiva el motor de movimiento principal
        CharacterController cc = GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        // Desactiva la entrada de usuario para el jugador
        PlayerInput playerInput = GetComponent<PlayerInput>();
        if (playerInput != null) playerInput.enabled = false;

        // Desactiva scripts de movimiento Starter Assets
        // Desactiva scripts de movimiento Starter Assets
        StarterAssetsInputs inputScript = GetComponent<StarterAssetsInputs>();
        if (inputScript != null)
        {
            inputScript.enabled = false;
        }

        ThirdPersonController controllerScript = GetComponent<ThirdPersonController>();
        if (controllerScript != null)
        {
            controllerScript.enabled = false;
        }

        // Desactiva la etiqueta para que el enemigo deje de buscarlo inmediatamente
        gameObject.tag = "Untagged";

        // =================================================================
        // 2. BLOQUEO DEL SPAWN Y PREPARACIÓN DE CÁMARA
        // =================================================================

        // 🔑 Detiene el spawn
        if (spawnManagerScript != null)
        {
            spawnManagerScript.enabled = false;
        }

        // Prepara la cámara para el modo fantasma
        if (playerCamera != null)
        {
            playerCamera.Follow = null; // Deja de seguir al jugador
            playerCamera.LookAt = null; // Deja de mirar al jugador
            playerCamera.enabled = true;
        }

        // 🔑 ACTIVA EL SCRIPT DE MOVIMIENTO FANTASMA EN LA CÁMARA PRINCIPAL
        if (mainCameraObject != null)
        {
            CamaraFantasma camFantasma = mainCameraObject.GetComponent<CamaraFantasma>();
            if (camFantasma != null) camFantasma.enabled = true;

            // Asegura que el CinemachineBrain esté activo para el look
            CinemachineBrain brain = mainCameraObject.GetComponent<CinemachineBrain>();
            if (brain != null) brain.enabled = true;
        }

        // =================================================================
        // 3. DETENCIÓN DE FÍSICA Y DESAPARICIÓN DEL MODELO
        // =================================================================

        // Detener y deshabilitar el Rigidbody (Soluciona la advertencia CS0618)
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        // Desactiva el Collider (para evitar el error del enemigo)
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        // Desactivar el Renderizador (El modelo desaparece)
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null) renderer.enabled = false;

        SkinnedMeshRenderer skinnedRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        if (skinnedRenderer != null) skinnedRenderer.enabled = false;

        this.enabled = false;

        // Destrucción final del GameObject (Soluciona la persecución)
        Destroy(gameObject, 0.1f);
    }

    void Update()
    {
        if (estaMuerto) return;

        // Lógica de ataque
        if (Keyboard.current.qKey.wasPressedThisFrame && !estaGolpeando)
        {
            IniciarGolpe();
        }
    }

    void IniciarGolpe()
    {
        if (hitboxAtaque == null || estaMuerto) return;

        estaGolpeando = true;
        hitboxAtaque.SetActive(true);
        Invoke(nameof(FinalizarGolpe), duracionAtaque);
    }

    void FinalizarGolpe()
    {
        hitboxAtaque.SetActive(false);
        estaGolpeando = false;
    }
}