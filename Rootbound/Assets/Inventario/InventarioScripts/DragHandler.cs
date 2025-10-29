using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    Slot slot;
    GameObject dragIcon;
    RectTransform dragIconRect;
    Canvas canvas;
    GraphicRaycaster raycaster;


    void Awake()
    {
        slot = GetComponent<Slot>();

        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            canvas = FindFirstObjectByType<Canvas>();
        }

        if (canvas != null)
        {
            raycaster = canvas.GetComponent<GraphicRaycaster>();
        }

        var cg = GetComponent<CanvasGroup>();
        cg.blocksRaycasts = true;

    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slot == null) return;

        if (slot.GetItem() == null) return;

        dragIcon = new GameObject("DragIcon");
        dragIcon.transform.SetParent(canvas.transform, false);
        dragIconRect = dragIcon.AddComponent<RectTransform>();
        Image image = dragIcon.AddComponent<Image>();
        image.sprite = slot.GetImageSlot();

        dragIconRect.sizeDelta = new Vector2(160, 160);
        SetDraggedPosition(eventData);


        var cg = GetComponent<CanvasGroup>();
        if (cg != null) cg.blocksRaycasts = false;




    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
            SetDraggedPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
            Destroy(dragIcon);

        // Reactivar raycasts del slot original
        var cg = GetComponent<CanvasGroup>();
        if (cg != null) cg.blocksRaycasts = true;

        // Hacer raycast desde el canvas para encontrar el slot debajo del cursor
        if (raycaster == null) return;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);

        foreach (var res in results)
        {
            Slot other = res.gameObject.GetComponent<Slot>();
            if (other != null)
            {
                // pedir al inventario que mueva/intercambie
                if (Inventario.Instancia != null)
                {
                    bool ok = Inventario.Instancia.Swap(slot.GetCategoria(), slot.GetIndex(), other.GetCategoria(), other.GetIndex());
                    if (!ok)
                    {
                        Debug.Log("Movimiento no válido — el item volverá a su lugar.");
                        // No necesitamos hacer nada más: como no se movió, el item sigue en el slot original.
                    }
                    else
                    {
                        Debug.Log($"Swap realizado: {slot.GetCategoria()}, {slot.GetIndex()} -> {other.GetCategoria()}, {other.GetIndex()}");
                    }
                }
                return;
            }
        }
    }

    void SetDraggedPosition(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            eventData.position,
            canvas.worldCamera,
            out pos);
        dragIconRect.localPosition = pos;
    }
}