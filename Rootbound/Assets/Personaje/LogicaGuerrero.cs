using UnityEngine;

/*
 * ESTE SCRIPT MANEJA VARIAS PARTES DE LA LOGICA DEL PERSONAJE
 *  - EL MANEJO DE LAS STATS DEL PERSONAJE
 *  - EL MANEJO DE LA MUERTE/DAÑO DEL PERSONAJE
 *  - UNA PARTE DEL MANEJO DE LA HOTBAR DEPENDIENTE AL SCRIPT INVENTARIO
 * 
*/ 


public class LogicaGuerrero : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private InformacionPersonaje datos;
    [SerializeField] private Transform manoDerecha;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject manejadorDerrota;


    private Arma armaEquipada;
    private GameObject instanciaModelo;
    private Collider colliderArma;


    private float vidaMaxima;
    private float vidaActual;

    private float dañoBase;
    private float velocidadAtaqueBase;
    private float criticoBase;

    private float dañoActual;
    private float velocidadAtaqueActual;
    private float criticoActual;



    public InformacionPersonaje Datos {  get { return datos; } }
    private int ultimoHotbarIndexSeleccionado = -1;


    private float siguienteAtaqueTiempo = 0f;
    private bool estaAtacando = false;

    // -------------------------------------------------


    private void Awake()
    {
        vidaMaxima = datos.Vida;
        vidaActual = vidaMaxima;

        dañoBase = datos.DañoDeAtaque;
        velocidadAtaqueBase = datos.VelocidadAtaque;
        criticoBase = datos.AtaqueCritico;

        RecalcularStats();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) ProcesarHotbar(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) ProcesarHotbar(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) ProcesarHotbar(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) ProcesarHotbar(3);

        if (Input.GetMouseButtonDown(0) && armaEquipada != null)
        {
            IntentarAtacar();
        }
    }
    private void RecalcularStats()
    {
        dañoActual = dañoBase;
        velocidadAtaqueActual = velocidadAtaqueBase;
        criticoActual = criticoBase;

        if (armaEquipada != null)
        {
            dañoActual += armaEquipada.Daño;
            velocidadAtaqueActual += armaEquipada.VelocidadDeAtaque;
            criticoActual += armaEquipada.AtaqueCritico;
        }
    }




    // METODOS DEL ACCESO AL ARMA DEL HOTBAR


    public void EquiparArma(Arma arma)
    {
        if (arma == null) return;

        DesequiparArma();

        armaEquipada = arma;
        instanciaModelo = Instantiate(arma.Modelo, manoDerecha);
        instanciaModelo.transform.localPosition = Vector3.zero;
        instanciaModelo.transform.localRotation = Quaternion.Euler(0, 180, 0);

        colliderArma = instanciaModelo.GetComponent<Collider>();
        if (colliderArma != null)
            colliderArma.enabled = false;

        RecalcularStats();


    }

    public void DesequiparArma()
    {
        if (armaEquipada == null && instanciaModelo == null) return;

        if (instanciaModelo != null)
        {
            Destroy(instanciaModelo);
            instanciaModelo = null;
        }

        armaEquipada = null;

        RecalcularStats();

    }

    private void ProcesarHotbar(int index)
    {
        if (ultimoHotbarIndexSeleccionado == index)
        {
            var player = FindFirstObjectByType<LogicaGuerrero>();
            if (player != null) player.DesequiparArma();
            ultimoHotbarIndexSeleccionado = -1;
            return;
        }

        Inventario.Instancia.EquiparDesdeHotbar(index);
        ultimoHotbarIndexSeleccionado = index;
    }







    // METODOS ACCESORES DE DATOS DE LA LOGICA DEL GUERRERO


    public float ObtenerDañoActual()
    {
        return dañoActual;
    }

    public float ObtenerVidaActual()
    {
        return vidaActual;
    }

    public float ObtenerCriticoActual()
    {
        return criticoActual;
    }

    public bool HayArmaEquipada() => armaEquipada != null;






    // METODOS DEL ATAQUE DEL GUERRERO


    private void IntentarAtacar()
    {

        Debug.Log("PASO POR ACA");
        //Formula para el cooldown de los ataques
        float ataquesPorSegundo = Mathf.Max(0.0001f, velocidadAtaqueActual);
        float cooldown = 1f / ataquesPorSegundo;

        Debug.Log("PASO POR ACA 2");

        if (Time.time < siguienteAtaqueTiempo) return; // aún en cooldown
        Debug.Log("PASO POR ACA 2.5");
        Debug.Log("estaAtacando vale: " + estaAtacando);
        if (estaAtacando) return; // ya en animación de ataque

        Debug.Log("PASO POR ACA 3");

        estaAtacando = true;
        siguienteAtaqueTiempo = Time.time + cooldown; // para no permitir otro input hasta cooldown (se puede ajustar)
        if (animator != null)
        {
            Debug.Log("PASO POR ACA 4");
            animator.SetTrigger("attack"); // asegurate de tener el trigger "Attack" en el Animator
        }
    }

    public void OnAttackHit()
    {
        if (colliderArma != null) colliderArma.enabled = true;
    }

    public void AtaqueTerminado()
    {
        estaAtacando = false;
        if (colliderArma != null)  colliderArma.enabled = false;
        if (animator != null) animator.SetTrigger("attackEnd");
    }



    // METODOS DE MUERTE/RECIBIR DAÑO DEL GUERRERO



    public void recibirDaño(float cantidad)
    {
        vidaActual -= cantidad;

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        manejadorDerrota.GetComponent<PartidaPerdidaScript>().MuerteDesactivadorScripts();

        manejadorDerrota.SetActive(true);
    }





}
