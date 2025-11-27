using UnityEngine;

public class HitboxArma : MonoBehaviour
{
    private LogicaGuerrero personaje;

    private void Start()
    {
        // Buscar al player por tag una vez
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            personaje = playerObj.GetComponent<LogicaGuerrero>();

            if (personaje == null)
                Debug.LogError("El objeto con tag Player NO tiene LogicaGuerrero.");
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
