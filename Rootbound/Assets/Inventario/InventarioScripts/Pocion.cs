using UnityEngine;

public class Pocion : Item
{
    private int duracion;
    private int cantidad;

    public int Duracion
    {
        get => duracion;
        set => duracion = value;
    }

    public int Cantidad
    {
        get => cantidad;
        set => cantidad = value;
    }

    public Pocion(string nombre, string descripcion, GameObject modelo, Sprite imagenInventario, int duracion, int cantidad) : base(nombre, descripcion, modelo, imagenInventario)
    {
        Duracion = duracion;
        Cantidad = cantidad;
    }
}
