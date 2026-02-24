using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    public GameObject personaje;
    private LogicaGuerrero personajeDatos;

    public GameObject arbol;
    private ArbolScript arbolDatos;

    public GameObject spawnDeEnemigos;
    private SpawnerEnemigoManager spawnerDatos;


    int vidaActualPersonaje;
    int puntosActuales;

    int rondaActual;
    int miniRondasActual;

    int vidaActualArbol;


    //TEXTOS

    public Text vidaActualPersonajeTexto;
    public Text puntosActualesTexto;
    public Text rondaActualTexto;
    public Text miniRondasActualTexto;
    public Text vidaActualArbolTexto;

    void Start()
    {
        personajeDatos = personaje.GetComponent<LogicaGuerrero>();
        spawnerDatos = spawnDeEnemigos.GetComponent<SpawnerEnemigoManager>();
        arbolDatos = arbol.GetComponent<ArbolScript>();

    }

    void Update()
    {

        puntosActuales = GameManagerSC.Instancia.scoreManager.obtenerPuntos();
        rondaActual = (int)GameManagerSC.Instancia.roundManager.obtenerRonda();
        vidaActualPersonaje = (int)personajeDatos.ObtenerVidaActual();
        miniRondasActual = spawnerDatos.obtenerMiniRondaActual();

        vidaActualArbol = (int)arbolDatos.ObtenerVidaActual();

        vidaActualPersonajeTexto.text = vidaActualPersonaje.ToString();
        puntosActualesTexto.text = puntosActuales.ToString();
        rondaActualTexto.text = rondaActual.ToString();
        miniRondasActualTexto.text = miniRondasActual.ToString();
        vidaActualArbolTexto.text = vidaActualArbol.ToString();



    }
}
