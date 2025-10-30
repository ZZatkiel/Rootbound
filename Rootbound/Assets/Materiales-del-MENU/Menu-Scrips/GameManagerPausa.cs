using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerPausa : MonoBehaviour
{
    public GameObject panelPausa;
    private bool juegoPausado = false;
    
    private void Awake()

    {
        panelPausa.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        Debug.Log("Juego pausado");
        panelPausa.SetActive(true);
        Time.timeScale = 0f;
        juegoPausado = true;
    }

    public void Reanudar()
    {
        Debug.Log("Juego reanudado");
        panelPausa.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;
    }

    public void VolverAlMenu()
    {
        Debug.Log("Volviendo al menú...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("InterfazMenu");
    }
}
