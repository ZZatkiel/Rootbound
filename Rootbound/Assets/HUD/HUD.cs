using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    public GameObject personaje;
    private LogicaGuerrero personajeDatos;

    public GameObject spawnDeEnemigos;
    private SpawnerEnemigoManager spawnerDatos;


    int vidaActual;
    int puntosActuales;

    int rondaActual;
    int miniRondasActual;


    //TEXTOS

    public Text vidaActualTexto;
    public Text puntosActualesTexto;
    public Text rondaActualTexto;
    public Text miniRondasActualTexto;


    void Start()
    {
        personajeDatos = personaje.GetComponent<LogicaGuerrero>();
        spawnerDatos = spawnDeEnemigos.GetComponent<SpawnerEnemigoManager>();

    }

    void Update()
    {

        puntosActuales = GameManagerSC.Instancia.scoreManager.obtenerPuntos();
        rondaActual = (int)GameManagerSC.Instancia.roundManager.obtenerRonda();
        vidaActual = (int)personajeDatos.ObtenerVidaActual();
        miniRondasActual = spawnerDatos.obtenerMiniRondaActual();

        vidaActualTexto.text = vidaActual.ToString();
        puntosActualesTexto.text = puntosActuales.ToString();
        rondaActualTexto.text = rondaActual.ToString();
        miniRondasActualTexto.text = miniRondasActual.ToString();


    }
}
