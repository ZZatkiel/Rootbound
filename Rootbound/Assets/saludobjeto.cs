using UnityEngine;

public class SaludObjeto : MonoBehaviour
{
    // Vida total del objeto (ajustar en el Inspector)
    public float vidaActual = 100f;

    // Este método es llamado por el script AtaquePersonaje
    public void RecibirDano(float cantidadDano)
    {
        vidaActual -= cantidadDano; // Aplica el daño

        // Muestra la vida restante en la consola para depuración
        Debug.Log(gameObject.name + " recibió daño. Vida restante: " + vidaActual);

        // Comprueba si el objeto debe morir
        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log(gameObject.name + " ha sido destruido.");
        // Quita el objeto de la escena
        Destroy(gameObject);
    }
}