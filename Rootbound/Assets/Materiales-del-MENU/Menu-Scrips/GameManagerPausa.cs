using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerPausa : MonoBehaviour
{
    public GameObject panelPausa;
    private bool juegoPausado = false; //le damos valor bolleano false a la variable juegoPausado

    private void Awake()

    {
        panelPausa.SetActive(false); // Asegura de que el panel de pausa esté desactivado al inicio
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Detecta si se presiona la tecla ESC
        {
            Debug.Log("Presioné ESC"); //  debería verse en la consola
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Pausar()
    {
        Debug.Log("Juego pausado"); //muestra en consola que el juego está pausado
        panelPausa.SetActive(true); // Activa el panel de pausa
        Time.timeScale = 0f;
        juegoPausado = true;
    }

    public void Reanudar()
    {
        Debug.Log("Juego reanudado"); // muestra en consola que el juego está reanudado
        panelPausa.SetActive(false);// Desactiva el panel de pausa
        Time.timeScale = 1f;
        juegoPausado = false;
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