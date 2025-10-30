using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  // Los tres prefabs de enemigos
    public Transform[] spawnPoints;    // Los puntos de spawn
    public float spawnInterval = 3f;   // Intervalo entre oleadas
    private float timer = 0f;

    private int waveNumber = 1;        // Comienza en la oleada 1

    void Update()
    {
        // Controlar el tiempo entre oleadas
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemies();
            timer = 0f;  // Resetear el temporizador
        }
    }

    void SpawnEnemies()
    {
        // Determina la cantidad de enemigos para esta oleada
        int enemiesToSpawn = 5 + (waveNumber - 1) * 7;  // 5 enemigos en la primera oleada, 12 en la segunda, 19 en la tercera, etc.

        // Spawnear enemigos
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // Elegir un enemigo aleatorio
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemy = Instantiate(enemyPrefabs[randomIndex], GetRandomSpawnPoint(), Quaternion.identity);

            // Asignar el objetivo (el árbol en este caso)
            enemy.GetComponent<EnemyBehavior>().target = GameObject.Find("Arbol").transform;  // Buscamos el objeto "Arbol" y lo asignamos como target
        }

        // Incrementar el número de la siguiente oleada
        waveNumber++;
    }

    Vector3 GetRandomSpawnPoint()
    {
        // Elegir un punto de spawn aleatorio de los puntos disponibles
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomSpawnIndex].position;
    }
}
