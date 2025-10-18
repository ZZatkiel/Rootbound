using UnityEngine;
using System.Collections;

public class EnemigoDanoColision : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadMovimiento = 2f;

    [Header("Daño")]
    public int danoPorColision = 10;
    public float cooldownDano = 1.0f;

    private Transform objetivo;      // Transform del jugador
    private bool puedeDanar = true; // Controla el cooldown

    void Start()
    {
        // Buscar el jugador por tag (se convertirá en null cuando el jugador sea destruido)
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            objetivo = jugador.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró un objeto con tag 'Player'.");
        }
    }

    void Update()
    {
        // 🚨 VERIFICACIÓN CRÍTICA: Detiene la persecución si el objetivo ya no existe.
        if (objetivo == null) return;

        Vector3 vectorAlObjetivo = objetivo.position - transform.position;

        // Evita el error "Look rotation viewing vector is zero"
        if (vectorAlObjetivo.magnitude > 0.001f)
        {
            Vector3 direccion = vectorAlObjetivo.normalized;

            // Mueve al enemigo hacia el jugador
            transform.Translate(direccion * velocidadMovimiento * Time.deltaTime, Space.World);

            // Gira al enemigo hacia el jugador
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * 5f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!puedeDanar) return;

        if (other.CompareTag("Player"))
        {
            // Busca el script en el objeto o sus padres
            ControlPersonaje personaje = other.GetComponentInParent<ControlPersonaje>();

            if (personaje != null)
            {
                // Verifica que el jugador NO esté muerto antes de hacer daño
                if (!personaje.EstaMuerto)
                {
                    personaje.RecibirDano(danoPorColision);
                    StartCoroutine(AplicarCooldown());
                    Debug.Log("Jugador golpeado. Daño: " + danoPorColision);
                }
                else
                {
                    Debug.Log("El jugador ya está muerto. No se aplica daño.");
                }
            }
            // NOTA: Si 'personaje' es null, el objeto ya está siendo destruido (por el 0.1s de retraso)
            // y no es necesario un mensaje de error aquí.
        }
    }

    private IEnumerator AplicarCooldown()
    {
        puedeDanar = false;
        yield return new WaitForSeconds(cooldownDano);
        puedeDanar = true;
    }
}