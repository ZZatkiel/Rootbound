using UnityEngine;

public class ThirdPersonCameraSimple : MonoBehaviour
{
    [Header("Configuración del Target")]
    public Transform target; // el jugador o el objeto a seguir
    public Vector3 offset = new Vector3(0f, 1.6f, -3f); // posición de la cámara respecto al jugador

    [Header("Rotación")]
    public float sensibilidadX = 100f;
    public float sensibilidadY = 80f;
    public bool invertirY = false;
    public float minY = -30f;
    public float maxY = 60f;

    [Header("Suavizado")]
    public float suavizado = 10f;

    private float rotacionX;
    private float rotacionY;

    void Start()
    {
        if (target == null && transform.parent != null)
            target = transform.parent;

        // Bloquea y oculta el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Inicializa los ángulos
        Vector3 rot = transform.eulerAngles;
        rotacionX = rot.y;
        rotacionY = rot.x;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // --- Entrada del mouse ---
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadY * Time.deltaTime;

        rotacionX += mouseX;
        rotacionY += (invertirY ? mouseY : -mouseY);
        rotacionY = Mathf.Clamp(rotacionY, minY, maxY);

        // --- Rotación y posición ---
        Quaternion rotacion = Quaternion.Euler(rotacionY, rotacionX, 0);
        Vector3 posicionDeseada = target.position + rotacion * offset;

        // --- Suavizado de movimiento ---
        transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavizado * Time.deltaTime);
        transform.LookAt(target.position + Vector3.up * 1.5f); // mira ligeramente por encima del centro
    }
}
