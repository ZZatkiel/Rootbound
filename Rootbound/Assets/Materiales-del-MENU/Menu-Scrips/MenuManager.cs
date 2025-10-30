using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // Importante para usar TMP_InputField

public class MenuManager : MonoBehaviour
{
    [Header("Referencias de Paneles")]
    public GameObject menuPrincipal;      // Panel del menú principal
    public GameObject panelNuevaPartida;  // Panel donde se ingresa el nombre

    [Header("Campo de Nombre")]
    public TMP_InputField campoNombre;    // Input Field para escribir el nombre del jugador

    // Mostrar el panel para nueva partida
    public void MostrarPanelNuevaPartida()
    {
        menuPrincipal.SetActive(false);
        panelNuevaPartida.SetActive(true);
    }

    // Cancelar nueva partida y volver al menú principal
    public void CancelarNuevaPartida()
    {
        panelNuevaPartida.SetActive(false);
        menuPrincipal.SetActive(true);
    }

    // Aceptar nueva partida: guarda el nombre y cambia de escena
    public void AceptarNuevaPartida()
    {
        string nombreJugador = campoNombre.text;

        if (!string.IsNullOrEmpty(nombreJugador))
        {
            GameData.Instance.SetPlayerName(nombreJugador);  // ✅ Guarda el nombre usando GameData
            SceneManager.LoadScene("JuegoEscenaPrincipal");            // ✅ Cambia a la escena del juego
        }
        else
        {
            Debug.Log("El nombre no puede estar vacío");
        }
    }
}
