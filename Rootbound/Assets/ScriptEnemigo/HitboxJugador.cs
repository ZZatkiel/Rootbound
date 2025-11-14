using UnityEngine;

public class HitboxJugador : MonoBehaviour
{
    private string tagJugador = "Player";
    GameObject Jugador;

    private void Start()
    {
        Jugador = GameObject.FindWithTag(tagJugador);

    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            CharacterController cc = Jugador.GetComponent<CharacterController>();

            Debug.Log("El enemigo toco al jugador");

            Vector3 pushDir = Jugador.transform.position - other.transform.position;
            pushDir.y = 0f; // Ignora la componente vertical
            pushDir.Normalize();

            // Empuja al jugador
            cc.Move(pushDir * 2 * Time.deltaTime);

            // IDEA: 
            // DESACTIVAR EL MOVIMIENTO PARA IR HACIA ADELANTE DE MI CHARACTERCONTROLLER
        }
    }
}
