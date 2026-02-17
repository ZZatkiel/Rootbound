using UnityEngine;
using UnityEngine.SceneManagement;


// Este srcipt aparece cuando te matan en la partida, este menu puede o volver a cargar la partida/escena o ir al menu principal donde podes empezar una nueva partida
public class PartidaPerdidaScript : MonoBehaviour
{
    public MonoBehaviour[] ScriptsDesactivar;


    public void MuerteDesactivadorScripts()
    {

        Debug.Log("Se llamo a muerteDesavidaroScript");
        foreach (var script in ScriptsDesactivar)
        {
            script.enabled = false;
        }
    }



    public void ReiniciarPartida()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("JuegoEscenaPrincipal");
        GameManagerSC.Instancia.roundManager.reiniciarRonda();
        GameManagerSC.Instancia.scoreManager.reiniciarPuntos();
        foreach (var script in ScriptsDesactivar)
        {
            script.enabled = true;
        }



    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("InterfazMenu");
        GameManagerSC.Instancia.roundManager.reiniciarRonda();
        GameManagerSC.Instancia.scoreManager.reiniciarPuntos();
        foreach (var script in ScriptsDesactivar)
        {
            script.enabled = true;
        }
    }

}
