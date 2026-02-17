
// Es el manejador del score de la partida
// Es parte del GameManager

public class ScoreManager
{
    private int puntos;


    public ScoreManager(int puntos)
    {
        this.puntos = puntos;
    }

    public void reiniciarPuntos()
    {
        puntos = 0;
    }

    public int obtenerPuntos()
    {
        return puntos;
    }

    public void ResetearPuntos(int x)
    {
        puntos = x;
    }

    public void modificarPuntos(int x)
    {
        puntos += x;
        if (puntos < 0) puntos = 0;
    }


}
