using UnityEngine;

public enum tiposDePersonaje
{
    Guerrero,
    Arquero
}

[CreateAssetMenu(menuName = "EstadisticaPersonaje")]
public class InformacionPersonaje : ScriptableObject
{
    [SerializeField] private tiposDePersonaje tipoDePersonaje;
    [SerializeField] private float vida;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float multiplicadorCorrer;
    [SerializeField] private float dañoDeAtaque;
    [SerializeField] private float rangoDeAtaque;
    [SerializeField] private float ataqueCritico;
    [SerializeField] private float velocidadAtaque;
    [SerializeField] private float recargaDeAtaque;

    public tiposDePersonaje TipoDePersonaje { get { return tipoDePersonaje;}}
    public float Vida {  get { return vida; }}
    public float VelocidadMovimiento { get { return velocidadMovimiento;}}
    public float MultiplicadorCorrer { get { return multiplicadorCorrer; } }
    public float DañoDeAtaque { get { return dañoDeAtaque; } }
    public float RangoDeAtaque { get { return rangoDeAtaque; } }
    public float AtaqueCritico { get { return ataqueCritico; } }
    public float VelocidadAtaque { get { return velocidadAtaque; } }
    public float RecargaDeAtaque { get { return recargaDeAtaque; } }

}