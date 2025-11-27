using System.Collections;
using UnityEngine;

public class SpawnerEnemigoManager : MonoBehaviour
{
    [Header("Configuración Enemigos")]
    [SerializeField] private InformacionEnemigo[] enemigosDisponibles;
    [SerializeField] private Transform[] puntosSpawn;

    [Header("Configuración Rondas")]
    [SerializeField] private int EnemigosBasePorRonda = 10;
    [SerializeField] private int IncrementarPorRonda = 5;
    [SerializeField] private int VariacionDeEnemigos = 3;
    [SerializeField] private int minirondasPorRonda = 4;

    [Header("Tiempos")]
    [SerializeField] private float tiempoEntreSpawns = 0.6f;
    [SerializeField] private float tiempoEntreMinirondas = 6f;
    [SerializeField] private float tiempoEntreRondas = 12f;

    [Header("Referencia a la Tienda (opcional)")]
    private Tienda tienda;

    private int rondaActual = 0;
    private bool corriendo = true;
    private int enemigosSpawneadosEstaRonda = 0;

    private int enemigosVivos = 0;

    int miniRondaActual = 0;

    private void Start()
    {
        tienda = GetComponent<Tienda>();

        // validaciones mínimas
        if (minirondasPorRonda <= 0) minirondasPorRonda = 1;

        StartCoroutine(RondasLoop());
    }

    private IEnumerator RondasLoop()
    {
        while (corriendo)
        {
            // Nueva ronda
            GameManagerSC.Instancia.roundManager.avanzarRonda();
            rondaActual = (int)GameManagerSC.Instancia.roundManager.obtenerRonda();

            enemigosVivos = 0; // reset por si quedó algo raro
            enemigosSpawneadosEstaRonda = 0;
            Debug.Log($"--- RONDA {rondaActual} ---");

            // Calcular cuántos enemigos en la ronda
            int cantidadBaseEnemigos = EnemigosBasePorRonda + (IncrementarPorRonda * (rondaActual - 1));
            int variacion = Random.Range(0, VariacionDeEnemigos + 1);
            int enemigosTotales = Mathf.Max(1, cantidadBaseEnemigos + variacion);

            // Calcular enemigos por minironda
            int enemigosPorMinironda = Mathf.CeilToInt((float)enemigosTotales / minirondasPorRonda);

            // Recorrer minirondas
            for (int m = 0; m <= minirondasPorRonda; m++)
            {
                Debug.Log($" -> Minironda {m}");
                miniRondaActual = m;


                // Abrir tienda en la mitad (si existe)
                if (m == Mathf.CeilToInt(minirondasPorRonda / 2f) && tienda != null)
                {
                    tienda.MostrarArmasEnUI();

                    // Esperamos a que el jugador cierre la tienda si la tienda usa tiendaEnabled
                    // Si tu Tienda no tiene esa propiedad, podés eliminar la línea siguiente.
                    yield return new WaitWhile(() => tienda != null && tienda.tiendaEnabled);
                }

                // Calcular cuántos enemigos tocan en esta minironda
                int enemigosEstaMiniRonda = enemigosPorMinironda;

                // Última minironda corrige sobrantes
                if (m == minirondasPorRonda)
                    enemigosEstaMiniRonda = Mathf.Max(0, enemigosTotales - enemigosSpawneadosEstaRonda);

                if (enemigosEstaMiniRonda > 0)
                {
                    // Spawnear la minironda (esto incrementa enemiesAlive por cada spawn)
                    yield return StartCoroutine(SpawnerMinironda(enemigosEstaMiniRonda));

                    // NUEVO: Esperar hasta que los enemigos vivos lleguen a 0 (es decir, que el jugador los mate)
                    yield return new WaitUntil(() => enemigosVivos == 0);
                }

                // Descanso entre minirondas
                yield return new WaitForSeconds(tiempoEntreMinirondas);
            }

            Debug.Log($"--- FIN RONDA {rondaActual} ---");
            Debug.Log($"Descanso para la siguiente ronda de {tiempoEntreRondas} segundos");
            yield return new WaitForSeconds(tiempoEntreRondas);
        }
    }

    private IEnumerator SpawnerMinironda(int cantidad)
    {
        int spawned = 0;

        while (spawned < cantidad)
        {
            SpawnEnemigoRandom();
            spawned++;
            enemigosSpawneadosEstaRonda++;

            yield return new WaitForSeconds(tiempoEntreSpawns);
        }
    }

    private void SpawnEnemigoRandom()
    {
        // Validaciones mínimas
        if (enemigosDisponibles == null || enemigosDisponibles.Length == 0)
        {
            Debug.LogWarning("No hay enemigos configurados en SpawnerEnemigoManager.");
            return;
        }

        if (puntosSpawn == null || puntosSpawn.Length == 0)
        {
            Debug.LogWarning("No hay puntos de spawn configurados en SpawnerEnemigoManager.");
            return;
        }

        // Elegir enemigo y punto
        InformacionEnemigo data = enemigosDisponibles[Random.Range(0, enemigosDisponibles.Length)];
        Transform spawnPoint = puntosSpawn[Random.Range(0, puntosSpawn.Length)];

        // Instanciar
        GameObject obj = Instantiate(data.Prefab, spawnPoint.position, Quaternion.identity);

        // Agregar reporter que notificará la muerte de este enemigo
        var reporter = obj.AddComponent<NotificadorDeMuerteEnemigo>();
        reporter.spawner = this;

        // Aumentar contador de enemigos vivos
        EnemySpawned();

        // Dificultad
        float dificultad = 1f;
        if (GameManagerSC.Instancia != null && GameManagerSC.Instancia.roundManager != null)
            dificultad = GameManagerSC.Instancia.roundManager.multiplicadorDeDifcultad();

        // Inicializar (si existe LogicaEnemigo)
        var logica = obj.GetComponent<LogicaEnemigo>();
        if (logica != null)
        {
            logica.Inicializador(data, dificultad);
        }
        else
        {
            Debug.LogWarning("Objeto enemigo instanciado no tiene LogicaEnemigo.");
        }
    }

    // ---------- Métodos para el contador de enemigos vivos ----------
    public void EnemySpawned()
    {
        enemigosVivos++;
        // Debug.Log($"Enemy spawned. Alive: {enemiesAlive}");
    }

    public void EnemyDied()
    {
        enemigosVivos = Mathf.Max(0, enemigosVivos - 1);
        // Debug.Log($"Enemy died. Alive: {enemiesAlive}");
    }

    // Métodos para controlar spawner externamente si querés
    public void StopSpawner()
    {
        corriendo = false;
        StopAllCoroutines();
    }

    public void StartSpawner()
    {
        if (!corriendo)
        {
            corriendo = true;
            StartCoroutine(RondasLoop());
        }
    }


    public int obtenerMiniRondaActual()
    {
        return miniRondaActual;
    }

}
