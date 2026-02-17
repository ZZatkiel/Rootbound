using UnityEngine;

/*
 * USAMOS ESTE SCRIPT PARA CREAR OBJETOS DESDE EL EDITOR Y LUEGO OBTENER SUS CARACTERISTICAS POR MEDIO DE REFERENCIA 
*/

[CreateAssetMenu(menuName = "EstadisticaArma")]
public class InformacionArma : ScriptableObject
{
    public string nombre;
    [TextArea] public string descripcion;
    public Sprite imagenInventario;
    public GameObject prefabModelo;
    public RarezaArmas rareza;
    public CategoriaItemEnum categoriaItem;

    [Header("Estadísticas")]
    public float daño = 10f;
    public float velocidadAtaque = 1f;
    public float critico = 0f;
    public int precio = 0;

    public string Nombre { get { return nombre; } }
    public string Descripcion { get { return descripcion; } }
    public Sprite ImagenInventario { get { return imagenInventario; } }
    public GameObject PrefabModelo { get { return prefabModelo; } }
    public RarezaArmas Rareza { get { return rareza; } }
    public CategoriaItemEnum CategoriaItem { get { return categoriaItem; } }
    public float Daño { get { return daño; } }
    public float VelocidadAtaque { get { return velocidadAtaque; } }
    public float Critico { get { return critico; } }
    public int Precio { get { return precio; } }


}