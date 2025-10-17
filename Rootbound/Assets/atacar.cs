using UnityEngine;

public class AtaquePersonaje : MonoBehaviour
{
    // Da�o que causa este ataque
    public float dano = 10f;

    // Debe coincidir con la Tag de tu objeto a da�ar (ej. "ObjetivoDestructible")
    public string etiquetaObjetivo = "ObjetivoDestructible";

    // Se ejecuta al tocar algo, SIEMPRE Y CUANDO la Hitbox est� ACTIVA
    private void OnTriggerEnter(Collider other)
    {
        // 1. Verifica la Tag (filtro)
        if (other.CompareTag(etiquetaObjetivo))
        {
            // 2. Intenta obtener el script de salud
            SaludObjeto salud = other.GetComponent<SaludObjeto>();

            // 3. Si lo tiene, aplica el da�o
            if (salud != null)
            {
                salud.RecibirDano(dano);
            }
        }
    }
}