using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    Item item;
    Image iconoDelSlot;
    Sprite IconoPorDefecto;

    private void Awake()
    {
        IconoPorDefecto = Resources.Load<Sprite>("SpritesInventario/Error");
        iconoDelSlot = GetComponent<Image>();
        iconoDelSlot.enabled = false;

    }

    public void SetItem(Item newItem)
    {
        item = newItem;
        if (item == null)
        {
            ClearSlot();
            return;
        }
        else
        {
            Debug.Log("Paso por aca");
            iconoDelSlot.sprite = item.ImagenInventario ?? IconoPorDefecto;
            GetComponent<Image>().enabled = true;

        }
    }

    public void ClearSlot()
    {
        item = null;
        iconoDelSlot.sprite = null;
        GetComponent<Image>().enabled = false;
    }

    public Item GetItem() => item;
}
