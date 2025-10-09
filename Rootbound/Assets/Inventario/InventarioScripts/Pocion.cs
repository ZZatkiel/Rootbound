using Unity.VisualScripting;
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
        get
        {
            return cantidad;
        }
        set
        {
            if (value < 0)
            {
                cantidad = 0;
            }
            else if (value > 50)
            {
                cantidad = 50;    
            }
            else
            {
                cantidad = value;
            }
        }
    }

    public Pocion(string nombre, string descripcion, GameObject modelo, Sprite imagenInventario, CategoriaItemEnum categoriaItem, int duracion, int cantidad) : base(nombre, descripcion, modelo, imagenInventario, categoriaItem)
    {
        Duracion = duracion;
        Cantidad = cantidad;
    }
}
