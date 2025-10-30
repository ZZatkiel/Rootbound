using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovimientoDelPersonaje : MonoBehaviour
{
    [Header("Atributos Base")]
    public float vida = 100f;
    public float daño = 15f;
    public float velocidadDeAtaque = 1.2f;
    [Range(0f, 100f)] public float critico = 10f;
    public float defensa = 5f;

    [Header("Movimiento")]
    public float velocidad = 5f;
    public float gravedad = -9.8f;
    public float fuerzaSalto = 3f;

    [Header("Referencia cámara (si está vacía usa Camera.main)")]
    public Transform camTransform;

    private CharacterController controller;
    private Vector3 velocidadVertical;
    private bool estaEnSuelo;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (camTransform == null && Camera.main != null)
            camTransform = Camera.main.transform;
    }

    void Update()
    {
        Mover();
    }

    void Mover()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // Input en plano local (sin Y)
        Vector3 input = new Vector3(x, 0f, z);

        if (input.magnitude > 1f) input.Normalize();

        Vector3 movimientoWorld;

        if (camTransform != null)
        {
            // usamos la dirección de la cámara (proyectada en XZ)
            Vector3 camForward = camTransform.forward;
            camForward.y = 0f;
            camForward.Normalize();

            Vector3 camRight = camTransform.right;
            camRight.y = 0f;
            camRight.Normalize();

            // movimiento en world relativo a la camara
            movimientoWorld = camRight * input.x + camForward * input.z;
        }
        else
        {
            // fallback: relativo al propio transform del cubo
            movimientoWorld = transform.right * input.x + transform.forward * input.z;
        }

        // mover
        controller.Move(movimientoWorld * velocidad * Time.deltaTime);

        // --- GRAVEDAD Y SALTO ---
        estaEnSuelo = controller.isGrounded;
        if (estaEnSuelo && velocidadVertical.y < 0f)
            velocidadVertical.y = -2f;

        if (Input.GetButtonDown("Jump") && estaEnSuelo)
            velocidadVertical.y = Mathf.Sqrt(fuerzaSalto * -2f * gravedad);

        velocidadVertical.y += gravedad * Time.deltaTime;
        controller.Move(velocidadVertical * Time.deltaTime);
    }
}
