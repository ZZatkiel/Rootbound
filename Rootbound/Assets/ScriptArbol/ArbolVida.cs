using UnityEngine;

public class ArbolVida : MonoBehaviour
{
    [Header("Configuraci�n de Vida del �rbol")]
    [SerializeField] private int vidaMaxima = 100;

    [Header("Referencia a la UI de Game Over")]
    [SerializeField] private GameObject panelGameOver; // UI que se activar� al morir el �rbol

    public int VidaActual { get; private set; }

    private bool arbolDestruido = false;

    private void Awake()
    {
        VidaActual = vidaMaxima;

        // Asegura que el panel est� oculto al iniciar
        if (panelGameOver != null)
            panelGameOver.SetActive(false);
    }

    public void RecibirDa�o(int cantidad)
    {
        if (arbolDestruido) return;

        VidaActual = Mathf.Max(VidaActual - cantidad, 0);
        Debug.Log($" �rbol recibi� {cantidad} de da�o. Vida actual: {VidaActual}");

        if (VidaActual <= 0)
            Morir();
    }

   

    public float ObtenerVidaPorcentaje() => (float)VidaActual / vidaMaxima;

    private void Morir()
    {
        if (arbolDestruido) return;
        arbolDestruido = true;

        Debug.Log(" El �rbol ha muerto. �Game Over!");

        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
            Time.timeScale = 0f; // Pausa el juego
        }
        
    }
}
