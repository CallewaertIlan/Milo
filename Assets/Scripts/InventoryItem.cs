using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour,
    IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //public string itemName;
    private RectTransform rectTransform;

    public Vector2 initialPos;
    public bool isOnImage;

    private Transform baseParent;
    [SerializeField] private Canvas canvas;


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = FindHighestParent(transform).GetComponent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GetComponent<Image>().sprite != null)
        {
            initialPos = transform.localPosition;
            baseParent = transform.parent;
            transform.SetParent(canvas.transform, true);
            rectTransform.SetAsLastSibling();
            isOnImage = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isOnImage == false)
        {
            transform.SetParent(baseParent, true);
            rectTransform.localPosition = initialPos;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.Translate(eventData.delta);

    }

    private Transform FindHighestParent(Transform child)
    {
        Transform parent = child.parent;

        while (parent != null)
        {
            child = parent;
            parent = child.parent;
        }

        return child;
    }

}