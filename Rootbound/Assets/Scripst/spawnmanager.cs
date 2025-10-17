using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Variables que asignaremos en el Inspector de Unity
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnTime = 5f;
    public int maxEnemies = 10;      // Límite máximo de enemigos a crear

    // Variables internas del script
    private float timer;
    private int enemiesSpawnedCount = 0; // Contador de enemigos ya creados

    void Start()
    {
        timer = spawnTime;
    }

    void Update()
    {
        // 1. Condición de Parada: Si ya creamos el máximo, detenemos el spawn.
        if (enemiesSpawnedCount >= maxEnemies)
        {
            return; // Salimos de la función Update
        }

        // Si no hemos llegado al límite, hacemos la cuenta regresiva
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = spawnTime; // Reinicia el temporizador a 5 segundos
        }
    }

    void SpawnEnemy()
    {
        // 2. Verificación de seguridad: Asegurar que hay al menos 2 puntos asignados.
        // Si tienes más de 2, la función Random.Range(0, 2) solo usará los dos primeros (índices 0 y 1).
        if (spawnPoints.Length < 2)
        {
            Debug.LogError("Error en SpawnManager: Necesitas asignar al menos dos puntos de spawn.");
            return;
        }

        // 3. Selección Aleatoria de los 2 Puntos:
        // Random.Range(0, 2) devuelve 0 o 1, que son los índices de nuestros dos puntos.
        int spawnPointIndex = Random.Range(0, 2);

        // 4. Crear el Enemigo (Instantiate):
        Instantiate(enemyPrefab,
                    spawnPoints[spawnPointIndex].position,
                    spawnPoints[spawnPointIndex].rotation);

        // 5. Aumentar el Contador:
        enemiesSpawnedCount++;

        // Opcional: Para ver en la consola cuántos van.
        Debug.Log("Enemigo creado. Total: " + enemiesSpawnedCount + "/" + maxEnemies);
    }
}