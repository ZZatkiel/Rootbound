using UnityEngine;
using UnityEngine.SceneManagement;

public class Salud : MonoBehaviour
{
    public float vidaMaxima = 100f;
    public float vidaActual;
    public GameObject manejadorDerrota;

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
        // L�gica de efectos de muerte (animaci�n, part�culas, etc.)

        if (transform.parent != null && transform.parent.name == "Jugadores")
        {
            Time.timeScale = 0f;

            // desbloquear y mostrar cursor para poder clicar en la UI
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            manejadorDerrota.SetActive(true);

        }
        else
        {
            Destroy(gameObject);

        }
    }

}