using UnityEngine;

public class kjklj : MonoBehaviour
{
    private void Start()
    {
        int puntaje = GameManagerSC.Instancia.scoreManager.obtenerPuntos();
        Debug.Log(puntaje);
    }

}
