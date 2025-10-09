using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    // Array Interno donde estan todos los items
    Dictionary<string, Item[]> ItemsTotales = new Dictionary<string, Item[]>();

    //Elementos del array Externo

    int allSlotsArmas;
    int allSlotsPociones;
    public GameObject pocionHandler;
    public GameObject armasHandler;
    public GameObject invPociones;
    public GameObject invArmas;



    // Array Externo o visual donde se ve todo

    GameObject[] PocionesInventarioUI;
    GameObject[] ArmasInventarioUI;



    // Parte UI
    bool inventoryEnabled;
    public GameObject inventario;

    private void Start()
    {
        // Array EXTERNO -UI-
        allSlotsArmas = armasHandler.transform.childCount;
        allSlotsPociones = pocionHandler.transform.childCount;

        PocionesInventarioUI = new GameObject[allSlotsPociones];
        ArmasInventarioUI = new GameObject[allSlotsArmas];


        for (int i = 0; i < allSlotsArmas; i++)
        {
            ArmasInventarioUI[i] = armasHandler.transform.GetChild(i).gameObject;
            ArmasInventarioUI[i].GetComponent<Slot>().InicializarSlot();
        }


        for (int i = 0; i < allSlotsPociones; i++)
        {
            PocionesInventarioUI[i] = pocionHandler.transform.GetChild(i).gameObject;
            PocionesInventarioUI[i].GetComponent<Slot>().InicializarSlot();


        }


        // ARRAY INTERNO -Gestion de Datos-

        Item[] pociones = new Item[allSlotsPociones];
        Item[] armas = new Item[allSlotsArmas];

        ItemsTotales.Add("Pociones", pociones);
        ItemsTotales.Add("Armas", armas);

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryEnabled = !inventoryEnabled;
        }

        if (inventoryEnabled)
        {
            inventario.SetActive(true);
        }
        else
        {
            inventario.SetActive(false);
        }
    }

    public void MostrarArmas()
    {
        invArmas.SetActive(true);
        invPociones.SetActive(false);
        UpdateArmaInventarioUI();
    }

    public void MostrarPociones()
    {
        invPociones.SetActive(true);
        invArmas.SetActive(false);
        UpdatePocionInventarioUI();
    }

}
