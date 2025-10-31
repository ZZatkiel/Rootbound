using UnityEngine;

public class Salud : MonoBehaviour
{
    public float vidaMaxima = 100f;
    public float vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDano(float cantidad)
    {
        if (vidaActual <= 0) return;

        vidaActual -= cantidad;

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    // El objeto se DESTRUYE al morir.
    public void Morir()
    {
        // Lógica de efectos de muerte (animación, partículas, etc.)

        // Destrucción final del objeto
        Destroy(gameObject);
        Debug.Log(gameObject.name + " ha sido destruido.");
    }
}