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
                {"modelo", ""},
                {"imagenInventario", ""},
                {"categoriaItem", "Arma"},
                {"daño", 15},
                {"velocidadAtaque", 5},
                {"critico", 5},
                {"rareza", "Común"},
                {"precio", 20}
            },

            // ESPADA 2 - Común
            new Dictionary<string, object>()
            {
                {"nombre", "Espada de Hierro"},
                {"descripcion", "Una espada simple pero resistente, ideal para aventureros novatos"},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"categoriaItem", "Arma"},
                {"daño", 25},
                {"velocidadAtaque", 6},
                {"critico", 7},
                {"rareza", "Común"},
                {"precio", 45}
            },

            // ESPADA 3 - Rara
            new Dictionary<string, object>()
            {
                {"nombre", "Espada del Alba"},
                {"descripcion", "Brilla con la luz del amanecer, otorgando precisión y poder"},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"categoriaItem", "Arma"},
                {"daño", 40},
                {"velocidadAtaque", 6},
                {"critico", 10},
                {"rareza", "Raro"},
                {"precio", 120}
            },

            // ESPADA 4 - Rara
            new Dictionary<string, object>()
            {
                {"nombre", "Espada Carmesí"},
                {"descripcion", "Forjada con el fuego de un volcán, su filo arde con furia"},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"categoriaItem", "Arma"},
                {"daño", 50},
                {"velocidadAtaque", 5},
                {"critico", 12},
                {"rareza", "Raro"},
                {"precio", 150}
            },

            // ESPADA 5 - Épica
            new Dictionary<string, object>()
            {
                {"nombre", "Hoja del Dragón"},
                {"descripcion", "Creada con escamas de dragón, otorga un poder devastador"},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"categoriaItem", "Arma"},
                {"daño", 70},
                {"velocidadAtaque", 7},
                {"critico", 15},
                {"rareza", "Épico"},
                {"precio", 300}
            },

            // ESPADA 6 - Épica
            new Dictionary<string, object>()
            {
                {"nombre", "Espada del Vacío"},
                {"descripcion", "Su filo corta incluso la esencia del alma"},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"categoriaItem", "Arma"},
                {"daño", 80},
                {"velocidadAtaque", 6},
                {"critico", 18},
                {"rareza", "Épico"},
                {"precio", 350}
            },

            // ESPADA 7 - Legendaria
            new Dictionary<string, object>()
            {
                {"nombre", "Excalibur"},
                {"descripcion", "La espada mítica del rey, símbolo de justicia y poder absoluto"},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"categoriaItem", "Arma"},
                {"daño", 100},
                {"velocidadAtaque", 7},
                {"critico", 20},
                {"rareza", "Legendario"},
                {"precio", 500}
            },

            // ESPADA 8 - Legendaria
            new Dictionary<string, object>()
            {
                {"nombre", "Filo del Infinito"},
                {"descripcion", "Una espada que se alimenta de las almas caídas, incrementando su poder"},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"categoriaItem", "Arma"},
                {"daño", 120},
                {"velocidadAtaque", 8},
                {"critico", 25},
                {"rareza", "Legendario"},
                {"precio", 700}
            },

            // ESPADA 9 - Común
            new Dictionary<string, object>()
            {
                {"nombre", "Espada de Bronce"},
                {"descripcion", "Pesada y rudimentaria, pero confiable"},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"categoriaItem", "Arma"},
                {"daño", 20},
                {"velocidadAtaque", 5},
                {"critico", 6},
                {"rareza", "Común"},
                {"precio", 35}
            },

            // ESPADA 10 - Rara
            new Dictionary<string, object>()
            {
                {"nombre", "Espada del Relámpago"},
                {"descripcion", "Canaliza energía eléctrica para ataques veloces"},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"categoriaItem", "Arma"},
                {"daño", 55},
                {"velocidadAtaque", 8},
                {"critico", 14},
                {"rareza", "Raro"},
                {"precio", 200}
            },

            // POCIÓN 1
            new Dictionary<string, object>()
            {
                {"nombre", "Poción de Daño"},
                {"descripcion", "Aumenta temporalmente el daño de tus ataques"},
                {"precio", 75},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"categoriaItem", "Pocion"},
                {"Duracion", 15},
                {"Cantidad", 1}
            },

            // POCIÓN 2
            new Dictionary<string, object>()
            {
                {"nombre", "Poción de Velocidad"},
                {"descripcion", "Incrementa tu velocidad de ataque y movimiento"},
                {"precio", 90},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"categoriaItem", "Pocion"},
                {"Duracion", 20},
                {"Cantidad", 1}
            },

            // POCIÓN 3
            new Dictionary<string, object>()
            {
                {"nombre", "Poción de Regeneración"},
                {"descripcion", "Regenera lentamente tu salud durante un tiempo"},
                {"precio", 100},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"categoriaItem", "Pocion"},
                {"Duracion", 25},
                {"Cantidad", 1}
            },

            // POCIÓN 4
            new Dictionary<string, object>()
            {
                {"nombre", "Poción de Defensa"},
                {"descripcion", "Refuerza temporalmente tu resistencia al daño"},
                {"precio", 85},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"categoriaItem", "Pocion"},
                {"Duracion", 20},
                {"Cantidad", 1}
            },

            // POCIÓN 5
            new Dictionary<string, object>()
            {
                {"nombre", "Poción de Energía"},
                {"descripcion", "Restaura parte de tu energía o maná"},
                {"precio", 70},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"categoriaItem", "Pocion"},
                {"Duracion", 2},
                {"Cantidad", 1}
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
        }
        else
        {
            Debug.Log("No tenes suficientes monedas");
        }


    }

}





