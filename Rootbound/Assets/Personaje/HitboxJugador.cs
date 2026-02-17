using UnityEngine;

/*
 * ESTE SCRIPT NOS PERMITE SIMULAR UN EMPUJE USANDO LA RESTA DE VECTORES PARA OBTENER LA DIRECCION Y USANDO MOVE
 * LO HAGO DE ESTA MANERA YA QUE COMO NO SON COMPATIBLES LOS SISTEMAS DE FISICAS DEL RIGIDBODY Y EL CHARACTERCONTROLLER, TUVE QUE IMPROVISAR
 * 
*/ 

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

            Vector3 pushDir = Jugador.transform.position - other.transform.position;
            pushDir.y = 0f;
            pushDir.Normalize();

            cc.Move(pushDir * 2 * Time.deltaTime);

        }
    }
}
