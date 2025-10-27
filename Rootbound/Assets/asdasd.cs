using UnityEngine;

public class asdasd : MonoBehaviour
{
    

    void Start()
    {
        GameManagerSC.Instancia.scoreManager.modificarPuntos(1500);
        Tienda otroScript = GetComponent<Tienda>();

        // Ahora puedo acceder a variables o funciones públicas de ScriptB
        otroScript.MostrarArmasEnUI();
    }

}
