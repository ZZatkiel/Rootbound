using UnityEngine;
using UnityEngine.InputSystem;

// Adjuntar este script al objeto Main Camera
public class CamaraFantasma : MonoBehaviour
{
    [Header("Movimiento")]
    // Aumentamos un poco la velocidad base para sentir el efecto 'fantasma'
    public float velocidadBase = 20f;
    public float multiplicadorVelocidad = 3f; // Para un movimiento más rápido al presionar Shift
    public float velocidadRotacion = 3f;

    // Variables para el look
    private float lookX;
    private float lookY;

    // Se llama solo una vez si el Input System está configurado para eventos
    public void OnLook(InputValue value)
    {
        // Esto asume que el input look del jugador se sigue transmitiendo al Main Camera Object
        Vector2 lookValue = value.Get<Vector2>();
        lookX = lookValue.x;
        lookY = lookValue.y;
    }

    void Update()
    {
        // Bloquea y oculta el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // =================================================================
        // ROTACIÓN (LOOK) CON EL RATÓN
        // =================================================================

        // Rotación horizontal
        transform.Rotate(Vector3.up * lookX * velocidadRotacion * Time.deltaTime, Space.World);

        // Rotación vertical (Límite para evitar volteo)
        // Obtenemos el ángulo actual en X y lo limitamos
        float anguloX = transform.localEulerAngles.x;
        if (anguloX > 180) anguloX -= 360;

        float nuevaRotacionX = anguloX - (lookY * velocidadRotacion * Time.deltaTime);
        nuevaRotacionX = Mathf.Clamp(nuevaRotacionX, -80f, 80f);

        // Aplicar la rotación solo en el eje local X
        transform.localRotation = Quaternion.Euler(nuevaRotacionX, transform.localEulerAngles.y, 0f);


        // =================================================================
        // MOVIMIENTO LIBRE (WASD / Flotación ILIMITADA)
        // =================================================================

        float moveX = 0f;
        float moveY = 0f;
        float moveZ = 0f;

        // Lectura de inputs
        if (Keyboard.current.wKey.isPressed) moveZ = 1f;
        if (Keyboard.current.sKey.isPressed) moveZ = -1f;
        if (Keyboard.current.aKey.isPressed) moveX = -1f;
        if (Keyboard.current.dKey.isPressed) moveX = 1f;

        // Movimiento vertical (Arriba/Abajo)
        if (Keyboard.current.spaceKey.isPressed) moveY = 1f;
        if (Keyboard.current.leftCtrlKey.isPressed) moveY = -1f;

        // 🚨 CAMBIO CLAVE: CÁLCULO DE MOVIMIENTO FANTASMA 🚨

        // Calcula la velocidad actual (si se presiona Shift, se multiplica)
        float velocidadActual = velocidadBase;
        if (Keyboard.current.leftShiftKey.isPressed)
        {
            velocidadActual *= multiplicadorVelocidad;
        }

        // Crea el vector de movimiento en el espacio local (sin normalizar para un movimiento más libre)
        Vector3 movimientoLocal = new Vector3(moveX, moveY, moveZ);

        // Aplica la velocidad y el delta time
        Vector3 movimientoFinal = transform.TransformDirection(movimientoLocal) * velocidadActual * Time.deltaTime;

        // Aplica el movimiento
        transform.position += movimientoFinal;
    }
}