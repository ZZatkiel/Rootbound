using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    Item item;
    Image iconoDelSlot;
    Sprite IconoPorDefecto;
    Text contadorPocion;

    private void Awake()
    {

        IconoPorDefecto = Resources.Load<Sprite>("SpritesInventario/Error");
        iconoDelSlot = GetComponent<Image>();
        iconoDelSlot.enabled = false;
        GameObject ObjetoContador = new GameObject("ContadorPociones");
        Text contadorPocion = ObjetoContador.AddComponent<Text>();

        ObjetoContador.transform.SetParent(gameObject.transform, false);

        ObjetoContador.GetComponent<RectTransform>().localPosition = new Vector3(-732, 160, 0);
        ObjetoContador.GetComponent<RectTransform>().sizeDelta = new Vector2(66, 66);

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
        iconoDelSlot.sprite = null;
        GetComponent<Image>().enabled = false;
        contadorPocion.text = "";
    }

    public Item GetItem() => item;
}
