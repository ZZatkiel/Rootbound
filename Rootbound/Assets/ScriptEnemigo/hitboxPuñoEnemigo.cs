using UnityEngine;

/*
 * ESTE SCRIPT CONTROLA LA HITBOX DEL PUÑO DEL ENEMIGO.
 * SE ENCARGA DE DETECTAR CUANDO EL ENEMIGO GOLPEA AL JUGADOR O AL ÁRBOL.
 * 
*/

public class hitboxPuñoEnemigo : MonoBehaviour
{

    GameObject playerObj;
    GameObject treeObj;
    public GameObject Enemigo;
    float dañoDelEnemigo;
    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        treeObj = GameObject.FindGameObjectWithTag("Arbol");

        dañoDelEnemigo = Enemigo.GetComponent<LogicaEnemigo>().ObtenerDañoActual();


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HitboxPlayer"))
        {
            Debug.Log("El enemigo le pego al guerrero");
            playerObj.GetComponent<LogicaGuerrero>().recibirDaño(dañoDelEnemigo);

        }
        else if (other.CompareTag("HitboxArbol"))
        {
            Debug.Log("El enemigo le pego al arbol");
            treeObj.GetComponent<ArbolScript>().recibirDaño(dañoDelEnemigo);
        }

        // si es el player o si es el arbol
    }

}
