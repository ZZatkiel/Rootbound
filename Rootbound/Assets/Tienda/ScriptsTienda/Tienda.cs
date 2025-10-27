using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tienda : MonoBehaviour
{
    public List<Dictionary<string, object>> armas;

    public GameObject panelTienda;
    public GameObject objetoComprable1;
    public GameObject objetoComprable2;
    public GameObject objetoComprable3;

    GameObject[] listObjetosComprables;

    private void Awake()
    {
        armas = new List<Dictionary<string, object>>()
        {
            // 1. Común
            new Dictionary<string, object>()
            {
                {"nombre", "Espada Espadita"},
                {"descripcion", "Pequeña espada que sirve para principiantes"},
                {"precio", 20},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"daño", 15},
                {"velocidadAtaque", 5},
                {"critico", 5},
                {"rareza", 1}
            },
            // 2. Común
            new Dictionary<string, object>()
            {
                {"nombre", "Espada Ligera"},
                {"descripcion", "Espada común rápida y ligera"},
                {"precio", 25},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"daño", 18},
                {"velocidadAtaque", 6},
                {"critico", 6},
                {"rareza", 1}
            },
            // 3. Común
            new Dictionary<string, object>()
            {
                {"nombre", "Espada Básica"},
                {"descripcion", "Espada de entrenamiento, confiable y simple"},
                {"precio", 30},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"daño", 20},
                {"velocidadAtaque", 5},
                {"critico", 4},
                {"rareza", 1}
            },
            // 4. Común
            new Dictionary<string, object>()
            {
                {"nombre", "Espada Cortante"},
                {"descripcion", "Espada común con filo decente"},
                {"precio", 28},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"daño", 22},
                {"velocidadAtaque", 5},
                {"critico", 5},
                {"rareza", 1}
            },
            // 5. Común
            new Dictionary<string, object>()
            {
                {"nombre", "Espada de Hierro"},
                {"descripcion", "Espada común de hierro reforzado"},
                {"precio", 30},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"daño", 25},
                {"velocidadAtaque", 4},
                {"critico", 3},
                {"rareza", 1}
            },
            // 6. Rara
            new Dictionary<string, object>()
            {
                {"nombre", "Espada del Guardián"},
                {"descripcion", "Espada rara que aumenta la defensa"},
                {"precio", 75},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"daño", 60},
                {"velocidadAtaque", 6},
                {"critico", 15},
                {"rareza", 2}
            },
            // 7. Rara
            new Dictionary<string, object>()
            {
                {"nombre", "Espada de la Aurora"},
                {"descripcion", "Espada rara con brillo místico"},
                {"precio", 80},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"daño", 65},
                {"velocidadAtaque", 7},
                {"critico", 18},
                {"rareza", 2}
            },
            // 8. Rara
            new Dictionary<string, object>()
            {
                {"nombre", "Espada Veloz"},
                {"descripcion", "Espada rara ligera y rápida"},
                {"precio", 90},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"daño", 70},
                {"velocidadAtaque", 8},
                {"critico", 20},
                {"rareza", 2}
            },
            // 9. Épica
            new Dictionary<string, object>()
            {
                {"nombre", "Filo Épico"},
                {"descripcion", "Espada épica con gran daño y velocidad"},
                {"precio", 250},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"daño", 250},
                {"velocidadAtaque", 10},
                {"critico", 25},
                {"rareza", 3}
            },
            // 10. Épica
            new Dictionary<string, object>()
            {
                {"nombre", "Espada de Fuego"},
                {"descripcion", "Espada épica que inflige quemaduras"},
                {"precio", 280},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"daño", 270},
                {"velocidadAtaque", 9},
                {"critico", 30},
                {"rareza", 3}
            },
            // 11. Épica
            new Dictionary<string, object>()
            {
                {"nombre", "Espada de Hielo"},
                {"descripcion", "Espada épica que ralentiza enemigos"},
                {"precio", 300},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"daño", 300},
                {"velocidadAtaque", 8},
                {"critico", 28},
                {"rareza", 3}
            },
            // 12. Legendaria
            new Dictionary<string, object>()
            {
                {"nombre", "Excalibur Legendaria"},
                {"descripcion", "La legendaria espada de los héroes"},
                {"precio", 1000},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"daño", 500},
                {"velocidadAtaque", 12},
                {"critico", 50},
                {"rareza", 4}
            },
            // 13. Legendaria
            new Dictionary<string, object>()
            {
                {"nombre", "Espada del Dragón"},
                {"descripcion", "Legendaria espada que arde con fuego dracónico"},
                {"precio", 1200},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"daño", 550},
                {"velocidadAtaque", 11},
                {"critico", 55},
                {"rareza", 4}
            },
            // 14. Legendaria
            new Dictionary<string, object>()
            {
                {"nombre", "Espada Celestial"},
                {"descripcion", "Legendaria espada con poder divino"},
                {"precio", 1500},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"daño", 600},
                {"velocidadAtaque", 12},
                {"critico", 60},
                {"rareza", 4}
            },
            // 15. Legendaria
            new Dictionary<string, object>()
            {
                {"nombre", "Espada de la Eternidad"},
                {"descripcion", "Legendaria espada que nunca se rompe"},
                {"precio", 2000},
                {"modelo", ""},
                {"imagenInventario", ""},
                {"daño", 650},
                {"velocidadAtaque", 13},
                {"critico", 65},
                {"rareza", 4}
            }
        };
    }


    public List<Dictionary<string, object>> obtenerTresArmasRandom()
    {
        List<Dictionary<string, object>> copiaArmas = new List<Dictionary<string, object>>(armas);
        List<Dictionary<string, object>> seleccion = new List<Dictionary<string, object>>();
        
        for (int i = 0; i < 3 && copiaArmas.Count > 0; i++)
        {
            int index = Random.Range(0, copiaArmas.Count);
            seleccion.Add(copiaArmas[index]);
            copiaArmas.RemoveAt(index);
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

            listObjetosComprables[i].transform.Find("NombreDelObjeto").GetComponent<Text>().text = item["nombre"].ToString();
            listObjetosComprables[i].transform.Find("DescripcionDelObjeto").GetComponent<Text>().text = item["descripcion"].ToString();
            listObjetosComprables[i].transform.Find("PrecioDelObjeto").GetComponent<Text>().text = item["precio"].ToString();

            Button botonCompra = listObjetosComprables[i].transform.Find("BotonTienda").GetComponent<Button>();

            botonCompra.onClick.RemoveAllListeners();
            botonCompra.onClick.AddListener(() => Comprar(item));
        }
    }

    public void Comprar(Dictionary<string, object> item)
    {
        Debug.Log($"Comprado {item["nombre"].ToString()}");
        // si no tenes las monedas suficientes, no podes comprarlo y se pone en rojo durante 5s el boton
        // si tenes las monedas suficientes el objeto se desactiva y pasa al Inventario

        // osea hay que crear ya el gameManager con el audioManager y el ScoreManager
        // el ScoreManager tiene las monedas del jugador
        
    }


}





