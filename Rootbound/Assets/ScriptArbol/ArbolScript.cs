using UnityEngine;

//SCRIPT QUE MANEJA EL DAÑO/MUERTE DEL ARBOL, EL CUAL SI SE MUERE EL ARBOL, SE PIERDE LA PARTIDA

public class ArbolScript : MonoBehaviour
{
    public float vidaMaxima = 100f;
    public float vidaActual;
    public GameObject manejadorDerrota;

    void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void recibirDaño(float cantidad)
    {

        vidaActual -= cantidad;

        Debug.Log($"Arbol recibió {cantidad} daño. Vida restante: {vidaActual}");


        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    public void Morir()
    {
        Debug.Log("El arbol murió.");

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        manejadorDerrota.GetComponent<PartidaPerdidaScript>().MuerteDesactivadorScripts();

        manejadorDerrota.SetActive(true);

    }
}