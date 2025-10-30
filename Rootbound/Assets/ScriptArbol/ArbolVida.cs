using UnityEngine;

public class ArbolVida : MonoBehaviour
{
    [Header("Configuración de Vida del Árbol")]
    [SerializeField] private int vidaMaxima = 100;

    [Header("Referencia a la UI de Game Over")]
    [SerializeField] private GameObject panelGameOver; // UI que se activará al morir el árbol

    public int VidaActual { get; private set; }

    private bool arbolDestruido = false;

    private void Awake()
    {
        VidaActual = vidaMaxima;

        // Asegura que el panel esté oculto al iniciar
        if (panelGameOver != null)
            panelGameOver.SetActive(false);
    }

    public void RecibirDaño(int cantidad)
    {
        if (arbolDestruido) return;

        VidaActual = Mathf.Max(VidaActual - cantidad, 0);
        Debug.Log($" Árbol recibió {cantidad} de daño. Vida actual: {VidaActual}");

        if (VidaActual <= 0)
            Morir();
    }

   

    public float ObtenerVidaPorcentaje() => (float)VidaActual / vidaMaxima;

    private void Morir()
    {
        if (arbolDestruido) return;
        arbolDestruido = true;

        Debug.Log(" El árbol ha muerto. ¡Game Over!");

        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
            Time.timeScale = 0f; // Pausa el juego
        }
        
    }
}
