using UnityEngine;

public class AtaqueJugador : MonoBehaviour
{
    [Header("Configuración de Ataque")]
    public float radioAtaque = 2f;    // Distancia máxima para golpear enemigos
    public float danioAtaque = 10f;   // Cantidad de daño a infligir
    public KeyCode teclaAtaque = KeyCode.Q; // Tecla que activa el ataque

    [Header("Cooldown y Tags")]
    public float cooldownAtaque = 1.0f; // Tiempo de espera entre ataques
    private bool puedeAtacar = true;
    public string tagEnemigo = "Enemy"; // El tag de los objetivos a dañar

    void Update()
    {
        // 1. Detectar la pulsación de la tecla 'Q' y verificar el cooldown
        if (Input.GetKeyDown(teclaAtaque) && puedeAtacar)
        {
            EjecutarAtaque();
        }
    }

    void EjecutarAtaque()
    {
        // Inicia el cooldown inmediatamente
        puedeAtacar = false;
        Invoke("RestablecerAtaque", cooldownAtaque);

        Debug.Log("Ataque de Jugador (Q) activado. Buscando enemigos en radio: " + radioAtaque);

        // 2. Detectar todos los Colliders en el radio de ataque
        // Usamos Physics.OverlapSphere para simular un área de efecto (AoE)
        Collider[] enemigosAlcanzados = Physics.OverlapSphere(transform.position, radioAtaque);

        // 3. Iterar sobre los colliders encontrados y aplicar daño
        foreach (Collider enemigo in enemigosAlcanzados)
        {
            // Verificar si el objeto tiene la etiqueta "Enemy"
            if (enemigo.CompareTag(tagEnemigo))
            {
                // Intentar obtener el script de Salud
                Salud saludObjetivo = enemigo.GetComponent<Salud>();

                if (saludObjetivo != null)
                {
                    // Aplicar el daño
                    saludObjetivo.RecibirDano(danioAtaque);
                    Debug.Log($"Golpeado: {enemigo.name}. Daño: {danioAtaque}.");
                }
            }
        }
    }

    void RestablecerAtaque()
    {
        puedeAtacar = true;
        Debug.Log("Ataque de Jugador listo de nuevo.");
    }

    // Opcional: Para visualizar el radio de ataque en el Editor de Unity
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        // Dibuja una esfera que muestra el área de efecto del ataque
        Gizmos.DrawWireSphere(transform.position, radioAtaque);
    }
}