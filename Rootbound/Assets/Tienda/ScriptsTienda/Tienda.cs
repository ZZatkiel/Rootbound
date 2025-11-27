using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tienda : MonoBehaviour
{
    public GameObject panelTienda;
    public GameObject objetoComprable1;
    public GameObject objetoComprable2;
    public GameObject objetoComprable3;
    public Text TextoDeRecarga;
    public int valorDeRecarga = 10;
    public bool tiendaEnabled;

    GameObject[] listObjetosComprables;

    [Header("Armas (ScriptableObjects)")]
    public List<InformacionArma> armasDisponibles = new List<InformacionArma>();

    [Header("Pociones (ScriptableObjects)")]
    public List<InformacionPocion> pocionesDisponibles = new List<InformacionPocion>();

    public MonoBehaviour[] scriptsADesactivar;


    public enum ShopItemType { Arma, Pocion }
    [Serializable]
    public class ShopEntry
    {
        public ShopItemType tipo;
        public InformacionArma armaSO;     // si tipo == Arma
        public InformacionPocion pocionSO; // si tipo == Pocion

        public string Nombre
        {
            get
            {
                if (tipo == ShopItemType.Arma && armaSO != null) return armaSO.nombre;
                if (tipo == ShopItemType.Pocion && pocionSO != null) return pocionSO.nombre;
                return "Desconocido";
            }
        }

        public string Descripcion
        {
            get
            {
                if (tipo == ShopItemType.Arma && armaSO != null) return armaSO.descripcion;
                if (tipo == ShopItemType.Pocion && pocionSO != null) return pocionSO.descripcion;
                return "";
            }
        }

        public int Precio
        {
            get
            {
                if (tipo == ShopItemType.Arma && armaSO != null) return armaSO.precio;
                if (tipo == ShopItemType.Pocion && pocionSO != null) return pocionSO.precio;
                return 0;
            }
        }
    }

    private List<ShopEntry> itemsPool;

    private void Awake()
    {
        BuildItemsPool();
    }

    private void OnEnable()
    {
        TextoDeRecarga.text = valorDeRecarga.ToString();
    }

    private void OnDisable()
    {
        valorDeRecarga = 10;
    }

    private void BuildItemsPool()
    {
        itemsPool = new List<ShopEntry>();

        foreach (var arma in armasDisponibles)
        {
            if (arma == null) continue;
            itemsPool.Add(new ShopEntry { tipo = ShopItemType.Arma, armaSO = arma });
        }

        foreach (var poc in pocionesDisponibles)
        {
            if (poc == null) continue;
            itemsPool.Add(new ShopEntry { tipo = ShopItemType.Pocion, pocionSO = poc });
        }
    }

    public List<ShopEntry> ObtenerTresRandom()
    {
        if (itemsPool == null || itemsPool.Count == 0) BuildItemsPool();

        List<ShopEntry> copia = new List<ShopEntry>(itemsPool);
        List<ShopEntry> seleccion = new List<ShopEntry>();

        for (int i = 0; i < 3 && copia.Count > 0; i++)
        {
            int index = UnityEngine.Random.Range(0, copia.Count);
            seleccion.Add(copia[index]);
            copia.RemoveAt(index);
        }

        return seleccion;
    }

    public void MostrarArmasEnUI()
    {
        panelTienda.SetActive(true);
        tiendaEnabled = true;
        Time.timeScale = 0f;

        // Activar/desactivar scripts de movimiento/cámara
        foreach (var script in scriptsADesactivar)
        {
            script.enabled = false;
        }

        // Mostrar u ocultar cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


        List<ShopEntry> seleccion = ObtenerTresRandom();
        listObjetosComprables = new GameObject[3] { objetoComprable1, objetoComprable2, objetoComprable3 };

        for (int i = 0; i < listObjetosComprables.Length; i++)
            if (listObjetosComprables[i] != null) listObjetosComprables[i].SetActive(false);

        for (int i = 0; i < seleccion.Count; i++)
        {
            ShopEntry entry = seleccion[i];
            GameObject contenedor = listObjetosComprables[i];
            contenedor.SetActive(true);

            contenedor.transform.Find("NombreDelObjeto").GetComponent<Text>().text = entry.Nombre;
            contenedor.transform.Find("DescripcionDelObjeto").GetComponent<Text>().text = entry.Descripcion;
            contenedor.transform.Find("PrecioDelObjeto").GetComponent<Text>().text = entry.Precio.ToString();

            Button botonCompra = contenedor.transform.Find("BotonTienda").GetComponent<Button>();
            botonCompra.onClick.RemoveAllListeners();

            ShopEntry captured = entry;
            botonCompra.onClick.AddListener(() => Comprar(captured, contenedor));
        }
    }

    public void Recargar()
    {
        if (GameManagerSC.Instancia.scoreManager.obtenerPuntos() >= valorDeRecarga)
        {
            MostrarArmasEnUI();
            GameManagerSC.Instancia.scoreManager.modificarPuntos(-valorDeRecarga);
            valorDeRecarga *= 2;
            TextoDeRecarga.text = valorDeRecarga.ToString();
        }
        else
        {
            Debug.Log("No tenes suficientes puntos para recargar.");
        }
    }

    public void Comprar(ShopEntry entry, GameObject contenedorDeDatosComprables)
    {
        int precio = entry.Precio;

        if (GameManagerSC.Instancia.scoreManager.obtenerPuntos() >= precio)
        {
            Debug.Log($"Comprado {entry.Nombre}");
            GameManagerSC.Instancia.scoreManager.modificarPuntos(-precio);
            contenedorDeDatosComprables.SetActive(false);

            if (entry.tipo == ShopItemType.Arma && entry.armaSO != null)
            {
                // Crear Arma (como antes)
                Arma armaNueva = new Arma(
                    entry.armaSO.nombre,
                    entry.armaSO.descripcion,
                    entry.armaSO.prefabModelo,
                    entry.armaSO.imagenInventario,
                    entry.armaSO.categoriaItem,
                    entry.armaSO.daño,
                    entry.armaSO.velocidadAtaque,
                    entry.armaSO.critico,
                    entry.armaSO.rareza
                );
                Inventario.Instancia.AgregarArma(armaNueva);
            }
            else if (entry.tipo == ShopItemType.Pocion && entry.pocionSO != null)
            {
                // Crear Pocion usando los datos del ScriptableObject
                Pocion pocionNueva = new Pocion(
                    entry.pocionSO.nombre,
                    entry.pocionSO.descripcion,
                    entry.pocionSO.prefabModelo,
                    entry.pocionSO.imagenInventario,
                    entry.pocionSO.categoriaItem,
                    entry.pocionSO.duracion,
                    entry.pocionSO.cantidad
                );
                Inventario.Instancia.AgregarPocion(pocionNueva);
            }
            else
            {
                Debug.LogWarning("Entry inválido en Compra.");
            }
        }
        else
        {
            Debug.Log("No tenes suficientes monedas");
        }
    }

    public void CerrarLaTienda()
    {
        tiendaEnabled = false;
        // Activar/desactivar scripts de movimiento/cámara
        foreach (var script in scriptsADesactivar)
        {
            script.enabled = true;
        }

        // Mostrar u ocultar cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;

    }
}
