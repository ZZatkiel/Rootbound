using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MostrarInfoItem : MonoBehaviour , IPointerClickHandler
{
    GraphicRaycaster raycaster;

    // PARTE DEL ARMA 

    [Header ("Datos del arma")]
    public Text NombreArma;
    public Text DescripcionArma;
    public Text DañoArma;
    public Text VelocidadDeAtaqueArma;
    public Text CriticoArma;
    public Text RarezaArma;

    [Space(3)]
    [Header ("Campos de la UI del arma")]
    public Text TituloDañoArma;
    public Text TituloVelocidadDeAtaqueArma;
    public Text TituloCriticoArma;
    public Text TituloRarezaArma;


    //
    Text[] listaArma;
    Text[] listaPocion;

    Text[] TitulolistaArma;
    Text[] TitulolistaPocion;
    //

    // PARTE DE LA POCION
    [Space (6)]
    [Header("Datos de las pociones")]
    public Text NombrePocion;
    public Text DescripcionPocion;
    public Text DuracionPocion;
    public Text CantidadPocion;

    [Space(3)]
    [Header("Campos de la UI de las pociones")]
    public Text TituloDuracionPocion;
    public Text TituloCantidadPocion;

    private void Awake()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        listaArma = new Text[] { NombreArma, DescripcionArma, DañoArma, VelocidadDeAtaqueArma, CriticoArma, RarezaArma };
        listaPocion = new Text[] { NombrePocion, DescripcionPocion, DuracionPocion, CantidadPocion };

        TitulolistaArma = new Text[]{ TituloDañoArma, TituloVelocidadDeAtaqueArma, TituloCriticoArma, TituloRarezaArma };
        TitulolistaPocion = new Text[] { TituloDuracionPocion, TituloCantidadPocion};

        LimpiarTexto(TitulolistaArma);
        LimpiarTexto(TitulolistaPocion);
        LimpiarTexto(listaArma);
        LimpiarTexto(listaPocion);

    }

    public void ClasificacionDeInformacion(Slot datoObtenido)
    {
        if (datoObtenido.GetCategoria() == CategoriaDelSlotEnum.ArmaSlot)
        {
            LimpiarTexto(listaArma);
            Item item = datoObtenido.GetItem();
            Arma itemConvertido = (Arma)item;

            TituloDañoArma.text = "Daño";
            TituloVelocidadDeAtaqueArma.text = "Vel. Atq.";
            TituloCriticoArma.text = "Critico";
            TituloRarezaArma.text = "Rareza";
            NombreArma.text = itemConvertido.Nombre.ToString();
            DescripcionArma.text = itemConvertido.Descripcion.ToString();
            DañoArma.text = itemConvertido.Daño.ToString();
            VelocidadDeAtaqueArma.text = itemConvertido.VelocidadDeAtaque.ToString();
            CriticoArma.text = itemConvertido.AtaqueCritico.ToString();
            RarezaArma.text = itemConvertido.Rareza.ToString();
            return;
        }
        else
        {
            LimpiarTexto(listaPocion);
            Item item = datoObtenido.GetItem();
            Pocion itemConvertido = (Pocion)item;

            TituloDuracionPocion.text = "Duracion";
            TituloCantidadPocion.text = "Cantidad";
            NombrePocion.text = itemConvertido.Nombre.ToString();
            DescripcionPocion.text = itemConvertido.Descripcion.ToString();
            DuracionPocion.text = itemConvertido.Duracion.ToString();
            CantidadPocion.text = itemConvertido.Cantidad.ToString();
        }
    }

    public void LimpiarTexto(Text[] listaALimpiar)
    {
        for (int i = 0; i < listaALimpiar.Length; i++)
        {
            listaALimpiar[i].text = "";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);

        foreach (var res in results)
        {
            Slot datoObtenido = res.gameObject.GetComponent<Slot>();

            if (datoObtenido != null)
            {
                ClasificacionDeInformacion(datoObtenido);
            }
        }

    }
}
