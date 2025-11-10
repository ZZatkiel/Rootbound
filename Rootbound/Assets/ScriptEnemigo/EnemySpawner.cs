using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuración de Plantilla")]
    // Usaremos esta referencia solo para crear la plantilla oculta en Start()
    public GameObject enemyPrefabBase;

    // El objeto que NUNCA se destruye y sirve como plantilla de clonación
    private GameObject permanentEnemyTemplate;

    [Header("Spawn y Ciclo")]
    public float spawnInterval = 3f;

    // Configuración de Ciclo (sin cambios)
    private const int MAX_ENEMIES_CREATED = 20;
    private const float BREAK_DURATION = 30f;

    // Variables de estado
    private int enemiesCreatedInCycle = 0;
    private int waveNumber = 1;
    private float timer = 0f;

    // Control de estados
    private bool isSpawning = true;
    private bool isBreakActive = false;
    private bool secondWaveCompleted = false;
    private bool spawnDetenidoPorMuerte = false;

    private float breakTimer = BREAK_DURATION;
    private List<Transform> spawnPointsList;

    void Start()
    {
        // 1. CREAR LA PLANTILLA PERMANENTE DEBAJO DEL MAPA
        if (enemyPrefabBase != null)
        {
            // Creamos la plantilla muy por debajo del mapa (-1000) y la deshabilitamos.
            permanentEnemyTemplate = Instantiate(enemyPrefabBase, new Vector3(0, -1000f, 0), Quaternion.identity);
            permanentEnemyTemplate.name = "Enemy Template (DO NOT DESTROY)";
            permanentEnemyTemplate.SetActive(false);
        }
        else
        {
            Debug.LogError("El Prefab Base del enemigo es nulo. Asignar un Prefab en el Inspector.");
            isSpawning = false;
        }

        FindNamedSpawnPoints();
        Debug.Log("--- INICIO ---: Spawner activado en Oleada 1 (Máx. 20 enemigos).");
    }

    void FindNamedSpawnPoints()
    {
        // ... (Tu lógica de búsqueda de puntos de spawn, sin cambios)
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
            Debug.LogError("No se encontraron puntos de spawn. El spawner no funcionará.");
            isSpawning = false;
        }
    }

    void Update()
    {
        // ... (Tu lógica de ciclos y verificación de objetivos, sin cambios)
        if (!spawnDetenidoPorMuerte)
        {
            VerificarEstadoDeObjetivos();
        }

        if (secondWaveCompleted || spawnDetenidoPorMuerte) return;

        if (isBreakActive)
        {
            HandleBreakTimer();
        }
        else if (isSpawning)
        {
            HandleSpawnTimer();
        }
    }

    void VerificarEstadoDeObjetivos()
    {
        GameObject jugadorGO = GameObject.FindWithTag("Player");
        GameObject arbolGO = GameObject.FindWithTag("Arbol");

        // REGLA: Si el Árbol muere, el Player también pierde
        if (arbolGO == null)
        {
            Debug.LogWarning("--- GAME OVER ---: ¡El Árbol ha sido destruido! Deteniendo Spawner.");
            DetenerSpawnPermanentemente();

            if (jugadorGO != null)
            {
                Salud saludJugador = jugadorGO.GetComponent<Salud>();
                if (saludJugador != null)
                {
                    saludJugador.Morir();
                }
                else
                {
                    Destroy(jugadorGO);
                }
            }
            return;
        }

        if (jugadorGO == null)
        {
            Debug.LogWarning("--- GAME OVER ---: El Jugador ha sido destruido. Deteniendo Spawner.");
            DetenerSpawnPermanentemente();
            return;
        }
    }

    void DetenerSpawnPermanentemente()
    {
        isSpawning = false;
        isBreakActive = false;
        spawnDetenidoPorMuerte = true;
    }

    void HandleSpawnTimer()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnSingleEnemy();
            timer = 0f;

            if (enemiesCreatedInCycle >= MAX_ENEMIES_CREATED)
            {
                if (waveNumber == 1)
                {
                    isSpawning = false;
                    isBreakActive = true;
                    waveNumber = 2;
                    breakTimer = BREAK_DURATION;
                    Debug.Log($"CONTADOR LLEGÓ A {MAX_ENEMIES_CREATED}. INICIANDO TEMPORIZADOR DE DESCANSO DE {BREAK_DURATION} SEGUNDOS.");
                }
                else if (waveNumber == 2)
                {
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
        Debug.Log($"Temporizador de Descanaso: {Mathf.CeilToInt(breakTimer)} segundos restantes.");

        if (breakTimer <= 0f)
        {
            isBreakActive = false;
            isSpawning = true;
            enemiesCreatedInCycle = 0;
            timer = 0f;
            Debug.Log("--- FIN DEL DESCANSO ---: Iniciando Oleada 2 (Máx. 20 enemigos). El contador vuelve a 0.");
        }
    }

    // --- LÓGICA DE SPAWN SIMPLE (CLONAR PLANTILLA OCULTA) ---
    void SpawnSingleEnemy()
    {
        if (spawnPointsList.Count == 0 || permanentEnemyTemplate == null) return;

        Vector3 spawnPosition = GetRandomSpawnPoint();

        // 1. Clonar la plantilla oculta y crear un enemigo activo
        GameObject newEnemy = Instantiate(permanentEnemyTemplate, spawnPosition, Quaternion.identity);

        // 2. Asegurarse de que el nuevo enemigo esté activo para el juego
        if (!newEnemy.activeSelf)
        {
            newEnemy.SetActive(true);
        }

        // 3. (Opcional) Renombrar para debug
        newEnemy.name = "Enemy Spawned " + enemiesCreatedInCycle;

        enemiesCreatedInCycle++;
        Debug.Log($"Enemigo creado. Total en ciclo (Oleada {waveNumber}): {enemiesCreatedInCycle} / {MAX_ENEMIES_CREATED}");
    }

    Vector3 GetRandomSpawnPoint()
    {
        int randomSpawnIndex = Random.Range(0, spawnPointsList.Count);
        return spawnPointsList[randomSpawnIndex].position;
    }
}