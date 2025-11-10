using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerSC : MonoBehaviour
{
    private static GameManagerSC instancia;
    public ScoreManager scoreManager;
    public AudioManager audioManager;
    public RoundManager roundManager;

    static public GameManagerSC Instancia
    {
        get { return instancia; }

        private set { instancia = value; }
    }

    private void Awake()
    {
        // Si ya existe una instancia y no es esta, que destruya la nueva
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        Instancia = this;    // Asignamos la instancia
        DontDestroyOnLoad(gameObject); //Hace que no se destruya cuando cambie la escena
        scoreManager = new ScoreManager(0);
        audioManager = new AudioManager(gameObject);
        roundManager = new RoundManager(1); 
    }

}
