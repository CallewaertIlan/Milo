using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrowSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Ressources res = eventData.pointerDrag.GetComponent<Ressources>();
        Equipement equip = eventData.pointerDrag.GetComponent<Equipement>();

        if (res != null) InventoryManager.Instance.Remove(res);
        if (equip != null) InventoryManager.Instance.Remove(equip);

        Debug.Log("Drop Throw");

    }
}