using UnityEngine;


/*
 * ESTE SCRIPT USA EL PATRON DE DISEÑO SINGLETON, EL CUAL ME PERMITE OBTENER 2 COSAS IMPORTANTES
 * - QUE SEA COMO UNA VARIABLE GLOBAL, ACCESIBLE DESDE CUALQUEIR LUGAR
 * - QUE TENGA UNA UNICA INSTANCIA EN TODO EL JUEGO Y EVITAR DUPLICACIONES
 *
 * MI IDEA CON ESTE SCRIPT ES USARLO COMO SI FUERA UN ROUTER
 * - Centralizar el acceso a los distintos sistemas principales.
 * - Recibir peticiones desde cualquier parte del juego.
 * - Redirigir esas peticiones al sistema correspondiente.
*/

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
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        Instancia = this;    
        DontDestroyOnLoad(gameObject); 
        scoreManager = new ScoreManager(0);
        audioManager = new AudioManager(gameObject);
        roundManager = new RoundManager(0);

    }

}
