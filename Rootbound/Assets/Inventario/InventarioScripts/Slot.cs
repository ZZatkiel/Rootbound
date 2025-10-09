using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    Item item;
    Image iconoDelSlot; // Imagen donde se guarda el item
    Sprite IconoPorDefecto; //Si no tiene icono el item
    Text contadorPocion;

    public void InicializarSlot()
    {
        iconoDelSlot = GetComponent<Image>();

        IconoPorDefecto = Resources.Load<Sprite>("SpritesInventario/Error");
        iconoDelSlot.enabled = false;

        GameObject ObjetoContador = new GameObject("ContadorPociones");
        contadorPocion = ObjetoContador.AddComponent<Text>();
        RectTransform posicionContador = contadorPocion.GetComponent<RectTransform>();

        ObjetoContador.transform.SetParent(gameObject.transform, false);

        ObjetoContador.GetComponent<RectTransform>().localPosition = new Vector3(-20, 11, 0);
        ObjetoContador.GetComponent<RectTransform>().sizeDelta = new Vector2(66, 66);

        posicionContador.anchorMin = new Vector2(1, 0);
        posicionContador.anchorMax = new Vector2(1, 0);

        contadorPocion.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        contadorPocion.fontSize = 55;
        contadorPocion.alignment = TextAnchor.MiddleCenter;
        contadorPocion.color = Color.white;

    }

    public void SetItem(Item newItem)
    {
        item = newItem;
        if (item == null)
        {
            ClearSlot();
            return;
        }

        iconoDelSlot.sprite = item.ImagenInventario ?? IconoPorDefecto;
        GetComponent<Image>().enabled = true;

        if (newItem.CategoriaItem == CategoriaItemEnum.Pocion)
        {
            Pocion pocion = (Pocion)newItem;
            contadorPocion.text = pocion.Cantidad.ToString();
        }
        else
        {
            contadorPocion.text = "";
        }

    }

    public void ClearSlot()
    {
        item = null;

        if (iconoDelSlot != null)
            iconoDelSlot.sprite = null;

        if (GetComponent<Image>() != null)
            GetComponent<Image>().enabled = false;

        if (contadorPocion != null)
            contadorPocion.text = "";
    }

    public Item GetItem() => item;
}
