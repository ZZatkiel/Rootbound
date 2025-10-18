using UnityEngine;

public class SpawnEnemigos : MonoBehaviour
{
    // Asigna el Prefab del enemigo en el Inspector
    public GameObject prefabEnemigo;

    // Distancia a la que aparecer�n (respecto a este objeto SpawnManager)
    public float distanciaSpawn = 5f;

    // Intervalo de tiempo entre cada aparici�n
    public float tiempoEntreSpawns = 3f;

    private float proximoTiempoSpawn;

    void Start()
    {
        // El primer enemigo aparecer� de inmediato o despu�s del primer intervalo
        proximoTiempoSpawn = Time.time + tiempoEntreSpawns;
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
        // Calcula una posici�n aleatoria en un c�rculo alrededor del SpawnManager
        Vector3 posicionAleatoria = Random.insideUnitCircle.normalized * distanciaSpawn;

        // Ajusta la Y a 0 (asumiendo juego 2D en XZ o 3D plano)
        Vector3 posicionSpawn = transform.position + new Vector3(posicionAleatoria.x, 0f, posicionAleatoria.y);

        // Instancia (crea) el enemigo en la posici�n calculada
        Instantiate(prefabEnemigo, posicionSpawn, Quaternion.identity);
    }
}