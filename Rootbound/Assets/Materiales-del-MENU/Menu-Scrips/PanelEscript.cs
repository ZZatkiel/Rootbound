using UnityEngine;

public class BotonCancelarNuevaPartida : MonoBehaviour
{
    public GameObject menuPrincipal;
    public GameObject panelNuevaPartida;

    public void Cancelar()
    {
        panelNuevaPartida.SetActive(false); // Oculta el panel de nueva partida
        menuPrincipal.SetActive(true); // Muestra el menú principal
    }
}
