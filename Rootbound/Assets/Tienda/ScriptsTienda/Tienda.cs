using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tienda : MonoBehaviour
{
    List<Dictionary<string, object>> items;

    public GameObject panelTienda;
    public GameObject objetoComprable1;
    public GameObject objetoComprable2;
    public GameObject objetoComprable3;
    public Text TextoDeRecarga;
    public int valorDeRecarga;

    GameObject[] listObjetosComprables;

    private void OnEnable()
    {
        TextoDeRecarga.text = valorDeRecarga.ToString();
    }


    private void OnDisable()
    {
        valorDeRecarga = 10;

    }

    private void Awake()
    {
        items = new List<Dictionary<string, object>>()
{
    // ESPADA 1 - Común
    new Dictionary<string, object>()
    {
        {"nombre", "Espada de Madera"},
        {"descripcion", "Pequeña espada que sirve para principiantes"},
        {"modelo", null},
        {"imagenInventario", null},
        {"categoriaItem", CategoriaItemEnum.Arma},
        {"daño", 15f},
        {"velocidadAtaque", 5f},
        {"critico", 5f},
        {"rareza", RarezaArmas.Comun},
        {"precio", 20}
    },

    // ESPADA 2 - Común
    new Dictionary<string, object>()
    {
        {"nombre", "Espada de Hierro"},
        {"descripcion", "Una espada simple pero resistente, ideal para aventureros novatos"},
        {"modelo", null},
        {"imagenInventario", null},
        {"categoriaItem", CategoriaItemEnum.Arma},
        {"daño", 25f},
        {"velocidadAtaque", 6f},
        {"critico", 7f},
        {"rareza", RarezaArmas.Comun},
        {"precio", 45}
    },

    // ESPADA 3 - Rara
    new Dictionary<string, object>()
    {
        {"nombre", "Espada del Alba"},
        {"descripcion", "Brilla con la luz del amanecer, otorgando precisión y poder"},
        {"modelo", null},
        {"imagenInventario", null},
        {"categoriaItem", CategoriaItemEnum.Arma},
        {"daño", 40f},
        {"velocidadAtaque", 6f},
        {"critico", 10f},
        {"rareza", RarezaArmas.Raro},
        {"precio", 120}
    },

    // ESPADA 4 - Rara
    new Dictionary<string, object>()
    {
        {"nombre", "Espada Carmesí"},
        {"descripcion", "Forjada con el fuego de un volcán, su filo arde con furia"},
        {"modelo", null},
        {"imagenInventario", null},
        {"categoriaItem", CategoriaItemEnum.Arma},
        {"daño", 50f},
        {"velocidadAtaque", 5f},
        {"critico", 12f},
        {"rareza", RarezaArmas.Raro},
        {"precio", 150}
    },

    // ESPADA 5 - Épica
    new Dictionary<string, object>()
    {
        {"nombre", "Hoja del Dragón"},
        {"descripcion", "Creada con escamas de dragón, otorga un poder devastador"},
        {"modelo", null},
        {"imagenInventario", null},
        {"categoriaItem", CategoriaItemEnum.Arma},
        {"daño", 70f},
        {"velocidadAtaque", 7f},
        {"critico", 15f},
        {"rareza", RarezaArmas.Epico},
        {"precio", 300}
    },

    // ESPADA 6 - Épica
    new Dictionary<string, object>()
    {
        {"nombre", "Espada del Vacío"},
        {"descripcion", "Su filo corta incluso la esencia del alma"},
        {"modelo", null},
        {"imagenInventario", null},
        {"categoriaItem", CategoriaItemEnum.Arma},
        {"daño", 80f},
        {"velocidadAtaque", 6f},
        {"critico", 18f},
        {"rareza", RarezaArmas.Epico},
        {"precio", 350}
    },

    // ESPADA 7 - Legendaria
    new Dictionary<string, object>()
    {
        {"nombre", "Excalibur"},
        {"descripcion", "La espada mítica del rey, símbolo de justicia y poder absoluto"},
        {"modelo", null},
        {"imagenInventario", null},
        {"categoriaItem", CategoriaItemEnum.Arma},
        {"daño", 100f},
        {"velocidadAtaque", 7f},
        {"critico", 20f},
        {"rareza", RarezaArmas.Legendario},
        {"precio", 500}
    },

    // ESPADA 8 - Legendaria
    new Dictionary<string, object>()
    {
        {"nombre", "Filo del Infinito"},
        {"descripcion", "Una espada que se alimenta de las almas caídas, incrementando su poder"},
        {"modelo", null},
        {"imagenInventario", null},
        {"categoriaItem", CategoriaItemEnum.Arma},
        {"daño", 120f},
        {"velocidadAtaque", 8f},
        {"critico", 25f},
        {"rareza", RarezaArmas.Legendario},
        {"precio", 700}
    },

    // ESPADA 9 - Común
    new Dictionary<string, object>()
    {
        {"nombre", "Espada de Bronce"},
        {"descripcion", "Pesada y rudimentaria, pero confiable"},
        {"modelo", null},
        {"imagenInventario", null},
        {"categoriaItem", CategoriaItemEnum.Arma},
        {"daño", 20f},
        {"velocidadAtaque", 5f},
        {"critico", 6f},
        {"rareza", RarezaArmas.Comun},
        {"precio", 35}
    },

    // ESPADA 10 - Rara
    new Dictionary<string, object>()
    {
        {"nombre", "Espada del Relámpago"},
        {"descripcion", "Canaliza energía eléctrica para ataques veloces"},
        {"modelo", null},
        {"imagenInventario", null},
        {"categoriaItem", CategoriaItemEnum.Arma},
        {"daño", 55f},
        {"velocidadAtaque", 8f},
        {"critico", 14f},
        {"rareza", RarezaArmas.Raro},
        {"precio", 200}
    },

    // POCIÓN 1
    new Dictionary<string, object>()
    {
        {"nombre", "Poción de Daño"},
        {"descripcion", "Aumenta temporalmente el daño de tus ataques"},
        {"precio", 75},
        {"modelo", null},
        {"imagenInventario", null},
        {"categoriaItem", CategoriaItemEnum.Pocion},
        {"duracion", 15},
        {"cantidad", 1}
    },

    // POCIÓN 2
    new Dictionary<string, object>()
    {
        {"nombre", "Poción de Velocidad"},
        {"descripcion", "Incrementa tu velocidad de ataque y movimiento"},
        {"precio", 90},
        {"modelo", null},
        {"imagenInventario", null},
        {"categoriaItem", CategoriaItemEnum.Pocion},
        {"duracion", 20},
        {"cantidad", 1}
    },

    // POCIÓN 3
    new Dictionary<string, object>()
    {
        {"nombre", "Poción de Regeneración"},
        {"descripcion", "Regenera lentamente tu salud durante un tiempo"},
        {"precio", 100},
        {"modelo", null},
        {"imagenInventario", null},
        {"categoriaItem", CategoriaItemEnum.Pocion},
        {"duracion", 25},
        {"cantidad", 1}
    },

    // POCIÓN 4
    new Dictionary<string, object>()
    {
        {"nombre", "Poción de Defensa"},
        {"descripcion", "Refuerza temporalmente tu resistencia al daño"},
        {"precio", 85},
        {"modelo", null},
        {"imagenInventario", null},
        {"categoriaItem", CategoriaItemEnum.Pocion},
        {"duracion", 20},
        {"cantidad", 1}
    },

    // POCIÓN 5
    new Dictionary<string, object>()
    {
        {"nombre", "Poción de Energía"},
        {"descripcion", "Restaura parte de tu energía o maná"},
        {"precio", 70},
        {"modelo", null},
        {"imagenInventario", null},
        {"categoriaItem", CategoriaItemEnum.Pocion},
        {"duracion", 2},
        {"cantidad", 1}
    }
};


    }

    public List<Dictionary<string, object>> obtenerTresArmasRandom()
    {
        List<Dictionary<string, object>> copiaItems = new List<Dictionary<string, object>>(items);
        List<Dictionary<string, object>> seleccion = new List<Dictionary<string, object>>();

        for (int i = 0; i < 3 && copiaItems.Count > 0; i++)
        {
            int index = UnityEngine.Random.Range(0, copiaItems.Count);
            seleccion.Add(copiaItems[index]);
            copiaItems.RemoveAt(index);
        }

        return seleccion;
    }
    public void MostrarArmasEnUI()
    {
        panelTienda.SetActive(true);
        List<Dictionary<string, object>> armasSeleccionadas = obtenerTresArmasRandom();
        listObjetosComprables = new GameObject[3] { objetoComprable1, objetoComprable2, objetoComprable3 };

        for (int i = 0; i < armasSeleccionadas.Count; i++)
        {
            Dictionary<string, object> item = armasSeleccionadas[i];
            GameObject contenedorDeDatosComprables = listObjetosComprables[i];
            contenedorDeDatosComprables.SetActive(true);

            listObjetosComprables[i].transform.Find("NombreDelObjeto").GetComponent<Text>().text = item["nombre"].ToString();
            listObjetosComprables[i].transform.Find("DescripcionDelObjeto").GetComponent<Text>().text = item["descripcion"].ToString();
            listObjetosComprables[i].transform.Find("PrecioDelObjeto").GetComponent<Text>().text = item["precio"].ToString();

            Button botonCompra = listObjetosComprables[i].transform.Find("BotonTienda").GetComponent<Button>();

            botonCompra.onClick.RemoveAllListeners();
            botonCompra.onClick.AddListener(() => Comprar(item, contenedorDeDatosComprables));
        }
    }

    public void Recargar()
    {
        if (GameManagerSC.Instancia.scoreManager.obtenerPuntos() >= valorDeRecarga)
        {
            MostrarArmasEnUI();
            valorDeRecarga *= 2;
            GameManagerSC.Instancia.scoreManager.modificarPuntos(-valorDeRecarga);
            Debug.Log($"Puntos restantes {GameManagerSC.Instancia.scoreManager.obtenerPuntos()}");
            TextoDeRecarga.text = valorDeRecarga.ToString();
        }
    }

    public void Comprar(Dictionary<string, object> item, GameObject contenedorDeDatosComprables)
    {
        int precio = Convert.ToInt32(item["precio"]);

        if (GameManagerSC.Instancia.scoreManager.obtenerPuntos() >= precio)
        {
            Debug.Log($"Comprado {item["nombre"].ToString()}");
            GameManagerSC.Instancia.scoreManager.modificarPuntos(-precio);
            Debug.Log($"Puntos restantes {GameManagerSC.Instancia.scoreManager.obtenerPuntos()}");
            contenedorDeDatosComprables.SetActive(false);


            if ((CategoriaItemEnum)item["categoriaItem"] == CategoriaItemEnum.Arma)
            {
                string nombre = (string)item["nombre"];
                string descripcion = (string)item["descripcion"];
                GameObject modelo = (GameObject)item["modelo"];
                Sprite imagenInventario = (Sprite)item["imagenInventario"];
                CategoriaItemEnum categoriaItem = (CategoriaItemEnum)item["categoriaItem"];
                float daño = (float)item["daño"];
                float velocidadAtaque = (float)item["velocidadAtaque"];
                float critico = (float)item["critico"];
                RarezaArmas rareza = (RarezaArmas)item["rareza"];

                Arma armaNueva = new Arma(nombre,descripcion,modelo,imagenInventario,categoriaItem,daño,velocidadAtaque ,critico ,rareza);

                Inventario.Instancia.AgregarArma(armaNueva);
            }
            else
            {
                string nombre = (string)item["nombre"];
                string descripcion = (string)item["descripcion"];
                GameObject modelo = (GameObject)item["modelo"];
                Sprite imagenInventario = (Sprite)item["imagenInventario"];
                CategoriaItemEnum categoriaItem = (CategoriaItemEnum)item["categoriaItem"];
                int duracion = (int)item["duracion"];
                int cantidad = (int)item["cantidad"];

                Pocion pocionNueva = new Pocion(nombre, descripcion, modelo, imagenInventario, categoriaItem, duracion,cantidad);

                Inventario.Instancia.AgregarPocion(pocionNueva);


            }
        }
        else
        {
            Debug.Log("No tenes suficientes monedas");
        }


    }

}





