using UnityEngine;

/// <summary>
/// Script principal para el movimiento en tercera persona del personaje en el juego.
/// Asegurate de que el objeto que reciba este script (el jugador)
/// tenga el tag Player y el componente Character Controller.
/// </summary>
public class ThirdPersonController : MonoBehaviour
{
    [Tooltip("Velocidad a la que se mueve el personaje. No se ve afectada por la gravedad ni el salto.")]
    public float velocidadNormal = 5f;

    [Tooltip("Velocidad a la que se mueve el personaje. No se ve afectada por la gravedad ni el salto.")]
    public float velocidadDeseadaCorrer = 5f;
    float velocidadCorrer = 1;

    [Tooltip("Cuanto mayor sea el valor, más alto saltará el personaje.")]
    public float jumpForce = 18f;

    [Tooltip("Tiempo en el aire. Cuanto mayor sea el valor, más tiempo flotará el personaje antes de caer.")]
    public float jumpTime = 0.85f;

    [Space]
    [Tooltip("Fuerza que empuja al jugador hacia abajo. Cambiar este valor afecta todo el movimiento, salto y caída.")]
    public float gravity = 9.8f;

    float jumpElapsedTime = 0;

    // Estado de salto
    bool isJumping = false;

    // Inputs
    float inputHorizontal;
    float inputVertical;
    bool inputJump;
    bool inputRun;

    Animator animator;
    CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (animator == null)
            Debug.LogWarning("Che bro, no tenés el componente Animator en tu jugador. Sin eso, las animaciones no funcionan.");
    }

    // Update solo se usa acá para detectar teclas y activar animaciones
    void Update()
    {
        // Detectores de Input
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        inputJump = Input.GetButtonDown("Jump"); // detecta el momento de presionar

        inputRun = Input.GetKey(KeyCode.LeftShift);

        if (inputRun)
        {
            velocidadCorrer = velocidadDeseadaCorrer;
        }
        else
        {
            velocidadCorrer = 1f;
        }

        // Animaciones de caminar y correr (solo si estás en el suelo)
        if (cc.isGrounded && animator != null)
        {
            // Detectar si hay movimiento (aunque sea leve)
            bool isMoving = Mathf.Abs(inputHorizontal) > 0.1f || Mathf.Abs(inputVertical) > 0.1f;

            // Caminar = moviendo + NO Shift
            bool isWalking = isMoving && !inputRun;

            // Correr = moviendo + Shift
            bool isRunning = isMoving && inputRun;

            animator.SetBool("walk", isWalking);
            animator.SetBool("run", isRunning);
        }


        // Animación de salto (aire)
        if (animator != null)
            animator.SetBool("air", cc.isGrounded == false);

        // Manejo del salto: iniciamos salto solo si se presionó y estamos en el piso
        if (inputJump && cc.isGrounded)
        {
            isJumping = true;
            jumpElapsedTime = 0f; // reinicio por si acaso
        }
    }

    // FixedUpdate aplica el movimiento real
    private void FixedUpdate()
    {
        // Movimiento horizontal y vertical (sin sprint)
        float directionX = inputHorizontal * (velocidadNormal + velocidadCorrer) * Time.deltaTime;
        float directionZ = inputVertical * (velocidadNormal + velocidadCorrer) * Time.deltaTime;
        float directionY = 0;

        // Manejo del salto
        if (isJumping)
        {
            directionY = Mathf.SmoothStep(jumpForce, jumpForce * 0.30f, jumpElapsedTime / jumpTime) * Time.deltaTime;

            // Timer del salto
            jumpElapsedTime += Time.deltaTime;
            if (jumpElapsedTime >= jumpTime)
            {
                isJumping = false;
                jumpElapsedTime = 0;
            }
        }

        // Aplicar gravedad
        directionY = directionY - gravity * Time.deltaTime;

        // --- Rotación del personaje según cámara y entrada ---
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        // Usamos la entrada para calcular la dirección horizontal (esto evita depender de valores muy pequeños por deltaTime)
        Vector3 moveDir = camForward * inputVertical + camRight * inputHorizontal;

        if (moveDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 0.15f);
        }

        // --- Fin rotación ---

        Vector3 verticalDirection = Vector3.up * directionY;
        Vector3 horizontalDirection = (camForward * directionZ) + (camRight * directionX);

        Vector3 movement = verticalDirection + horizontalDirection;
        cc.Move(movement);
    }
}
