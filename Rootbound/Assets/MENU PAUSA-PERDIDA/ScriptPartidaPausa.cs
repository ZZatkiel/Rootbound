using UnityEngine;
using UnityEngine.SceneManagement;

// Este script gestiona la funcionalidad de pausa del juego. Al presionar la tecla ESC, el juego se pausa o se reanuda, mostrando u ocultando un panel de pausa. También incluye una función para volver al menú principal.

public class ScriptPartidaPausa : MonoBehaviour
{
    public GameObject panelPausa;
    public GameObject panelPerdido;
    public GameObject inventario;
    private bool juegoPausado = false;

    public MonoBehaviour[] scriptsADesactivar;

    void Update()
    {

        if (inventario != null && inventario.activeSelf)
            return;

        if (panelPerdido != null && panelPerdido.activeSelf)
        {
            return;
        }

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

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        foreach (var script in scriptsADesactivar)
        {
            script.enabled = false;
        }

    }

    public void ReanudarJuego()
    {
        panelPausa.SetActive(false);

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        foreach (var script in scriptsADesactivar)
        {
            script.enabled = true;
        }

    }

    public void VolverAlMenu()
    {
        Debug.Log("Volviendo al menú...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("InterfazMenu");
    }

    public void SiClickeoElReanudar()
    {
        juegoPausado = !juegoPausado;
    }


}



