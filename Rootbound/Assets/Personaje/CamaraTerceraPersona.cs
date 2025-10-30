using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ThirdPersonCameraChild : MonoBehaviour
{
    [Header("Target (se busca automáticamente en el parent)")]
    public Transform target; // si lo dejas vacío el script usará transform.parent

    [Header("Pivot (offset respecto al target)")]
    public Vector3 pivotOffset = new Vector3(0f, 1.6f, 0f); // punto aproximado de la cabeza

    [Header("Distancia y Zoom")]
    public float distance = 3f;
    public float minDistance = 1f;
    public float maxDistance = 6f;
    public float scrollSensitivity = 2f;

    [Header("Rotación")]
    public float rotationSpeed = 5f;
    public bool invertY = false;
    [Range(-89f, 89f)] public float minYAngle = -30f;
    [Range(-89f, 89f)] public float maxYAngle = 60f;

    [Header("Suavizado")]
    public float positionSmoothTime = 0.06f;
    public float rotationSmoothTime = 0.08f;

    [Header("Colisiones")]
    public float cameraRadius = 0.25f;
    public LayerMask obstructionMask = ~0; // por defecto todas las capas
    public float collisionOffset = 0.1f; // distancia para separar de la pared

    // estado interno
    float yaw;
    float pitch = 10f;
    float currentDistance;
    Vector3 currentVelocity = Vector3.zero;

    void Start()
    {
        // si no asignaron target, usamos el parent (según la jerarquía solicitada)
        if (target == null && transform.parent != null)
            target = transform.parent;

        currentDistance = Mathf.Clamp(distance, minDistance, maxDistance);

        // opcional: bloquear cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null) return;

        HandleInput();
        HandleZoom();

        Vector3 pivotWorld = target.position + pivotOffset;

        // calculamos la rotación deseada desde yaw/pitch
        Quaternion targetRot = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredDir = targetRot * Vector3.forward; // forward según rotación
        Vector3 desiredPos = pivotWorld - desiredDir * currentDistance;

        // Colisión: spherecast desde pivotWorld hacia desiredPos
        RaycastHit hit;
        Vector3 dir = (desiredPos - pivotWorld);
        float maxDist = dir.magnitude;
        if (maxDist > 0.001f)
        {
            dir /= maxDist; // normalizar

            if (Physics.SphereCast(pivotWorld, cameraRadius, dir, out hit, maxDist, obstructionMask, QueryTriggerInteraction.Ignore))
            {
                float hitDist = Mathf.Max(hit.distance - collisionOffset, minDistance);
                desiredPos = pivotWorld + dir * hitDist;
                currentDistance = Mathf.Clamp(hitDist, minDistance, maxDistance);
            }
            else
            {
                // si no choca, recuperamos la distancia objetivo
                currentDistance = Mathf.Clamp(distance, minDistance, maxDistance);
            }
        }

        // suavizado de posición
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref currentVelocity, positionSmoothTime);

        // rotación: mirar al pivot
        Quaternion wantLook = Quaternion.LookRotation(pivotWorld - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, wantLook, 1f - Mathf.Exp(-rotationSmoothTime * 60f * Time.deltaTime));
    }

    void HandleInput()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        yaw += mouseX * rotationSpeed;
        pitch += (invertY ? mouseY : -mouseY) * rotationSpeed;
        pitch = Mathf.Clamp(pitch, minYAngle, maxYAngle);
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.001f)
        {
            currentDistance = Mathf.Clamp(currentDistance - scroll * scrollSensitivity, minDistance, maxDistance);
        }
    }

    // Método público para recentar la cámara detrás del jugador
    public void RecenterBehindTarget()
    {
        yaw = target.eulerAngles.y;
    }
}
