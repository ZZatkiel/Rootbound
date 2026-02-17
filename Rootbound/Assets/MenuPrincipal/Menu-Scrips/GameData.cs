using UnityEngine;

//Metodo el cual permite obtener los datos del personaje
// La idea es usarlo para guardar/cargar la partida con los datos del jugador
public class GameData : MonoBehaviour
{
    public static GameData Instance;  
    public string playerName;         
    public int playerLevel;          
    public float playerHealth;        

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
        Debug.Log("Nombre del jugador guardado: " + playerName);
    }

    public void SaveProgress(int level, float health)
    {
        playerLevel = level;
        playerHealth = health;
        Debug.Log("Progreso guardado: Nivel " + playerLevel + ", Vida " + playerHealth);
    }
}
