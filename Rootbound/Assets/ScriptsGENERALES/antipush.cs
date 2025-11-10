using UnityEngine;

public class PlayerAntiPush : MonoBehaviour
{
    // Esta función es específica del CharacterController y se llama
    // cada vez que colisiona con otro Collider.
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // El Rigidbody del objeto que nos golpeó (el enemigo)
        Rigidbody body = hit.collider.attachedRigidbody;

        // Si el objeto golpeado no tiene Rigidbody o si está marcado como Kinematic,
        // no hay nada que empujar, así que salimos.
        if (body == null || body.isKinematic)
        {
            return;
        }

        // Si llegamos aquí, un Rigidbody (el enemigo) está empujando al CharacterController (el jugador).

        // La forma más simple de evitar el empuje es establecer la fuerza de empuje en cero.
        // Como el enemigo te está golpeando, el jugador aplica una fuerza contraria.

        // Opción 1: Empuje CERO (la solución que quieres)
        // Establecer la velocidad de ese Rigidbody (el enemigo) en cero.
        // Usamos linearVelocity para las versiones modernas de Unity.
        body.linearVelocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;

        // Nota: Si esto no funciona, significa que la velocidad del enemigo es recalculada
        // inmediatamente por su script. En ese caso, la única solución es que el enemigo
        // marque al jugador como objetivo de anti-empuje, o aplicar la Solución 2.
    }
}