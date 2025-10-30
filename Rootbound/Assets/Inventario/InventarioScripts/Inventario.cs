using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventario : MonoBehaviour
{
    public static Inventario Instancia { get; private set; } // Singleton

    public GameObject tiendaObjetos;
    public GameObject MenuPausa;
    public MonoBehaviour[] scriptsADesactivar;

    // Array Interno donde estan todos los items
    Dictionary<string, Item[]> ItemsTotales = new Dictionary<string, Item[]>();

    //Elementos del array Externo
    int allSlotsArmas;
    int allSlotsPociones;
    int allSlotsHotbar;
    public GameObject pocionHandler;
    public GameObject armasHandler;
    public GameObject invPocionesGENERAL;
    public GameObject invArmasGENERAL;

    public GameObject hotbarHandler;


    // Array Externo o visual donde se ve todo
    GameObject[] PocionesInventarioUI;
    GameObject[] ArmasInventarioUI;
    GameObject[] HotbarInventarioUI;


    // Parte UI
    bool inventoryEnabled;
    public GameObject inventario;


    private void Awake()
    {
         if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }   

         Instancia = this;
    }


    private void Start()
    {
        // Array EXTERNO -UI-
        allSlotsArmas = armasHandler.transform.childCount;
        allSlotsPociones = pocionHandler.transform.childCount;

        allSlotsHotbar = hotbarHandler.transform.childCount;

        PocionesInventarioUI = new GameObject[allSlotsPociones];
        ArmasInventarioUI = new GameObject[allSlotsArmas];
        HotbarInventarioUI = new GameObject[allSlotsHotbar];


        for (int i = 0; i < allSlotsArmas; i++)
        {
            ArmasInventarioUI[i] = armasHandler.transform.GetChild(i).gameObject;
            ArmasInventarioUI[i].GetComponent<Slot>().InicializarSlot();
            ArmasInventarioUI[i].GetComponent<Slot>().EstablecerIndiceYCategoria(i, CategoriaDelSlotEnum.ArmaSlot);
            if (ArmasInventarioUI[i].GetComponent<DragHandler>() == null)
                ArmasInventarioUI[i].AddComponent<DragHandler>();
        }


        for (int i = 0; i < allSlotsPociones; i++)
        {
            PocionesInventarioUI[i] = pocionHandler.transform.GetChild(i).gameObject;
            PocionesInventarioUI[i].GetComponent<Slot>().InicializarSlot();
            PocionesInventarioUI[i].GetComponent<Slot>().EstablecerIndiceYCategoria(i, CategoriaDelSlotEnum.PocionSlot);
            if (PocionesInventarioUI[i].GetComponent<DragHandler>() == null)
                PocionesInventarioUI[i].AddComponent<DragHandler>();

        }

        for (int i = 0; i < allSlotsHotbar; i++)
        {
            HotbarInventarioUI[i] = hotbarHandler.transform.GetChild(i).gameObject;
            HotbarInventarioUI[i].GetComponent<Slot>().InicializarSlot();
            HotbarInventarioUI[i].GetComponent<Slot>().EstablecerIndiceYCategoria(i, CategoriaDelSlotEnum.HotbarSlot);
            if (HotbarInventarioUI[i].GetComponent<DragHandler>() == null)
                HotbarInventarioUI[i].AddComponent<DragHandler>();

        }


        // ARRAY INTERNO -Gestion de Datos-

        Item[] pociones = new Item[allSlotsPociones];
        Item[] armas = new Item[allSlotsArmas];
        Item[] hotbar = new Item[allSlotsHotbar];

        ItemsTotales.Add("Pociones", pociones);
        ItemsTotales.Add("Armas", armas);
        ItemsTotales.Add("Hotbar", hotbar);

        UpdateHotbarInventarioUI();
        UpdateArmaInventarioUI();
        UpdatePocionInventarioUI();

    }

    private void Update()
    {
        AbrirInventario();
    }


    public bool AgregarPocion(Pocion pocionParaAgregar)
    {
        Item[] pocionesTotales = ItemsTotales["Pociones"];

        for (int i = 0; i < pocionesTotales.Length; i++)
        {
            if (pocionesTotales[i] == null)
            {
                pocionesTotales[i] = pocionParaAgregar;
                UpdatePocionInventarioUI();
                return true;
            }
            else
            {
                if (pocionesTotales[i].Nombre == pocionParaAgregar.Nombre)
                {
                    Pocion pocionDeLaLista = (Pocion)pocionesTotales[i];
                    pocionDeLaLista.Cantidad += pocionParaAgregar.Cantidad;
                    UpdatePocionInventarioUI();
                    return true;
                }

            }

        }
        return false;
    }

    public bool EliminarPocion(Pocion pocionParaEliminar)
    {
        Item[] pocionesTotales = ItemsTotales["Pociones"];

        for (int i = 0; i < pocionesTotales.Length; i++)
        {
            if (pocionesTotales[i] != null)
            {
                if (pocionesTotales[i].Nombre == pocionParaEliminar.Nombre)
                {
                    Pocion pocionDeLaLista = (Pocion)pocionesTotales[i];
                    pocionDeLaLista.Cantidad -= pocionParaEliminar.Cantidad;
                    UpdatePocionInventarioUI();
                    if (pocionDeLaLista.Cantidad == 0)
                    {
                        pocionesTotales[i] = null;
                        UpdatePocionInventarioUI();
                        return true;
                    }
                }
            }
        }
        return false;
    }


    // Funciones para Agregar y Eliminar Armas 

    public bool AgregarArma(Arma armaParaAgregar)
    {
        Item[] armasTotales = ItemsTotales["Armas"];

        for (int i = 0; i < armasTotales.Length; i++)
        {
            if (armasTotales[i] == null)
            {
                armasTotales[i] = armaParaAgregar;
                UpdateArmaInventarioUI();
                return true;
            }
        }
        Debug.Log("No hay espacios disponible para el arma");
        return false;
    }

    public bool EliminarArma(Arma armaParaEliminar)
    {
        Item[] armasTotales = ItemsTotales["Armas"];

        for (int i = armasTotales.Length - 1; i >= 0; i--)
        {
            if (armasTotales[i] != null)
            {
                if (armasTotales[i].Nombre == armaParaEliminar.Nombre)
                {
                    Debug.Log("Arma eliminada con exito");
                    armasTotales[i] = null;
                    UpdateArmaInventarioUI();
                    return true;
                }
            }
        }
        Debug.Log("No hay para eliminar");
        return false;
    }


    // MOVER / INTERCAMBIO ITEMS (lo usa el dragHandler)

    public bool Swap(CategoriaDelSlotEnum primeraCategoria, int primerIndice, CategoriaDelSlotEnum segundaCategoria, int segundoIndice)
    {
        Debug.Log("PASO POR ACA 2");

        // Mapear slot category -> clave del diccionario
        string ObtenerLaCategoriaDelSlot(CategoriaDelSlotEnum cat)
        {
            return cat switch
            {
                CategoriaDelSlotEnum.ArmaSlot => "Armas",
                CategoriaDelSlotEnum.PocionSlot => "Pociones",
                CategoriaDelSlotEnum.HotbarSlot => "Hotbar",
                _ => null
            };
        }

        // Mapear item.CategoriaItem -> clave del diccionario
        string ObtenerLaCategoriaDelItem(CategoriaItemEnum cat)
        {
            return cat switch
            {
                CategoriaItemEnum.Arma => "Armas",
                CategoriaItemEnum.Pocion => "Pociones",
                _ => null
            };
        }

        // Helper para actualizar UIs implicadas
        void UpdateUIFor(string key)
        {
            if (key == "Armas") UpdateArmaInventarioUI();
            else if (key == "Pociones") UpdatePocionInventarioUI();
            else if (key == "Hotbar") UpdateHotbarInventarioUI();
        }

        // Obtengo el dato de que tipo es la CATEGORIA DEL PRIMER SLOT SELECCIONADO Y EL SEGUNDO SELECCIONADO
        string llavePrimaria = ObtenerLaCategoriaDelSlot(primeraCategoria);
        string llaveSecundaria = ObtenerLaCategoriaDelSlot(segundaCategoria);

        // SI SELECCIONO UN LUGAR DONDE NO HAY SLOT O SELECCIONO UN SLOT EL CUAL NO ESTA DENTRO DEL ENUM, ENTONCES NO SE PODRA HACER EL INTERCAMBIO Y ME RETORNARA Y NO SE JECETURA LO OTRO
        if (llavePrimaria == null || llaveSecundaria == null)
        {
            Debug.Log("Categoria de slot no mapeada en ItemsTotales.");
            return false;
        }

        // OBTENGO LA LISTA INTERNA DEL ITEM SELECCIONADO, EN EL CUAL DESPUES USO PARA OBTENER EL ITEM
        Item[] ArrayPrimaria = ItemsTotales[llavePrimaria];
        Item[] ArraySecundaria = ItemsTotales[llaveSecundaria];

        // VERIFICO SI LOS INDICES SON VALIDOS, Y EN EL CASO QUE NO LO SEA, NO SE REALIZARA EL SWAP
        if (primerIndice < 0 || primerIndice >= ArrayPrimaria.Length || segundoIndice < 0 || segundoIndice >= ArraySecundaria.Length)
        {
            Debug.Log("Indices invalidos para Swap.");
            return false;
        }

        // OBTENGO LOS ITEMS SELECCIONADOS EN EL DRAG AND DROP
        Item itemPrimario = ArrayPrimaria[primerIndice];
        Item itemSecundario = ArraySecundaria[segundoIndice];

        Debug.Log($"ESTE ES EL ITEM SECUNDARIO: {itemSecundario}");

        // CASO 1: SI LAS LISTA DE LA UI SON LAS MISMAS, ENTONCES SE REALIZARA EL SWAP OSEA
        // - ARMA <-> ARMA
        // - POCION <-> POCION
        // - HOTBAR <-> HOTBAR

        if (llavePrimaria == llaveSecundaria)
        {
            // HAGO EL SWAP ENTRE EL PRIMER SLOT QUE TIENE EL ITEM Y EL SEGUNDO SLOT QUE NO TIENE ITEM
            if (itemPrimario != null && itemSecundario == null)
            {
                Debug.Log("HAGO EL SWAP ENTRE EL PRIMER SLOT QUE TIENE EL ITEM Y EL SEGUNDO SLOT QUE NO TIENE ITEM");
                ArraySecundaria[segundoIndice] = itemPrimario;
                ArrayPrimaria[primerIndice] = null;
                UpdateUIFor(llavePrimaria);
                return true;
            }

            // HAGO EL SWAP ENTRE EL PRIMER SLOT QUE TIENE EL ITEM Y EL SEGUNDO SLOT QUE TIENE EL ITEM
            if (itemPrimario != null && itemSecundario != null)
            {
                Debug.Log("HAGO EL SWAP ENTRE EL PRIMER SLOT QUE TIENE EL ITEM Y EL SEGUNDO SLOT QUE TIENE EL ITEM");
                Item tmp = ArraySecundaria[segundoIndice];
                ArraySecundaria[segundoIndice] = ArrayPrimaria[primerIndice];
                ArrayPrimaria[primerIndice] = tmp;
                UpdateUIFor(llavePrimaria);
                return true;
            }

            Debug.Log("No hay nada que mover en el mismo array.");
            return false;
        }
       
        // Distinto array: regla general
        // - Hotbar puede recibir tanto Arma como Pocion, pero el item debe ser compatible con el slot destino.
        // - Dicho de esta forma los items(ARMA O POCION) que tenga el Hotbar deberan corresponder a su contenedor (ARMAS O POCIONES)

        bool origenEsHotbar = llavePrimaria == "Hotbar";
        bool destinoEsHotbar = llaveSecundaria == "Hotbar";


        // Si no hay item en origen, no hay nada que mover
        if (itemPrimario == null)
        {
            Debug.Log("No hay item en el slot de origen para mover.");
            return false;
        }

        // Determinar clave de la categoria del item origen (p. ej. "Armas" o "Pociones")
        string llaveItemPrim = ObtenerLaCategoriaDelItem(itemPrimario.CategoriaItem);

        if (llaveItemPrim == null)
        {
            Debug.Log("Item de origen tiene CategoriaItem inválida.");
            return false;
        }

        // Validar compatibilidad item -> categoria destino:
        // - Si destino es Hotbar: permitido (Hotbar acepta todo).
        // - Si destino es Armas/Pociones: el item debe coincidir con esa categoria.
        if (!destinoEsHotbar && llaveItemPrim != llaveSecundaria)
        {
            Debug.Log("El item origen no es compatible con la categoria destino.");
            return false;
        }
        if (destinoEsHotbar && llaveItemPrim == llavePrimaria)
        {
            Item tmp = ArraySecundaria[segundoIndice];
            ArraySecundaria[segundoIndice] = ArrayPrimaria[primerIndice];
            ArrayPrimaria[primerIndice] = tmp;
            UpdateUIFor(llavePrimaria);
            UpdateUIFor(llaveSecundaria);
            return true;
        }
        if (origenEsHotbar && llaveItemPrim == llaveSecundaria)
        {
            Debug.Log("SE PUEDE METER ACA");
        }


        // Caso MOVE (destino vacío)
        if (itemSecundario == null)
        {
            ArraySecundaria[segundoIndice] = itemPrimario;
            ArrayPrimaria[primerIndice] = null;
            UpdateUIFor(llavePrimaria);
            UpdateUIFor(llaveSecundaria);
            return true;
        }

        // Caso SWAP (ambos con item): validar compatibilidad recíproca
        // Obtener la clave de categoria del item secundario (si es null, no es válido)
        string llaveItemSec = ObtenerLaCategoriaDelItem(itemSecundario.CategoriaItem);
        if (llaveItemSec == null)
        {
            Debug.Log("Item destino tiene CategoriaItem inválida.");
            return false;
        }

        // El item secundario debe poder ir al array origen:
        // - Si origen es Hotbar: siempre permitido
        // - Si origen es Armas/Pociones: llaveItemSec debe coincidir con llavePrimaria
        if (!origenEsHotbar && llaveItemSec != llavePrimaria)
        {
            Debug.Log("El item destino no es compatible con la categoria origen.");
            return false;
        }

        // Si llegamos hasta acá, ambos items son compatibles -> swap
        Item tmpSwap = ArraySecundaria[segundoIndice];
        ArraySecundaria[segundoIndice] = ArrayPrimaria[primerIndice];
        ArrayPrimaria[primerIndice] = tmpSwap;

        UpdateUIFor(llavePrimaria);
        UpdateUIFor(llaveSecundaria);
        return true;
    }







    // Actualizar Interfaz Externa

    public void UpdateArmaInventarioUI()
    {
        Item[] armasTotales = ItemsTotales["Armas"];

        for (int i = 0; i < ArmasInventarioUI.Length; i++)
        {
            ArmasInventarioUI[i].GetComponent<Slot>().SetItem(armasTotales[i]);
        }

    }


    public void UpdatePocionInventarioUI()
    {
        Item[] pocionesTotales = ItemsTotales["Pociones"];

        for (int i = 0; i < PocionesInventarioUI.Length; i++)
        {
            PocionesInventarioUI[i].GetComponent<Slot>().SetItem(pocionesTotales[i]);
        }
    }


    public void UpdateHotbarInventarioUI()
    {
        Item[] hotbarTotales = ItemsTotales["Hotbar"];

        for (int i = 0; i < HotbarInventarioUI.Length; i++)
        {
            HotbarInventarioUI[i].GetComponent<Slot>().SetItem(hotbarTotales[i]);
        }
    }

    // Metodos para Debuggear

    public void RecorrerInventarioInternoArma()
    {
        Item[] armasTotales = ItemsTotales["Armas"];

        for (int i = 0; i < armasTotales.Length; i++)
        {
            if (armasTotales[i] != null)
            {
                Arma arma = (Arma)armasTotales[i];
                Debug.Log($"{arma.Nombre} {arma.Descripcion} {arma.ImagenInventario}, {arma.Modelo} {arma.Rareza} {arma.AtaqueCritico} {arma.Daño} {arma.VelocidadDeAtaque}");
            }
            else
            {
                Debug.Log(armasTotales[i]);
            }

        }

    }

    public void RecorrerInventarioInternoPocion()
    {
        Item[] PocionesTotales = ItemsTotales["Pociones"];

        for (int i = 0; i < PocionesTotales.Length; i++)
        {
            if (PocionesTotales[i] != null)
            {
                Pocion pocion = (Pocion)PocionesTotales[i];
                Debug.Log($"{pocion.Nombre} {pocion.Descripcion} {pocion.ImagenInventario}, {pocion.Modelo}, {pocion.Duracion} {pocion.Cantidad}");
            }
            else
            {
                Debug.Log(PocionesTotales[i]);
            }

        }

    }



    public void AbrirInventario()
    {
        if (Input.GetKeyDown(KeyCode.E) && !tiendaObjetos.activeSelf && !MenuPausa.activeSelf)
        {
            inventoryEnabled = !inventoryEnabled;
            inventario.SetActive(inventoryEnabled);

            // Activar/desactivar scripts de movimiento/cámara
            foreach (var script in scriptsADesactivar)
            {
                script.enabled = !inventoryEnabled;
            }


            // Mostrar u ocultar cursor
            Cursor.visible = inventoryEnabled;
            Cursor.lockState = inventoryEnabled ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    public void MostrarArmas()
    {
        invArmasGENERAL.SetActive(true);
        invPocionesGENERAL.SetActive(false);
        UpdateArmaInventarioUI();
    }

    public void MostrarPociones()
    {
        invPocionesGENERAL.SetActive(true);
        invArmasGENERAL.SetActive(false);
        UpdatePocionInventarioUI();
    }

}
