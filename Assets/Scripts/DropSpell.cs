using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropSpell : MonoBehaviour, IDropHandler
{
    public RectTransform slotRectTransform;
    public Image slotImage;
    public Image spellImage;

    public void OnDrop(PointerEventData eventData)
    {
        DragSpell dragSpell = eventData.pointerDrag.GetComponent<DragSpell>();

        if (!dragSpell.isEquip)
        {
            dragSpell.isEquip = true;
            dragSpell.slotImage = slotImage;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = slotRectTransform.anchoredPosition;
            slotImage.sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
            spellImage.enabled = false;
        }
    }
}