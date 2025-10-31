using UnityEngine;
using UnityEngine.SceneManagement;
public class PartidaPerdidaScript : MonoBehaviour
{





    public void ReiniciarPartida()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("JuegoEscenaPrincipal"); 

    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("InterfazMenu");

    }

}
