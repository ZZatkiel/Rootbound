using UnityEngine;


public enum RarezaArmas
{
    Legendario,
    Epico,
    Raro,
    Comun
}
public class Arma : Item
{
    private float daño;
    private float velocidadDeAtaque;
    private float ataqueCritico;
    private RarezaArmas rareza;

    public float Daño { get => daño; set => daño = value; }
    public float VelocidadDeAtaque { get => velocidadDeAtaque; set => velocidadDeAtaque = value; }
    public float AtaqueCritico { get => ataqueCritico; set => ataqueCritico = value; }
    public RarezaArmas Rareza { get => rareza; set => rareza = value; }

    public Arma(string nombre, string descripcion, GameObject modelo, Sprite imagenInventario, float daño, float velocidadDeAtaque, float ataqueCritico, RarezaArmas rareza) : base(nombre, descripcion, modelo, imagenInventario)
    {
        Daño = daño;
        VelocidadDeAtaque = velocidadDeAtaque;
        AtaqueCritico = ataqueCritico;
        Rareza = rareza;
    }
}

