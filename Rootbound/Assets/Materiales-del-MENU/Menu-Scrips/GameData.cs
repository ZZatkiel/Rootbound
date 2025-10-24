using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;  // Singleton (una única instancia)
    public string playerName;         // Aquí se guarda el nombre del jugador
    public int playerLevel;           // Ejemplo de más datos
    public float playerHealth;        // Ejemplo de más datos

    private void Awake()
    {
        // Si ya existe una instancia, destruir este duplicado
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Si no existe, establecer esta como única
        Instance = this;
        DontDestroyOnLoad(gameObject);  // Evita que se destruya al cambiar de escena
    }

    // Método para guardar el nombre
    public void SetPlayerName(string name)
    {
        playerName = name;
        Debug.Log("Nombre del jugador guardado: " + playerName);
    }

    // Método opcional para guardar más datos en el futuro
    public void SaveProgress(int level, float health)
    {
        playerLevel = level;
        playerHealth = health;
        Debug.Log("Progreso guardado: Nivel " + playerLevel + ", Vida " + playerHealth);
    }
}
