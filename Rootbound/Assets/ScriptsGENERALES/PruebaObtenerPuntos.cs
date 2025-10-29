using UnityEngine;

public class PruebaObtenerPuntos : MonoBehaviour
{
    void Start()
    {
        GameManagerSC.Instancia.scoreManager.ResetearPuntos(1500);
        
    }

}
