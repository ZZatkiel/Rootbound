using UnityEngine;

public class FuncionSalir : MonoBehaviour
{
    public void SalirDelJuego()
    {
        Debug.Log("Saliendo de RootBound....");
        Application.Quit(); // Esta linea hace que la aplicacion se cierre.
    }



}
//Este scriot es para salir del juego cuando se presiona un boton en el menu principal.
//Este script esta agregado en el InterfazMenu, donde luego se asigna al boton salir.
