using System;
using UnityEngine;

public class LogicaEnemigo : MonoBehaviour
{
    // INFORMACION DEL PERSONAJE
    private int vida;
    private float daño;
    private float velocidadMovimiento;
    private float velocidadAtaque;

    // BUSCAR OBJETIVO 
    private string tagJugador = "Player";
    private string tagArbol = "Arbol";
    private Transform objetivoActual;


    public void Inicializador(InformacionEnemigo data, float multiplicadorRonda)
    {
        vida = data.Vida;
        daño = data.Daño;
        velocidadMovimiento = data.VelocidadMovimiento;
        velocidadAtaque = data.VelocidadAtaque;
    }

    // Que el enemigo busque al personaje o al arbol
    // Quien este mas cerca va a ir a por el
    // El movimiento de este va a ser realizado por el rigidBody
    // No hay ataque todavia


    // tengo un rango el cual me persiguen a mi, si me alejo de ese rango van al arbol 
    public void BuscarObjetivo()
    {
        objetivoActual = null;
        GameObject Jugador = GameObject.FindWithTag(tagJugador);
        GameObject Arbol = GameObject.FindWithTag(tagArbol);

        float DistanciaJugador;
        float DistanciaArbol;

        // Obtengo la distancia entre el ENEMIGO(ESTE) y el ARBOL/JUGADOR -Es una simple resta de vectores-
        if (Jugador != null)
        {
            DistanciaJugador = Vector3.Distance(transform.position, Jugador.transform.position);
        }

        if (Arbol != null)
        {
            DistanciaArbol = Vector3.Distance(transform.position, Jugador.transform.position);
        }

    
    }

    public void SeguimientoEnemigo()
    {

    }


    public void AtaqueEnemigo()
    {

    }

}
