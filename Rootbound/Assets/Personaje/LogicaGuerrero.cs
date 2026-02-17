using UnityEngine;

public class LogicaGuerrero : MonoBehaviour
{
    [SerializeField] private InformacionPersonaje datos; // tu SO base
    [SerializeField] private Transform manoDerecha; // asignar en inspector

    public InformacionPersonaje Datos {  get { return datos; } }

    private int ultimoHotbarIndexSeleccionado = -1; // para toggle


    // instancia visual del arma equipada
    private GameObject instanciaModelo;
    private Arma armaEquipada;

    // stats base (cargados desde datos)
    private float vidaBase;
    private float dañoBase;
    private float velocidadAtaqueBase;
    private float criticoBase;

    // stats actuales (usadas por la lógica de combate)
    private float dañoActual;
    private float velocidadAtaqueActual;
    private float criticoActual;

    [SerializeField] private Animator animator;

    // control de estado / cooldown
    private float siguienteAtaqueTiempo = 0f;
    private bool estaAtacando = false;

    private GameObject ArmaObjeto;
    private Collider colliderArma;

    public GameObject manejadorDerrota;


    // -------------------------------------------------


    private void Awake()
    {
        if (datos != null)
        {
            vidaBase = datos.Vida;
            dañoBase = datos.DañoDeAtaque;
            velocidadAtaqueBase = datos.VelocidadAtaque;
            criticoBase = datos.AtaqueCritico;
        }
        // Inicializar actuales a base
        RecalcularStatsPorBase();
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
    private void RecalcularStatsPorBase()
    {
        dañoActual = dañoBase;
        velocidadAtaqueActual = velocidadAtaqueBase;
        criticoActual = criticoBase;
    }

    // Equipa una Arma (clase Arma que ya guardas en inventario)
    public void EquiparArma(Arma arma)
    {
        if (arma == null) return;

        // Si es la misma arma que ya tengo, ignoro (o podrías toggleear)
        if (armaEquipada == arma)
        {
            Debug.Log("Ya está equipada esa misma arma.");
            return;
        }

        // Desequipar si hay una equipada
        DesequiparArma();

        // Guardar referencia a la nueva arma
        armaEquipada = arma;

        // Instanciar modelo visual si existe
        if (arma.Modelo != null && manoDerecha != null)
        {
            instanciaModelo = Instantiate(arma.Modelo, manoDerecha);
            instanciaModelo.transform.localPosition = Vector3.zero;
            instanciaModelo.transform.localRotation = Quaternion.Euler(0,180,0);
        }

        // Aplicar stats: ejemplo suma directa
        dañoActual = dañoBase + arma.Daño;
        velocidadAtaqueActual = velocidadAtaqueBase + arma.VelocidadDeAtaque;
        criticoActual = criticoBase + arma.AtaqueCritico;

        Debug.Log($"Equipado {arma.Nombre}. Daño: {dañoActual}, Velocidad: {velocidadAtaqueActual}, Crit: {criticoActual}");

        ArmaObjeto = GameObject.FindGameObjectWithTag("Arma");
        colliderArma = ArmaObjeto.GetComponent<Collider>();
        colliderArma.enabled = false;

    }

    // Desequipa arma actual (revierte stats y destruye visual)
    public void DesequiparArma()
    {
        if (armaEquipada == null && instanciaModelo == null)
            return; // nada que hacer

        // destruir modelo visual si existe
        if (instanciaModelo != null)
        {
            Destroy(instanciaModelo);
            instanciaModelo = null;
        }

        // quitar referencia
        armaEquipada = null;

        // volver a stats base
        RecalcularStatsPorBase();

        Debug.Log("Arma desequipada. Stats revertidos a base.");
    }

    // Método helper para obtener el daño actual (usar en cálculo de daño)
    public float ObtenerDañoActual()
    {
        return dañoActual;
    }

    public float ObtenerVidaActual()
    {
        return vidaBase;
    }

    // Exponer si hay arma equipada (útil para UI)
    public bool HayArmaEquipada() => armaEquipada != null;

    private void ProcesarHotbar(int index)
    {
        // Si el jugador ya tenía seleccionado ese slot -> toggle (desequipar)
        if (ultimoHotbarIndexSeleccionado == index)
        {
            // desequipar
            var player = FindFirstObjectByType<LogicaGuerrero>();
            if (player != null) player.DesequiparArma();
            ultimoHotbarIndexSeleccionado = -1;
            return;
        }

        // sino, equipar lo del hotbar (llama a Inventario)
        Inventario.Instancia.EquiparDesdeHotbar(index);
        ultimoHotbarIndexSeleccionado = index;
    }

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

    public void recibirDaño(float cantidad)
    {
        vidaBase -= cantidad;

        if (vidaBase <= 0)
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
