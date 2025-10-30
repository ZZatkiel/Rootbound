using UnityEngine;

public class PlayerAntiPush : MonoBehaviour
{
    // Esta funci�n es espec�fica del CharacterController y se llama
    // cada vez que colisiona con otro Collider.
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // El Rigidbody del objeto que nos golpe� (el enemigo)
        Rigidbody body = hit.collider.attachedRigidbody;

        // Si el objeto golpeado no tiene Rigidbody o si est� marcado como Kinematic,
        // no hay nada que empujar, as� que salimos.
        if (body == null || body.isKinematic)
        {
            return;
        }

        // Si llegamos aqu�, un Rigidbody (el enemigo) est� empujando al CharacterController (el jugador).

        // La forma m�s simple de evitar el empuje es establecer la fuerza de empuje en cero.
        // Como el enemigo te est� golpeando, el jugador aplica una fuerza contraria.

        // Opci�n 1: Empuje CERO (la soluci�n que quieres)
        // Establecer la velocidad de ese Rigidbody (el enemigo) en cero.
        // Usamos linearVelocity para las versiones modernas de Unity.
        body.linearVelocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;

        // Nota: Si esto no funciona, significa que la velocidad del enemigo es recalculada
        // inmediatamente por su script. En ese caso, la �nica soluci�n es que el enemigo
        // marque al jugador como objetivo de anti-empuje, o aplicar la Soluci�n 2.
    }
}