using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    [SerializeField] private Equipement.EquipementType slotIndex;

    public void OnDrop(PointerEventData eventData)
    {

        InventoryItem ii = eventData.pointerDrag.GetComponent<InventoryItem>();
        Equipement equipementItem = eventData.pointerDrag.GetComponent<Equipement>();

        Debug.Log(equipementItem);

        if (ii != null)
        {
            
            if (equipementItem != null) ii.isOnImage = EquipItem(equipementItem);

            ii.transform.SetParent(transform, true);

            ii.transform.localPosition = new Vector3(0, 0, 0);

            ii.transform.localScale = new Vector3(1, ii.transform.localScale.y, 1);
        }

    }

    private bool EquipItem(Equipement item)
    {
        if (item.equipementType == slotIndex)
        {
            if (InventoryManager.Instance.EquipementIsEmpty(item, (int)slotIndex))
                InventoryManager.Instance.Equip(item, (int)slotIndex);

            return true;
        }
        else return false;
    }
}