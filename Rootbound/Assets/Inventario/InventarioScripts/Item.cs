using UnityEngine;

public enum CategoriaItemEnum
{
    Arma,
    Pocion,
    Indefinido
}
public abstract class Item
{
    private string nombre;
    private string descripcion;
    private GameObject modelo;
    private Sprite imagenInventario;
    private CategoriaItemEnum categoriaItem;

    public string Nombre
    {
        get => nombre;
        set => nombre = value;
    }

    public string Descripcion
    {
        get => descripcion;
        set => descripcion = value;
    }

    public GameObject Modelo
    {
        get => modelo;
        set => modelo = value;
    }

    public Sprite ImagenInventario
    {
        get => imagenInventario;
        set => imagenInventario = value;
    }

    public CategoriaItemEnum CategoriaItem
    {
        get => categoriaItem;
        set => categoriaItem = value;
    }

    protected Item(string nombre, string descripcion, GameObject modelo, Sprite imagenInventario, CategoriaItemEnum categoriaItem)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        Modelo = modelo;
        ImagenInventario = imagenInventario;
        CategoriaItem = categoriaItem;
    }

}
