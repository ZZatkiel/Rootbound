using UnityEngine;

public class ArbolVida : MonoBehaviour
{
    [Header("Configuracion de Vida del arbol")]
    [SerializeField] private int vidaMaxima = 100;

    [Header("Referencia a la UI de Game Over")]
    [SerializeField] private GameObject panelGameOver; // UI que se activar� al morir el �rbol

    public int VidaActual { get; private set; }

    private bool arbolDestruido = false;

    private void Awake()
    {
        VidaActual = vidaMaxima;

        // Asegura que el panel esto oculto al iniciar
        if (panelGameOver != null)
            panelGameOver.SetActive(false);
    }

    public void RecibirDaño(int cantidad)
    {
        if (arbolDestruido) return;

        VidaActual = Mathf.Max(VidaActual - cantidad, 0);
        Debug.Log($" Arbol recibio {cantidad} de daño. Vida actual: {VidaActual}");

        if (VidaActual <= 0)
            Morir();
    }

   

    public float ObtenerVidaPorcentaje() => (float)VidaActual / vidaMaxima;

    private void Morir()
    {
        if (arbolDestruido) return;
        arbolDestruido = true;

        Debug.Log(" El Arbol ha muerto. Game Over!");

        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
            Time.timeScale = 0f; // Pausa el juego
        }
        
    }
}
