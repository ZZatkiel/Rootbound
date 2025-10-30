// Archivo: SpawnEnemigos.cs (Actualizado para Muerte/Reciclaje)
using UnityEngine;
using System.Collections.Generic;

public class SpawnEnemigos : MonoBehaviour
{
    // Asigna el Prefab del enemigo en el Inspector
    public GameObject prefabEnemigo;

    // Distancia a la que aparecerán (respecto a este objeto SpawnManager)
    public float distanciaSpawn = 5f;

    // Intervalo de tiempo entre cada aparición
    public float tiempoEntreSpawns = 3f;

    private float proximoTiempoSpawn;

    // Referencia al prefab enemigo original (para la lógica de Muerte)
    private GameObject enemigoOriginalPrefab;

    void Start()
    {
        proximoTiempoSpawn = Time.time + tiempoEntreSpawns;

        if (prefabEnemigo == null)
        {
            Debug.LogError("¡ERROR! Asigna el Prefab del enemigo en el Inspector de SpawnEnemigos.");
            enabled = false;
            return;
        }

        // Asignar el prefab original para la lógica de reciclaje
        enemigoOriginalPrefab = prefabEnemigo;
    }

    void Update()
    {
        if (Time.time >= proximoTiempoSpawn)
        {
            SpawnearEnemigo();
            proximoTiempoSpawn = Time.time + tiempoEntreSpawns;
        }
    }

    void SpawnearEnemigo()
    {
        Vector3 posicionAleatoria2D = Random.insideUnitCircle.normalized * distanciaSpawn;
        Vector3 posicionSpawn = transform.position + new Vector3(posicionAleatoria2D.x, transform.position.y, posicionAleatoria2D.y);

        GameObject nuevoEnemigo = Instantiate(prefabEnemigo, posicionSpawn, Quaternion.identity);

        // Marcar la instancia como clonada para que MovimientoEnemigo sepa qué hacer al morir
        MovimientoEnemigo mov = nuevoEnemigo.GetComponent<MovimientoEnemigo>();
        if (mov != null)
        {
            mov.esInstanciaClonada = true;
        }
    }

    /// <summary>
    /// Método llamado por SaludObjeto.cs cuando un enemigo muere.
    /// </summary>
    /// <param name="enemigo">El objeto GameObject del enemigo destruido.</param>
    public void EnemigoMuerto(GameObject enemigo)
    {
        // Si el objeto que muere es la plantilla original (el prefab en la escena)
        if (enemigo == enemigoOriginalPrefab)
        {
            Debug.Log("SpawnManager: La plantilla original ha muerto. Solo se inhabilita.");
            // En la nueva lógica, el prefab original debería estar deshabilitado en la escena.
            enemigo.SetActive(false);
        }
        else
        {
            // Si es un enemigo instanciado, se destruye normalmente (o se recicla)
            Debug.Log("SpawnManager: Enemigo instanciado destruido.");
            Destroy(enemigo);
        }

        // Nota: Si implementas un Pool de objetos, aquí harías: 
        // enemigo.GetComponent<MovimientoEnemigo>().ReciclarEnemigo();
    }
}