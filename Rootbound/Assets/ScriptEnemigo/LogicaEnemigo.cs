using System;
using System.Collections;
using UnityEngine;


public class LogicaEnemigo : MonoBehaviour
{
    [SerializeField] private Renderer modelo;   // MeshRenderer 
    [SerializeField] private Color colorDaño = Color.red;
    [SerializeField] private float duracion = 0.2f;

    private Color colorOriginal;


    // INFORMACION DEL PERSONAJE
    private float vida;
    private float daño;
    private float velocidadMovimiento;
    private float velocidadAtaque;

    //Informacion del GameObject
    Rigidbody rb;

    // BUSCAR OBJETIVO 
    private string tagJugador = "Player";
    private string tagArbol = "Arbol";

    GameObject Jugador;
    GameObject Arbol;

    private GameObject objetivoActual;
    private float radioEnemigo = 10f;

    private Animator anim;

    private bool cerca = false;


    public Collider ColliderPuñoEnemigo;
    bool estaAtacando;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Jugador = GameObject.FindWithTag(tagJugador);
        Arbol = GameObject.FindWithTag(tagArbol);
        anim = GetComponent<Animator>();

        if (modelo == null)
            modelo = GetComponentInChildren<Renderer>();

        colorOriginal = modelo.material.color;

        ColliderPuñoEnemigo.enabled = false;
    }

    private void Update()
    {
        BuscarObjetivo();


        anim.SetBool("cerca", cerca);
    }

    public void Inicializador(InformacionEnemigo data, float multiplicadorRonda)
    {
        vida = data.Vida * multiplicadorRonda;
        daño = data.Daño;
        velocidadMovimiento = data.VelocidadMovimiento;
        velocidadAtaque = data.VelocidadAtaque;
    }


    // tengo un rango el cual me persiguen a mi, si me alejo de ese rango van al arbol 
    public void BuscarObjetivo()
    {
        objetivoActual = null;


        if (Jugador == null || Arbol == null) return;

        // Ditancia del jugador 
        float distanciaJugador = Vector3.Distance(transform.position, Jugador.transform.position);


        if (distanciaJugador < radioEnemigo)
        {
            objetivoActual = Jugador;
        }
        else
        {
            objetivoActual = Arbol;
        }

        SeguimientoEnemigo();

    }

    public void SeguimientoEnemigo()
    {
        // Resta de vectores, le digo la direccion a la que va a tener que ir
        Vector3 direccion = (objetivoActual.transform.position - transform.position).normalized;
        direccion.y = 0f;
        float distancia = Vector3.Distance(objetivoActual.transform.position, transform.position);

        // Rotar suavemente hacia el objetivo
        Quaternion lookRotation = Quaternion.LookRotation(direccion);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);

        rb.MovePosition(transform.position + transform.forward * velocidadMovimiento * Time.deltaTime);
        
    }

    public void RecibirDaño(float cantidad)
    {
        vida -= cantidad;
        StartCoroutine(Flash());

        if (vida <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        var reporter = GetComponent<NotificadorDeMuerteEnemigo>();
        if (reporter != null)
            reporter.ReportarMuerte();
        GameManagerSC.Instancia.scoreManager.modificarPuntos(10);
        Destroy(gameObject);
    }



    private IEnumerator Flash()
    {
        modelo.material.color = colorDaño;
        yield return new WaitForSeconds(duracion);
        modelo.material.color = colorOriginal;
    }

    public float ObtenerDañoActual()
    {
        return daño;
    }

    public void OnAttackHit()
    {
        if (estaAtacando == false){
            if (ColliderPuñoEnemigo != null) ColliderPuñoEnemigo.enabled = true;
            estaAtacando = true;
        }

    }


    public void AtaqueTerminado()
    {
        if (ColliderPuñoEnemigo != null) ColliderPuñoEnemigo.enabled = false;
        estaAtacando = false;

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioEnemigo);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HitboxArbol") || (other.CompareTag("HitboxPlayer")))
        {
            cerca = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HitboxArbol") || (other.CompareTag("HitboxPlayer")))
        {
            cerca = false;

        }
    }

}
