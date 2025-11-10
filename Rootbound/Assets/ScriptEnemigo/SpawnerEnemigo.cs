using UnityEngine;
using UnityEngine.UIElements;

public class SpawnerEnemigo : MonoBehaviour
{

    [SerializeField] private InformacionEnemigo[] enemigosDisponibles;
    [SerializeField] private Transform[] puntosSpawn;

    public void SpawnerEnemigoRandom()
    {
        // obtengo 3 posibles enemigos los cuales usar
        InformacionEnemigo dataEnemigo = enemigosDisponibles[Random.Range(0, enemigosDisponibles.Length)];

        // Obtengo alguna de las 4 posiciones posibles para instanciar los enemigos
        Transform dataPosicion = puntosSpawn[Random.Range(0, puntosSpawn.Length)];

        //Instanciar en la escena al enemigo
        GameObject ObjetoEnemigo = Instantiate(dataEnemigo.Prefab, dataPosicion.position, Quaternion.identity);

        // Obtengo la dificultad que va a tener el enemigo
        float dificultad = GameManagerSC.Instancia.roundManager.multiplicadorDeDifcultad();

        // Ahora llamo al componente logico del enemigo para instanciarlo y pasarle sus respectivos datos 
        ObjetoEnemigo.GetComponent<LogicaEnemigo>().Inicializador(dataEnemigo, dificultad);



    }

}
