using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverAlMenu : MonoBehaviour
{
    public void IrAlMenu()
    {
        // Reanuda el tiempo por si el juego estaba pausado
        Time.timeScale = 1f;

        // Carga la escena del menú (asegúrate de que el nombre coincida con el de tu escena)
        SceneManager.LoadScene("InterfazMenu");
    }
}
