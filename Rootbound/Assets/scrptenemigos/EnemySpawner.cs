using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnInterval = 3f; // Un enemigo spawnea cada 3 segundos.

    [Header("Configuración de Ciclo")]
    private const int MAX_ENEMIES_CREATED = 20; // Límite de enemigos por oleada
    private const float BREAK_DURATION = 30f;   // Duración del descanso en segundos

    // Variables de estado
    private int enemiesCreatedInCycle = 0;
    private int waveNumber = 1;
    private float timer = 0f;

    // Control de estados
    private bool isSpawning = true;
    private bool isBreakActive = false;
    private bool secondWaveCompleted = false;

    private float breakTimer = BREAK_DURATION;

    // Lista para almacenar los puntos de spawn encontrados (Norte, Sur, Este, Oeste)
    private List<Transform> spawnPointsList;

    void Start()
    {
        FindNamedSpawnPoints();
        // El ciclo comienza en la Oleada 1
        Debug.Log("--- INICIO ---: Spawner activado en Oleada 1 (Máx. 20 enemigos).");
    }

    void FindNamedSpawnPoints()
    {
        spawnPointsList = new List<Transform>();
        string[] spawnNames = { "norte", "sur", "este", "oeste" };

        foreach (string name in spawnNames)
        {
            GameObject spawnGO = GameObject.Find(name);
            if (spawnGO != null)
            {
                spawnPointsList.Add(spawnGO.transform);
            }
        }

        if (spawnPointsList.Count == 0)
        {
            Debug.LogError("No se encontraron puntos de spawn (Norte, Sur, Este, Oeste). El spawner no funcionará.");
            isSpawning = false; // Detener el spawn si no hay puntos
        }
    }

    void Update()
    {
        // Si la Oleada 2 ha terminado, el spawner no hace nada
        if (secondWaveCompleted) return;

        if (isBreakActive)
        {
            HandleBreakTimer();
        }
        else if (isSpawning)
        {
            HandleSpawnTimer();
        }
    }

    void HandleSpawnTimer()
    {
        // Controlar el temporizador de spawn (1 enemigo cada 3 segundos)
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnSingleEnemy();
            timer = 0f; // Resetear el temporizador

            // 1. Verificar el límite de creación de enemigos
            if (enemiesCreatedInCycle >= MAX_ENEMIES_CREATED)
            {
                if (waveNumber == 1)
                {
                    // Transición a la FASE DE DESCANSO
                    isSpawning = false;
                    isBreakActive = true;
                    waveNumber = 2; // Preparar para la Oleada 2
                    breakTimer = BREAK_DURATION; // Reiniciar el temporizador de descanso

                    // DEBUG: Aviso de inicio de temporizador
                    Debug.Log($"CONTADOR LLEGÓ A {MAX_ENEMIES_CREATED}. INICIANDO TEMPORIZADOR DE DESCANSO DE {BREAK_DURATION} SEGUNDOS.");
                }
                else if (waveNumber == 2)
                {
                    // FIN PERMANENTE: La Oleada 2 ha terminado su cuota de 20
                    isSpawning = false;
                    secondWaveCompleted = true;
                    Debug.Log("--- FIN DEL SPAWN ---: La Oleada 2 ha completado su límite de 20. Spawning detenido permanentemente.");
                }
            }
        }
    }

    void HandleBreakTimer()
    {
        breakTimer -= Time.deltaTime;

        // DEBUG: Aviso del tiempo restante
        Debug.Log($"Temporizador de Descanaso: {Mathf.CeilToInt(breakTimer)} segundos restantes.");

        if (breakTimer <= 0f)
        {
            // Transición a la OLEADA 2
            isBreakActive = false;
            isSpawning = true;
            enemiesCreatedInCycle = 0; // Contador de creación vuelve a 0
            timer = 0f;
            Debug.Log("--- FIN DEL DESCANSO ---: Iniciando Oleada 2 (Máx. 20 enemigos). El contador vuelve a 0.");
        }
    }

    void SpawnSingleEnemy()
    {
        // Verificaciones de seguridad
        if (spawnPointsList.Count == 0 || enemyPrefabs.Length == 0) return;

        // Spawnea UN solo enemigo de forma aleatoria en uno de los 4 puntos
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        Vector3 spawnPosition = GetRandomSpawnPoint();
        GameObject enemy = Instantiate(enemyPrefabs[randomIndex], spawnPosition, Quaternion.identity);

        // Lógica de Asignación de Target (si es necesaria)
        GameObject arbolGO = GameObject.Find("Arbol");
        if (arbolGO != null)
        {
            EnemyBehavior enemyBehavior = enemy.GetComponent<EnemyBehavior>();
            if (enemyBehavior != null)
            {
                enemyBehavior.target = arbolGO.transform;
            }
        }

        // Incrementar el contador
        enemiesCreatedInCycle++;
        Debug.Log($"Enemigo creado. Total en ciclo (Oleada {waveNumber}): {enemiesCreatedInCycle} / {MAX_ENEMIES_CREATED}");
    }

    Vector3 GetRandomSpawnPoint()
    {
        int randomSpawnIndex = Random.Range(0, spawnPointsList.Count);
        return spawnPointsList[randomSpawnIndex].position;
    }
}