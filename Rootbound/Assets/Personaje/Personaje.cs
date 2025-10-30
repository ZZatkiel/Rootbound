using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Personaje : MonoBehaviour
{
    // --- ATRIBUTOS DEL PERSONAJE ---
    [Header("Atributos Base")]
    public float vida = 100f;
    public float daño = 15f;
    public float velocidadDeAtaque = 1.2f; // ataques por segundo
    [Range(0f, 100f)] public float critico = 10f; // probabilidad %
    public float defensa = 5f;

    // --- MOVIMIENTO ---
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float gravedad = -9.8f;
    public float fuerzaSalto = 3f;

    private CharacterController controller;
    private Vector3 velocidadVertical;
    private bool estaEnSuelo;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Mover();
    }

    void Mover()
    {
        // --- MOVIMIENTO HORIZONTAL ---
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movimiento = transform.right * x + transform.forward * z;
        controller.Move(movimiento * velocidad * Time.deltaTime);

        // --- GRAVEDAD Y SALTO ---
        estaEnSuelo = controller.isGrounded;
        if (estaEnSuelo && velocidadVertical.y < 0)
            velocidadVertical.y = -2f; // mantiene al personaje pegado al suelo

        if (Input.GetButtonDown("Jump") && estaEnSuelo)
            velocidadVertical.y = Mathf.Sqrt(fuerzaSalto * -2f * gravedad);

        velocidadVertical.y += gravedad * Time.deltaTime;
        controller.Move(velocidadVertical * Time.deltaTime);
    }
}