using UnityEngine;


/*
 * Script del movimiento del personaje
 * En este script se mueve el persoanaje mediante inputs y la posicion relativa de la camara
 * ACLARACION, NO SE REALIZA LA ROTACION DE LA CAMARA ACA
*/


public class ThirdPersonController : MonoBehaviour
{
    private float velocidadNormal = 4f;
    private float velocidadDeseadaCorrer = 6f;
    private float velocidadCorrer = 0f;

    public float fuerzaDeSalto = 10f;
    public float gravedad = 20f;

    private float velocidadVertical = 0f;

    float inputHorizontal;
    float inputVertical;
    bool inputJump;
    bool inputRun;

    Animator animator;
    CharacterController cc;
    LogicaGuerrero logica;

    float coyoteTime = 3f;
    float coyoteCounter;




    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (animator == null)
            Debug.LogWarning("No tenes el componente Animator en tu jugador.");

        logica = GetComponent<LogicaGuerrero>();
        if (logica != null)
            aplicarEstadisticasDeMovimiento(logica.Datos);
    }

    void Update()
    {
        // ---------------- INPUT ----------------
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        inputJump = Input.GetKeyDown(KeyCode.Space);
        inputRun = Input.GetKey(KeyCode.LeftShift);

        Debug.Log($"Fuerza de salto {fuerzaDeSalto}");

        velocidadCorrer = inputRun ? velocidadDeseadaCorrer : 0f;
        float velocidadTotal = velocidadNormal + velocidadCorrer;

        // ---------------- DIRECCIÓN CÁMARA ----------------
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 horizontalDirection = camForward * inputVertical + camRight * inputHorizontal;

        if (horizontalDirection.magnitude > 1)
            horizontalDirection.Normalize();

        // ---------------- ROTACIÓN ----------------
        if (horizontalDirection.magnitude > 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(horizontalDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 10f * Time.deltaTime);
        }

        horizontalDirection *= velocidadTotal;

        // ---------------- SALTO Y GRAVEDAD ----------------
        if (cc.isGrounded)
        {
            coyoteCounter = coyoteTime;

            if (velocidadVertical < 0)
                velocidadVertical = -2f;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        // Salto
        if (inputJump && coyoteCounter > 0f)
        {
            velocidadVertical = fuerzaDeSalto;
            animator?.SetTrigger("jump");
            coyoteCounter = 0f;
        }

        velocidadVertical -= gravedad * Time.deltaTime;

        Vector3 verticalDirection = Vector3.up * velocidadVertical;

        // ---------------- MOVIMIENTO FINAL ----------------
        Vector3 movement = horizontalDirection + verticalDirection;
        cc.Move(movement * Time.deltaTime);

        // ---------------- ANIMACIONES ----------------
        if (animator != null)
        {
            bool isIdle = horizontalDirection.magnitude < 0.1f && cc.isGrounded;
            bool isWalking = horizontalDirection.magnitude > 0.1f && !inputRun && cc.isGrounded;
            bool isRunning = horizontalDirection.magnitude > 0.1f && inputRun && cc.isGrounded;

            animator.SetBool("idle", isIdle);
            animator.SetBool("walk", isWalking);
            animator.SetBool("run", isRunning);
            animator.SetBool("air", !cc.isGrounded);
        }
    }

    public void aplicarEstadisticasDeMovimiento(InformacionPersonaje datos)
    {
        velocidadNormal = datos.VelocidadMovimiento;
        velocidadDeseadaCorrer = velocidadNormal * datos.MultiplicadorCorrer;
    }
}