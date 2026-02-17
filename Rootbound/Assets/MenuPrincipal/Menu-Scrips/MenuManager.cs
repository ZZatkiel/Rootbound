using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  

//Este script maneja todos los menus de la escena InterfazMenu
public class MenuManager : MonoBehaviour
{
    [Header("Referencias de Paneles")]
    public GameObject menuPrincipal;      
    public GameObject panelNuevaPartida;  

    [Header("Campo de Nombre")]
    public TMP_InputField campoNombre;   

    public void MostrarPanelNuevaPartida()
    {
        menuPrincipal.SetActive(false);
        panelNuevaPartida.SetActive(true);
    }

    public void CancelarNuevaPartida()
    {
        panelNuevaPartida.SetActive(false);
        menuPrincipal.SetActive(true);
    }

    public void AceptarNuevaPartida()
    {
        string nombreJugador = campoNombre.text;

        if (!string.IsNullOrEmpty(nombreJugador))
        {
            GameData.Instance.SetPlayerName(nombreJugador); 
            SceneManager.LoadScene("JuegoEscenaPrincipal");            
        }
        else
        {
            Debug.Log("El nombre no puede estar vacío");
        }
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo de RootBound....");
        Application.Quit(); // Esta linea hace que la aplicacion se cierre.
    }


}
