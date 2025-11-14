using UnityEngine;

public enum tiposDeEnemigo
{
    Ogro,
    Demonio,
    Mutante
}

[CreateAssetMenu(fileName = "EnemyData", menuName = "Crear Dato Enemigo")]
public class InformacionEnemigo : ScriptableObject
{
    [SerializeField] private string nombre;
    [SerializeField] private float vida;
    [SerializeField] private tiposDeEnemigo tipoDeEnemigo;
    [SerializeField] private float daño;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float velocidadAtaque;
    [SerializeField] private GameObject modelo;

    public string Nombre { get { return nombre; }}
    public float Vida { get { return vida; } }
    public tiposDeEnemigo TipoDeEnemigo { get { return tipoDeEnemigo; } }
    public float Daño { get { return daño; } }
    public float VelocidadMovimiento { get { return velocidadMovimiento; } }
    public float VelocidadAtaque { get { return velocidadAtaque; } }
    public GameObject Prefab { get { return modelo; } }



}
