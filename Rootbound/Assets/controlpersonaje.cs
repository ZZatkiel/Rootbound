using UnityEngine;
using UnityEngine.InputSystem; // ¡IMPORTANTE! Necesario para usar Keyboard.current

public class ControlPersonaje : MonoBehaviour
{
    public GameObject hitboxAtaque;
    public float duracionAtaque = 0.2f;

    private bool estaGolpeando = false;

    void Update()
    {
        // 🚨 AQUÍ ESTÁ EL CAMBIO CLAVE 🚨
        // Detecta la tecla 'Q' (puedes cambiar 'qKey' por otra tecla si quieres)
        if (Keyboard.current.qKey.wasPressedThisFrame && !estaGolpeando)
        {
            IniciarGolpe();
        }
    }

    void IniciarGolpe()
    {
        if (hitboxAtaque == null) return;

        estaGolpeando = true;
        hitboxAtaque.SetActive(true);
        Invoke("FinalizarGolpe", duracionAtaque);
    }

    void FinalizarGolpe()
    {
        hitboxAtaque.SetActive(false);
        estaGolpeando = false;
    }
}