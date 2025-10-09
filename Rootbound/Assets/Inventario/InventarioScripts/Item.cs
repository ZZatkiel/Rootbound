using UnityEngine;

public abstract class Item
{
    private string nombre;
    private string descripcion;
    private GameObject modelo;
    private Sprite imagenInventario;

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

    protected Item(string nombre, string descripcion, GameObject modelo, Sprite imagenInventario)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        Modelo = modelo;
        ImagenInventario = imagenInventario;
    }

}
