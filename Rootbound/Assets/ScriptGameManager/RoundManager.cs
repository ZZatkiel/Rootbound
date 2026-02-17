// Es el manejador de las rondas de la partida
// Es parte del GameManager

public class RoundManager
{
    private float ronda;
    private float numeroEnemigo;

    public RoundManager(float ronda)
    {
        this.ronda = ronda;
        numeroEnemigo = NumeroEnemigo;
    }

    private float NumeroEnemigo
    {
        get { return numeroEnemigo; }
        set { numeroEnemigo = value; }
    }


    public void reiniciarRonda()
    {
        ronda = 0f; 
    }

    public float obtenerRonda()
    {
        return ronda;
    }

    public void avanzarRonda()
    {
        ronda++;
        numeroEnemigo = numeroEnemigo + 8f;

    }

    public float multiplicadorDeDifcultad()
    {
        return 1f + (ronda - 1) * 0.35f;
    }
}
    


