using UnityEngine;
using UnityEngine.InputSystem;

public class ControlPersonaje : MonoBehaviour
{
    // VARIABLES DE ATAQUE
    public GameObject hitboxAtaque;
    public float duracionAtaque = 0.2f;
    private bool estaGolpeando = false;

    // VARIABLES DE MOVIMIENTO Y SALTO
    public float fuerzaSalto = 7f;
    public Rigidbody rb;

    // 💡 SOLUCIÓN AL SALTO INFINITO: Objeto que debe estar en los pies
    public Transform groundCheckPoint;

    // VARIABLES PARA VERIFICACIÓN DE SUELO
    public float distanciaSuelo = 0.1f; // Radio pequeño para precisión
    public LayerMask capaSuelo; // La Layer 'Ground' asignada al Plane
    private bool estaEnSuelo;

    void Start()
    {
        // Intenta obtener el Rigidbody automáticamente
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // *** 1. VERIFICACIÓN DE SUELO ***
        VerificarSuelo();

        // *** 2. LÓGICA DE SALTO (Tecla Espacio) ***
        // Solo salta si la tecla es presionada Y está en el suelo
        if (Keyboard.current.spaceKey.wasPressedThisFrame && estaEnSuelo)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }

        // *** 3. LÓGICA DE ATAQUE (Tecla Q) ***
        if (Keyboard.current.qKey.wasPressedThisFrame && !estaGolpeando)
        {
            IniciarGolpe();
        }
    }

    // Método que usa el punto de chequeo dedicado para verificar el suelo
    void VerificarSuelo()
    {
        // Si el GroundCheckPoint no está asignado, no hacemos nada (seguridad)
        if (groundCheckPoint == null) return;

        // Usamos la posición del objeto GroundCheckPoint (que está en los pies)
        estaEnSuelo = Physics.CheckSphere(
            groundCheckPoint.position,
            distanciaSuelo,
            capaSuelo
        );
    }

    // MÉTODOS DE ATAQUE
    void IniciarGolpe()
    {
        if (hitboxAtaque == null) return;

        estaGolpeando = true;
        hitboxAtaque.SetActive(true);
        Invoke("FinalizarGolpe", duracionAtaque);
    }

    void FinalizarGolpe()
    {
        hitboxAtaque.SetActive(false);
        estaGolpeando = false;
    }

    // Método para DEPURACIÓN: Dibuja la esfera de detección en el Editor
    private void OnDrawGizmos()
    {
        if (groundCheckPoint == null) return;

        Gizmos.color = estaEnSuelo ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, distanciaSuelo);
    }
}