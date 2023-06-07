using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Equipement equip = eventData.pointerDrag.GetComponent<Equipement>();

        if (equip != null)
        {
            if (InventoryManager.Instance.UnEquip(equip))
            {
                InventoryManager.Instance.AddToInventory(equip);
                InventoryManager.Instance.UpdateInventory();

                Destroy(equip.gameObject);
            }
        }
    }
}
