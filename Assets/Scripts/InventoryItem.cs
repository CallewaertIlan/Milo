using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GetComponent<Image>().sprite != null)
        {
            initialPos = transform.localPosition;
            baseParent = transform.parent;

            transform.SetParent(FindHighestParent(transform), true);
            transform.SetAsFirstSibling();
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