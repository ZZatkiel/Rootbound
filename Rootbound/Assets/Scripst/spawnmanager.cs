using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Variables que asignaremos en el Inspector de Unity
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnTime = 5f;
    public int maxEnemies = 10;      // L�mite m�ximo de enemigos a crear

    // Variables internas del script
    private float timer;
    private int enemiesSpawnedCount = 0; // Contador de enemigos ya creados

    void Start()
    {
        timer = spawnTime;
    }

    void Update()
    {
        // 1. Condici�n de Parada: Si ya creamos el m�ximo, detenemos el spawn.
        if (enemiesSpawnedCount >= maxEnemies)
        {
            return; // Salimos de la funci�n Update
        }

        // Si no hemos llegado al l�mite, hacemos la cuenta regresiva
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = spawnTime; // Reinicia el temporizador a 5 segundos
        }
    }

    void SpawnEnemy()
    {
        // 2. Verificaci�n de seguridad: Asegurar que hay al menos 2 puntos asignados.
        // Si tienes m�s de 2, la funci�n Random.Range(0, 2) solo usar� los dos primeros (�ndices 0 y 1).
        if (spawnPoints.Length < 2)
        {
            Debug.LogError("Error en SpawnManager: Necesitas asignar al menos dos puntos de spawn.");
            return;
        }

        // 3. Selecci�n Aleatoria de los 2 Puntos:
        // Random.Range(0, 2) devuelve 0 o 1, que son los �ndices de nuestros dos puntos.
        int spawnPointIndex = Random.Range(0, 2);

        // 4. Crear el Enemigo (Instantiate):
        Instantiate(enemyPrefab,
                    spawnPoints[spawnPointIndex].position,
                    spawnPoints[spawnPointIndex].rotation);

        // 5. Aumentar el Contador:
        enemiesSpawnedCount++;

        // Opcional: Para ver en la consola cu�ntos van.
        Debug.Log("Enemigo creado. Total: " + enemiesSpawnedCount + "/" + maxEnemies);
    }
}