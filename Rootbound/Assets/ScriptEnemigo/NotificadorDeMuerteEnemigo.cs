using UnityEngine;

public class NotificadorDeMuerteEnemigo : MonoBehaviour
{
    [HideInInspector] public SpawnerEnemigoManager spawner;

    public void ReportarMuerte()
    {
        if (spawner != null)
            spawner.EnemyDied(); // notifica al spawner
        // opcional: destruir este componente para limpieza
        Destroy(this);
    }
}
