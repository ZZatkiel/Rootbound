using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManagerPausa : MonoBehaviour
{
    public GameObject panelPausa;
    private bool juegoPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            juegoPausado = !juegoPausado;

            if (juegoPausado) PausarJuego();
            else ReanudarJuego();
        }
    }

    public void PausarJuego()
    {
        panelPausa.SetActive(true);

        // pausar el juego
        Time.timeScale = 0f;

        // desbloquear y mostrar cursor para poder clicar en la UI
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


    }

    public void ReanudarJuego()
    {
        panelPausa.SetActive(false);

        // reanudar el tiempo
        Time.timeScale = 1f;

        // opcional: volver a bloquear cursor (si tu juego lo usa)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }

    public void VolverAlMenu()
    {
        Debug.Log("Volviendo al menú...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("InterfazMenu"); // carga la escena del menú principal
    }

}



// Este script gestiona la funcionalidad de pausa del juego. Al presionar la tecla ESC, el juego se pausa o se reanuda, mostrando u ocultando un panel de pausa. También incluye una función para volver al menú principal.
//Esta Vnculada a un cubo en la escena del juego, que luego referencia al panel de pausa en el inspector de Unity.