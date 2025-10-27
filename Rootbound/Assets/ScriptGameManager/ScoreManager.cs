using UnityEngine;

public class ScoreManager
{
    private int puntos;

    private int Puntos
    {
        get { return puntos; }

        set { puntos = value; }
    }

    public ScoreManager(int puntos)
    {
        Puntos = puntos;
    }

    public int obtenerPuntos()
    {
        return Puntos;
    }

    public void ResetearPuntos(int x)
    {
        Puntos = x;
    }

    public void modificarPuntos(int x)
    {
        puntos += x;
        if (puntos < 0) puntos = 0;
    }


}
