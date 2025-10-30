using UnityEngine;

public class Salud : MonoBehaviour
{
    [Header("Configuración de Salud")]
    public float saludMaxima = 100f;
    private float saludActual;
    private bool estaVivo = true;

    void Start()
    {
        saludActual = saludMaxima;
        estaVivo = true;
    }

    public void RecibirDano(float cantidadDano)
    {
        if (!estaVivo) return;

        saludActual -= cantidadDano;
        Debug.Log(gameObject.name + " recibió " + cantidadDano + " de daño. Salud restante: " + saludActual);

        if (saludActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        estaVivo = false;

        Debug.Log(gameObject.name + " ha sido destruido.");

        // Lógica de Destrucción Especial para Player y Árbol
        // Si el objeto tiene un padre llamado "Jugadores", destruye el objeto padre.
        // Esto es útil si tienes objetos hijos (cámaras, etc.) que se destruyen con el padre.
        if (transform.parent != null && transform.parent.name == "Jugadores")
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            // Destrucción normal (para el enemigo CortaDistancia o cualquier otro objeto)
            Destroy(gameObject);
        }
    }

    // Método opcional para curar
    public void Curar(float cantidadCuracion)
    {
        if (!estaVivo) return;
        saludActual += cantidadCuracion;
        if (saludActual > saludMaxima)
        {
            saludActual = saludMaxima;
        }
    }
}