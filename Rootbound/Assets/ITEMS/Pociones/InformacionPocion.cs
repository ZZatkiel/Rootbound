using UnityEngine;

[CreateAssetMenu(menuName = "EstadisticaPocion")]
public class InformacionPocion : ScriptableObject
{
    public string nombre;
    [TextArea] public string descripcion;
    public Sprite imagenInventario;
    public GameObject prefabModelo;    // Prefab que se instancia en la mano
    public CategoriaItemEnum categoriaItem;

    [Header("Estadísticas")]
    public int duracion = 10;
    public int cantidad = 1;
    public int precio = 0;

    public string Nombre { get { return nombre; } }
    public string Descripcion { get { return descripcion; } }
    public Sprite ImagenInventario { get { return imagenInventario; } }
    public GameObject PrefabModelo { get { return prefabModelo; } }
    public CategoriaItemEnum CategoriaItem { get { return categoriaItem; } }
    public int Duracion { get { return duracion; } }
    public int Cantidad { get { return cantidad; } }
    public int Precio { get { return precio; } }



}