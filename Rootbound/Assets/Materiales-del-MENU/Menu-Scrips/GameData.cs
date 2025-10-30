using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;  // Singleton (una �nica instancia)
    public string playerName;         // Aqu� se guarda el nombre del jugador
    public int playerLevel;           // Ejemplo de m�s datos
    public float playerHealth;        // Ejemplo de m�s datos

    private void Awake()
    {
        // Si ya existe una instancia, destruir este duplicado
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Si no existe, establecer esta como �nica
        Instance = this;
        DontDestroyOnLoad(gameObject);  // Evita que se destruya al cambiar de escena
    }

    // M�todo para guardar el nombre
    public void SetPlayerName(string name)
    {
        playerName = name;
        Debug.Log("Nombre del jugador guardado: " + playerName);
    }

    // M�todo opcional para guardar m�s datos en el futuro
    public void SaveProgress(int level, float health)
    {
        playerLevel = level;
        playerHealth = health;
        Debug.Log("Progreso guardado: Nivel " + playerLevel + ", Vida " + playerHealth);
    }
}
