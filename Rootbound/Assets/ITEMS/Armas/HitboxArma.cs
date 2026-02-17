using UnityEngine;


/*
 * SCRIPT EL CUAL SE COMPLEMENTA CON LOGICAGUERRERO
 * CUANDO EL SCRIPT DE LOGICAGUERRERO DE ATACAR SE ACTIVA, SE ACTIVA EL COLLIDER DEL ARMA HACIENDO QUE ESTE Y EL ENEMIGO
 * SE CHOQUEN. PROVOCANDO QUE EL ENEMIGO RECIBA EL DAÑO OBTENIDO DE LA CLASE LOGICAGUERRERO
*/ 

public class HitboxArma : MonoBehaviour
{
    private LogicaGuerrero personaje;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            personaje = playerObj.GetComponent<LogicaGuerrero>();

            if (personaje == null)
            {
                Debug.LogError("El objeto con tag Player NO tiene LogicaGuerrero.");
            }
        }
        else
        {
            Debug.LogError("No se encontró ningún objeto con tag Player.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        LogicaEnemigo enemigo = other.GetComponent<LogicaEnemigo>();
        Debug.Log("PASO POR EL SCRIPT DE HITBOXARMA 1");

        if (enemigo != null && personaje != null)
        {
            enemigo.RecibirDaño(personaje.ObtenerDañoActual());
            Debug.Log("PASO POR EL SCRIPT DE HITBOXARMA 2");
        }
    }
}
