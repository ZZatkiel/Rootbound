using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // La referencia al objeto que queremos seguir (el Player)
    public Vector3 offset = new Vector3(0f, 5f, -7f); // La posici�n relativa de la c�mara (arriba y atr�s)
    public float smoothSpeed = 15f; // Qu� tan suave ser� el seguimiento

    void LateUpdate() // Usamos LateUpdate para asegurarnos de que el personaje se ha movido primero
    {
        if (target == null) return;

        // Calcula la posici�n deseada de la c�mara
        Vector3 desiredPosition = target.position + offset;

        // Usa Lerp para mover la c�mara suavemente a esa posici�n
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        
        transform.position = smoothedPosition;

        // mirar al� objetivo
        transform.LookAt(target);
    }
}