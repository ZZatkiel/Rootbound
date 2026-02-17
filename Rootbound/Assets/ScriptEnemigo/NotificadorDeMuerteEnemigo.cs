using UnityEngine;

/*
 * ESTE SCRIPT ES UN NOTIFICADOR DE MUERTE PARA LOS ENEMIGOS.
 * SU FUNCIÓN ES AVISAR AL SPAWNER CUANDO UN ENEMIGO MUERE.
*/

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
