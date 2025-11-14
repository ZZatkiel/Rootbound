using UnityEngine;

public enum tipoPersonaje
{
    Guerrero,
    Arquero
}

[CreateAssetMenu(menuName = "EstadisticaPersonaje")]
public class CharacterStats : ScriptableObject
{
    public tipoPersonaje TipoPersonaje;
    public float vidaMaxima;
    public float velocidadMovimiento;
    public float multiplicadorCorrer;
    public float dañoDeAtaque;
    public float rangoDeAtaque;
    public float ataqueCritico;
    public float velocidadAtaque;
    public float recargaDeAtaque;

}
//Infomracion del personaje
// CREAR UN SCRIPTABLEOBJECT PARA LOS DATOS DEL PERSONAJE, EL ARQUERO TENDRA SUS RESPECTIVOS DATOS Y EL GUERRO LOS SUYOS
// AMBOS TIENE LA MISMA LOGICA DE MOVIMIENTO Y CAMARA
// EL COMBATE SERA DIFERENTE EN CADA CASO

// RESPECTO AL ARMA, SE BUSCARA UNA ANIMACION EL CUAL EL ENEMIGO ATAQUE CON UN ARMA Y SE LE REEMPLAZARA EL ITEM POR EL SELECCIONADO EN EL INVETARIO

