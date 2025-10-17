using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // La referencia al objeto que queremos seguir (el Player)
    public Vector3 offset = new Vector3(0f, 5f, -7f); // La posición relativa de la cámara (arriba y atrás)
    public float smoothSpeed = 15f; // Qué tan suave será el seguimiento

    void LateUpdate() // Usamos LateUpdate para asegurarnos de que el personaje se ha movido primero
    {
        if (target == null) return;

        // Calcula la posición deseada de la cámara
        Vector3 desiredPosition = target.position + offset;

        // Usa Lerp para mover la cámara suavemente a esa posición
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        
        transform.position = smoothedPosition;

        // mirar alñ objetivo
        transform.LookAt(target);
    }
}