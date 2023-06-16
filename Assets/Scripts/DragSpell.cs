using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragSpell : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public string spellName;
    public RectTransform rectTransform;
    [HideInInspector] public Vector2 initialPosition;
    public Canvas canvas;
    [HideInInspector] public Image slotImage;
    public bool isEquip = false;

    private void Start()
    {
        initialPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slotImage != null)
        {
            isEquip = false;
            slotImage.sprite = null;
            slotImage.enabled = true;
            slotImage = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (slotImage == null)
        {
            rectTransform.anchoredPosition = initialPosition;
        }
    }
}