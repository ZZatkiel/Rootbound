using UnityEngine;

public class RoundManager
{
    private float ronda;

    public RoundManager(float ronda)
    {
        ronda = Ronda;

    }

    private float Ronda
    {
        get { return ronda; }
        set { ronda = value; }
    }

    public float obtenerRonda()
    {
        return ronda;
    }

    public void avanzarRonda()
    {
        ronda++;

    }

    public float multiplicadorDeDifcultad()
    {
        return 1f + (ronda - 1) * 0.35f;
    }

}
