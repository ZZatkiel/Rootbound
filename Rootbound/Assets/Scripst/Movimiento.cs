using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento que puedes ajustar en Unity
    private CharacterController controller; // Necesario para mover al personaje

    void Start()
    {
        // Obtiene el componente CharacterController que vamos a añadir
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("PlayerMovement requiere un CharacterController.");
            enabled = false; // Desactiva el script si no lo encuentra
        }
    }

    void Update()
    {
        // 1. Obtener la entrada del usuario (teclas WASD o flechas)
        float horizontalInput = Input.GetAxis("Horizontal"); // Para izquierda/derecha
        float verticalInput = Input.GetAxis("Vertical");     // Para adelante/atrás

        // 2. Crear un vector de movimiento
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // 3. Mover el personaje
        if (moveDirection.magnitude >= 0.1f)
        {
            // Opcional: Rotar el personaje en la dirección del movimiento
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

            // Mueve el personaje usando el CharacterController
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}