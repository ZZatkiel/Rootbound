// Archivo: MovimientoEnemigo.cs (IA, Física, Persecución Continua, Animaciones)
using UnityEngine;

// Requiere Rigidbody, Collider y Animator
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class MovimientoEnemigo : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadMovimiento = 3f;
    public float distanciaAlSuelo = 0.6f;
    public LayerMask capaSuelo;
    public float rangoAtaque = 2f; // Distancia para iniciar animación de ataque

    [Header("Animación y Ataque")]
    public float tiempoEntreAtaques = 1.5f;
    private bool atacando = false;
    private float tiempoProximoAtaque;

    public GameObject hitboxAtaqueObjeto;

    [HideInInspector]
    public bool esInstanciaClonada = false;

    private Rigidbody rb;
    private Transform objetivo;
    private Animator anim;
    private bool puedeMover = true; // Controla si la IA debe intentar mover el Rigidbody

    private float vidaMaximaInicial;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        // Obtener la vida máxima del script de salud
        SaludObjeto salud = GetComponent<SaludObjeto>();
        if (salud != null)
        {
            vidaMaximaInicial = salud.vidaActual;
        }

        // Configuración inicial del Rigidbody (IMPORTANTE para evitar traspaso)
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.freezeRotation = true;

        // Asignar Collision Detection a Continuous Speculative en el Inspector
        // para mejorar la detección de colisiones rápidas.

        if (hitboxAtaqueObjeto == null)
        {
            HitboxEnemigo hitboxComp = GetComponentInChildren<HitboxEnemigo>(true);
            if (hitboxComp != null)
            {
                hitboxAtaqueObjeto = hitboxComp.gameObject;
            }
        }
    }

    void Start()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
            objetivo = jugador.transform;

        if (capaSuelo == 0)
            capaSuelo = LayerMask.GetMask("Default");

        HabilitarComponentes();
    }

    void FixedUpdate()
    {
        // 🔑 VERIFICACIÓN CRÍTICA: Objetivo no válido (null o Tag cambiada)
        if (objetivo == null || !objetivo.CompareTag("Player") || rb == null || !enabled)
        {
            DetenerMovimiento();
            atacando = false;
            return;
        }

        float distancia = Vector3.Distance(transform.position, objetivo.position);

        if (distancia <= rangoAtaque)
        {
            DetenerMovimiento();
            IntentarAtacar();
        }
        else // Persecución
        {
            // 🔑 CORRECCIÓN: Llamar a MoverHaciaJugador solo si puedeMover es true
            if (puedeMover)
            {
                MoverHaciaJugador();
            }
        }
    }

    // 🔑 CORRECCIÓN 1: Lógica de movimiento y ajuste al suelo
    void MoverHaciaJugador()
    {
        Vector3 vectorAlObjetivo = objetivo.position - transform.position;
        Vector3 direccion = vectorAlObjetivo.normalized;

        // Movimiento horizontal
        Vector3 movimiento = new Vector3(direccion.x, 0, direccion.z) * velocidadMovimiento * Time.fixedDeltaTime;
        Vector3 nuevaPosicion = rb.position + movimiento;

        // Raycast para ajustar la posición vertical al suelo (CRÍTICO para evitar traspaso)
        RaycastHit hit;
        // Lanzamos el rayo desde una posición elevada
        Vector3 puntoDeInicioRay = new Vector3(nuevaPosicion.x, nuevaPosicion.y + 1f, nuevaPosicion.z);

        // 🔑 NOTA: Usamos 2.5f como distancia máxima para una detección más precisa.
        if (Physics.Raycast(puntoDeInicioRay, -Vector3.up, out hit, 2.5f, capaSuelo))
        {
            float alturaObjetivo = hit.point.y + distanciaAlSuelo;
            nuevaPosicion.y = alturaObjetivo;
        }
        else
        {
            // Si no encuentra el suelo (ej. está en el aire o cayendo de un precipicio),
            // se deja que la gravedad del Rigidbody actúe.
        }

        rb.MovePosition(nuevaPosicion);

        // Rotación hacia el objetivo (DEBE ocurrir siempre)
        if (direccion.sqrMagnitude > 0.001f)
        {
            Quaternion rotacionDeseada = Quaternion.LookRotation(new Vector3(direccion.x, 0, direccion.z));
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, rotacionDeseada, Time.fixedDeltaTime * 5f));
        }

        // 🔑 CORRECCIÓN 2: La animación de caminar DEBE activarse aquí.
        anim.SetBool("caminar", true);
        puedeMover = true;
    }

    void DetenerMovimiento()
    {
        // 🔑 CORRECCIÓN 3: Limpiar la velocidad del Rigidbody al detenerse.
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        anim.SetBool("caminar", false);
        puedeMover = false;
    }

    void IntentarAtacar()
    {
        if (Time.time >= tiempoProximoAtaque && !atacando)
        {
            atacando = true;
            tiempoProximoAtaque = Time.time + tiempoEntreAtaques;
            anim.SetTrigger("atacar");
        }
    }

    // ---------------------------------------------------------------------
    // MÉTODOS PÚBLICOS LLAMADOS POR EL EVENTO DE ANIMACIÓN
    // ---------------------------------------------------------------------

    public void AplicarDaño()
    {
        if (hitboxAtaqueObjeto != null)
        {
            hitboxAtaqueObjeto.SetActive(true);
        }
    }

    public void TerminarAtaque()
    {
        if (hitboxAtaqueObjeto != null)
        {
            hitboxAtaqueObjeto.SetActive(false);
        }

        atacando = false;
        puedeMover = true; // 🔑 CRÍTICO: Debe poder moverse al finalizar el ataque.
    }

    // ---------------------------------------------------------------------
    // LÓGICA DE RECICLAJE
    // ---------------------------------------------------------------------

    public void ReciclarEnemigo()
    {
        SaludObjeto salud = GetComponent<SaludObjeto>();
        if (salud != null)
        {
            salud.vidaActual = vidaMaximaInicial;
        }

        atacando = false;
        puedeMover = true;
        anim.SetBool("caminar", false);

        HabilitarComponentes();

        gameObject.SetActive(false);
    }

    private void HabilitarComponentes()
    {
        this.enabled = true;
        if (GetComponent<Collider>() != null) GetComponent<Collider>().enabled = true;

        if (hitboxAtaqueObjeto != null)
        {
            hitboxAtaqueObjeto.SetActive(false);
        }
    }
}