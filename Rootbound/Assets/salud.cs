using UnityEngine;

public class Salud : MonoBehaviour
{
    [Header("Configuraci�n de Salud")]
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
        Debug.Log(gameObject.name + " recibi� " + cantidadDano + " de da�o. Salud restante: " + saludActual);

        if (saludActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        estaVivo = false;

        Debug.Log(gameObject.name + " ha sido destruido.");

        // L�gica de Destrucci�n Especial para Player y �rbol
        // Si el objeto tiene un padre llamado "Jugadores", destruye el objeto padre.
        // Esto es �til si tienes objetos hijos (c�maras, etc.) que se destruyen con el padre.
        if (transform.parent != null && transform.parent.name == "Jugadores")
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            // Destrucci�n normal (para el enemigo CortaDistancia o cualquier otro objeto)
            Destroy(gameObject);
        }
    }

    // M�todo opcional para curar
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